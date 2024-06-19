using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Net;

using PontoSavi.Domain.Exceptions;
using PontoSavi.Domain.Repositories;
using PontoSavi.Domain.Filters;
using PontoSavi.Infra.Data.Context;
using PontoSavi.Domain.Entities;

namespace PontoSavi.Infra.Data.Repositories;

public class RoleRepository : BaseRepository<Role>, IRoleRepository
{
    private readonly AppDbContext _context;
    private readonly RoleManager<Role> _roleManager;

    public RoleRepository(AppDbContext context, RoleManager<Role> roleManager) : base(context)
    {
        _context = context;
        _roleManager = roleManager;
    }

    public async Task<QueryResult<Role>> Query(RoleFilter filter)
    {
        var query = _context.Roles.AsNoTracking().AsQueryable();

        query = query.Where(r => r.CompanyId == filter.CompanyId);

        if (!filter.Search.IsNullOrEmpty())
            query = query.Where(r =>
                r.Name!.ToLower().Contains(filter.Search!.ToLower()));

        if (filter.Id.HasValue) query = query.Where(r => r.Id == filter.Id);
        if (!filter.Name.IsNullOrEmpty()) query = query.Where(r => r.Name!.ToLower().Contains(filter.Name!.ToLower()));

        if (filter.NameDescOrderSort.HasValue)
            query = filter.NameDescOrderSort.Value ? query.OrderByDescending(r => r.Name) : query.OrderBy(r => r.Name);

        var totalCount = await query.CountAsync();

        var roles = await query
            .Skip(filter.PageIndex * filter.PageSize)
            .Take(filter.PageSize)
            .ToListAsync();

        return new QueryResult<Role>(roles, totalCount);
    }

    public async Task<bool> ExistsById(int id, int companyId) =>
        await _context.Roles.AsNoTracking().AnyAsync(r => r.Id == id && r.CompanyId == companyId);

    public async Task<bool> ExistsByName(string name, int companyId) =>
        await _context.Roles.AsNoTracking().AnyAsync(r => r.Name == name && r.CompanyId == companyId);

    public async Task<Role> GetById(int id, int companyId) =>
        await _context.Roles.AsNoTracking().FirstOrDefaultAsync(r => r.Id == id && r.CompanyId == companyId) ??
            throw new AppException("Perfil não encontrado!", HttpStatusCode.NotFound);

    public async Task<Role> GetByName(string name, int companyId) =>
        await _context.Roles.AsNoTracking().FirstOrDefaultAsync(r => r.Name == name && r.CompanyId == companyId) ??
            throw new AppException("Perfil não encontrado!", HttpStatusCode.NotFound);

    public async Task<List<Role>> GetByUser(int userId, int companyId) =>
        await _context.Roles.AsNoTracking()
            .Join(_context.UserRoles.AsNoTracking(), r => r.Id, ur => ur.RoleId, (r, ur) => new { r, ur })
            .Where(rur => rur.ur.UserId == userId && rur.r.CompanyId == companyId)
            .Select(rur => rur.r)
            .ToListAsync();

    // public async new Task<Role> Add(Role role)
    // {
    //     var result = await _roleManager.CreateAsync(role);

    //     return result.Succeeded ? role :
    //         throw new AppException("Não foi possível criar o perfil!", HttpStatusCode.BadRequest);
    // }

    public async new Task<Role> Update(Role role)
    {
        var result = await _roleManager.UpdateAsync(role);

        return result.Succeeded ? role :
            throw new AppException("Não foi possível atualizar o perfil!", HttpStatusCode.BadRequest);
    }

    public async new Task<Role> Remove(Role role)
    {
        var result = await _roleManager.DeleteAsync(role);

        return result.Succeeded ? role :
            throw new AppException("Não foi possível remover o perfil!", HttpStatusCode.BadRequest);
    }
}