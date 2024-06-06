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

        if (!filter.Search.IsNullOrEmpty())
            query = query.Where(u => u.UserName!.Contains(filter.Search!, StringComparison.CurrentCultureIgnoreCase) ||
                u.Email!.Contains(filter.Search!, StringComparison.CurrentCultureIgnoreCase) ||
                u.PhoneNumber!.Contains(filter.Search!, StringComparison.CurrentCultureIgnoreCase));

        if (!filter.PublicId.IsNullOrEmpty()) query = query.Where(u => u.PublicId == filter.PublicId);
        if (!filter.UserName.IsNullOrEmpty()) query = query.Where(u => u.UserName!.Contains(filter.UserName!, StringComparison.CurrentCultureIgnoreCase));
        if (!filter.Email.IsNullOrEmpty()) query = query.Where(u => u.Email!.Contains(filter.Email!, StringComparison.CurrentCultureIgnoreCase));
        if (!filter.PhoneNumber.IsNullOrEmpty()) query = query.Where(u => u.PhoneNumber!.Contains(filter.PhoneNumber!, StringComparison.CurrentCultureIgnoreCase));

        if (!filter.UserNameOrderSort.IsNullOrEmpty())
            query = filter.UserNameOrderSort!.Equals("asc", StringComparison.CurrentCultureIgnoreCase) ? query.OrderBy(u => u.UserName) :
                filter.UserNameOrderSort!.Equals("desc", StringComparison.CurrentCultureIgnoreCase) ? query.OrderByDescending(u => u.UserName) :
                query;
        if (!filter.EmailOrderSort.IsNullOrEmpty())
            query = filter.EmailOrderSort!.Equals("asc", StringComparison.CurrentCultureIgnoreCase) ? query.OrderBy(u => u.Email) :
                filter.EmailOrderSort!.Equals("desc", StringComparison.CurrentCultureIgnoreCase) ? query.OrderByDescending(u => u.Email) :
                query;

        var totalCount = await query.CountAsync();

        var users = await query
            .Skip(filter.PageIndex * filter.PageSize)
            .Take(filter.PageSize)
            .Select(u => new UserDTO
            {
                PublicId = u.PublicId!,
                Name = u.Name!,
                UserName = u.UserName!,
                Email = u.Email!,
                PhoneNumber = u.PhoneNumber!,
                Roles = (List<string>)_userManager.GetRolesAsync(u).Result
            })
            .ToListAsync();

        return new QueryResult<UserDTO>(users, totalCount);
    }

    public async Task<User> Auth(string userName, string password)
    {
        if (!await ExistsByUserName(userName)) throw new AppException("Erro ao fazer login!", HttpStatusCode.Unauthorized);
        var user = await GetByUserName(userName);

        if (!await CheckPassword(user, password)) throw new AppException("Erro ao fazer login!", HttpStatusCode.Unauthorized);

        return user;
    }

    public async Task<bool> CheckPassword(string id, string password) =>
        await _userManager.CheckPasswordAsync(await GetById(id), password);

    public async Task<bool> CheckPassword(User user, string password) =>
        await _userManager.CheckPasswordAsync(user, password);

    public async Task<bool> ExistsByEmail(string email) =>
        await _userManager.Users.AsNoTracking().AnyAsync(u => u.Email == email);

    public async Task<bool> ExistsById(int id) =>
        await _userManager.Users.AsNoTracking().AnyAsync(u => u.Id == id);

    public async Task<bool> ExistsByPublicId(string publicId) =>
        await _userManager.Users.AsNoTracking().AnyAsync(u => u.PublicId == publicId);

    public async Task<bool> ExistsByPhoneNumber(string phoneNumber) =>
        await _userManager.Users.AsNoTracking().AnyAsync(u => u.PhoneNumber == phoneNumber);

    public async Task<bool> ExistsByUserName(string userName) =>
        await _userManager.Users.AsNoTracking().AnyAsync(u => u.UserName == userName);

    public async Task<User> GetByEmail(string email) =>
        await _userManager.FindByEmailAsync(email) ?? throw new AppException("Usuário não encontrado!", HttpStatusCode.NotFound);

    public async Task<User> GetById(int id) =>
        await _userManager.FindByIdAsync(id.ToString()) ?? throw new AppException("Usuário não encontrado!", HttpStatusCode.NotFound);

    public async Task<User> GetByPublicId(string publicId) =>
        await _userManager.Users.AsNoTracking().FirstAsync(u => u.PublicId == publicId);

    public async Task<User> GetByPhoneNumber(string phoneNumber) =>
        await _userManager.Users.AsNoTracking().FirstAsync(u => u.PhoneNumber == phoneNumber);

    public async Task<User> GetByUserName(string userName) =>
        await _userManager.FindByNameAsync(userName) ?? throw new AppException("Usuário não encontrado!", HttpStatusCode.NotFound);

    public async Task<List<string>> GetRoles(User user) =>
        (List<string>)await _userManager.GetRolesAsync(user);

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

    public async Task<bool> UpdatePassword(int id, string oldPassword, string newPassword)
    {
        var user = await GetById(id);
        var result = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);

        if (!result.Succeeded) throw new AppException(result.Errors.First().Description, HttpStatusCode.BadRequest);

        return true;
    }

    public async Task<bool> RemoveByUserName(string userName)
    {
        var user = await GetByUserName(userName);

        await _userManager.DeleteAsync(user);

        return true;
    }
}