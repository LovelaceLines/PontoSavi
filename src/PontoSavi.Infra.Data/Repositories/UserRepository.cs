using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Net;

using PontoSavi.Domain.Exceptions;
using PontoSavi.Domain.Repositories;
using PontoSavi.Infra.Data.Context;
using PontoSavi.Domain.Filters;
using PontoSavi.Domain.DTOs;
using PontoSavi.Domain.Entities;

namespace PontoSavi.Infra.Data.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    private readonly AppDbContext _context;
    private readonly UserManager<User> _userManager;

    public UserRepository(AppDbContext context, UserManager<User> userManager) : base(context)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<QueryResult<UserDTO>> Query(UserFilter filter)
    {
        var query = _context.Users.AsNoTracking().AsQueryable();

        query = query.Where(u => u.TenantId == filter.TenantId);

        if (!filter.Search.IsNullOrEmpty())
            query = query.Where(u =>
                u.Name.ToLower().Contains(filter.Search!.ToLower()) ||
                u.UserName!.ToLower().Contains(filter.Search!.ToLower()) ||
                u.Email!.ToLower().Contains(filter.Search!.ToLower()) ||
                u.PhoneNumber!.ToLower().Contains(filter.Search!.ToLower()));

        if (filter.Id.HasValue) query = query.Where(u => u.Id == filter.Id);

        if (!filter.Name.IsNullOrEmpty()) query = query.Where(u => u.Name!.ToLower().Contains(filter.Name!.ToLower()));
        if (!filter.UserName.IsNullOrEmpty()) query = query.Where(u => u.UserName!.ToLower().Contains(filter.UserName!.ToLower()));
        if (!filter.Email.IsNullOrEmpty()) query = query.Where(u => u.Email!.ToLower().Contains(filter.Email!.ToLower()));
        if (!filter.PhoneNumber.IsNullOrEmpty()) query = query.Where(u => u.PhoneNumber!.ToLower().Contains(filter.PhoneNumber!.ToLower()));

        if (filter.IdDescOrderSort.HasValue)
            query = filter.IdDescOrderSort.Value ? query.OrderByDescending(u => u.Id) : query.OrderBy(u => u.Id);
        if (filter.NameDescOrderSort.HasValue)
            query = filter.NameDescOrderSort.Value ? query.OrderByDescending(u => u.Name) : query.OrderBy(u => u.Name);
        if (filter.UserNameDescOrderSort.HasValue)
            query = filter.UserNameDescOrderSort.Value ? query.OrderByDescending(u => u.UserName) : query.OrderBy(u => u.UserName);
        if (filter.EmailDescOrderSort.HasValue)
            query = filter.EmailDescOrderSort.Value ? query.OrderByDescending(u => u.Email) : query.OrderBy(u => u.Email);

        var totalCount = await query.CountAsync();

        var users = await query
            .Skip(filter.PageIndex * filter.PageSize)
            .Take(filter.PageSize)
            .Select(u => new UserDTO(u)
            {
                Roles = _context.UserRoles.Where(ur => ur.UserId == u.Id)
                    .Join(_context.Roles, ur => ur.RoleId, r => r.Id, (ur, r) => r)
                    .ToList(),
            })
            .ToListAsync();

        return new QueryResult<UserDTO>(users, totalCount);
    }

    public async Task<User> Auth(string userName, string password)
    {
        if (!await ExistsByUserName(userName)) throw new AppException("Erro ao fazer login!", HttpStatusCode.Unauthorized);

        var user = await GetByUserName(userName);

        if (!await CheckPassword(user, password)) throw new AppException("Erro ao fazer login!", HttpStatusCode.Unauthorized);

        // TODO refactor to tenantId
        user.Tenant = await GetCompanyByUserId(user.Id);

        return user;
    }

    public async Task<bool> CheckPassword(User user, string password) =>
        await _userManager.CheckPasswordAsync(user, password);

    public async Task<bool> ExistsById(int id, int tenantId) =>
        await _userManager.Users.AsNoTracking().AnyAsync(u => u.Id == id && u.TenantId == tenantId);

    public async Task<bool> ExistsByUserName(string userName) =>
        await _userManager.Users.AsNoTracking().AnyAsync(u => u.UserName == userName);

    public async Task<User> GetById(int id, int tenantId) =>
        await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id && u.TenantId == tenantId) ??
            throw new AppException("Usuário não encontrado!", HttpStatusCode.NotFound);

    public async Task<User> GetByUserName(string userName) =>
        await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.UserName == userName) ??
            throw new AppException("Usuário não encontrado!", HttpStatusCode.NotFound);

    public async Task<List<Role>> GetRoles(User user) =>
        await _context.UserRoles
            .Where(ur => ur.UserId == user.Id)
            .Join(_context.Roles, ur => ur.RoleId, r => r.Id, (ur, r) => r)
            .ToListAsync();

    public async Task<Company> GetCompanyByUserId(int userId) =>
        await _context.Users.AsNoTracking()
            .Where(u => u.Id == userId)
            .Include(u => u.Tenant)
            .Select(u => u.Tenant)
            .FirstOrDefaultAsync() ?? throw new AppException("Empresa não encontrada!", HttpStatusCode.NotFound);

    public async Task<User> Add(User user, string password)
    {
        var result = await _userManager.CreateAsync(user, password);

        if (!result.Succeeded) CatchError(new Exception(result.Errors.First().Description));

        return user;
    }

    public new async Task<User> Update(User user)
    {
        var result = await _userManager.UpdateAsync(user);

        if (!result.Succeeded) CatchError(new Exception(result.Errors.First().Description));

        return user;
    }

    public async Task<bool> UpdatePassword(User user, string oldPassword, string newPassword)
    {
        var result = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);

        if (!result.Succeeded) throw new AppException(result.Errors.First().Description, HttpStatusCode.BadRequest);

        return true;
    }
}