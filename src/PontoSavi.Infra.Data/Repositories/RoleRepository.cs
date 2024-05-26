using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Net;

using PontoSavi.Domain.Exceptions;
using PontoSavi.Domain.Repositories;
using PontoSavi.Domain.DTOs;
using PontoSavi.Domain.Filters;
using PontoSavi.Infra.Data.Context;

namespace PontoSavi.Infra.Data.Repositories;

public class RoleRepository : BaseRepository<IdentityRole>, IRoleRepository
{
    private readonly AppDbContext _context;
    private readonly RoleManager<IdentityRole> _roleManager;

    public RoleRepository(AppDbContext context, RoleManager<IdentityRole> roleManager) : base(context)
    {
        _context = context;
        _roleManager = roleManager;
    }

    public async Task<QueryResult<RoleDTO>> Query(RoleFilter filter)
    {
        var query = _context.Roles.AsNoTracking().AsQueryable();

        if (!filter.Search.IsNullOrEmpty()) query = query.Where(r => r.Name!.Contains(filter.Search!, StringComparison.CurrentCultureIgnoreCase));

        if (!filter.Id.IsNullOrEmpty()) query = query.Where(r => r.Id == filter.Id);
        if (!filter.Name.IsNullOrEmpty()) query = query.Where(r => r.Name!.Contains(filter.Name!, StringComparison.CurrentCultureIgnoreCase));

        if (!filter.NameOrderSort.IsNullOrEmpty())
            query = filter.NameOrderSort!.Equals("asc", StringComparison.CurrentCultureIgnoreCase) ? query.OrderBy(r => r.Name) :
                filter.NameOrderSort!.Equals("desc", StringComparison.CurrentCultureIgnoreCase) ? query.OrderByDescending(r => r.Name) :
                query;

        var totalCount = await query.CountAsync();

        var roles = await query
            .Skip(filter.PageIndex * filter.PageSize)
            .Take(filter.PageSize)
            .Select(r => new RoleDTO(r))
            .ToListAsync();

        return new QueryResult<RoleDTO>(roles, totalCount);
    }

    public async Task<bool> ExistsById(string id) =>
        await _roleManager.Roles.AnyAsync(r => r.Id == id);

    public async Task<bool> ExistsByName(string name) =>
        await _roleManager.RoleExistsAsync(name);

    public async Task<IdentityRole> GetById(string id) =>
        await _roleManager.FindByIdAsync(id) ??
            throw new AppException("Perfil não encontrado!", HttpStatusCode.NotFound);

    public async Task<IdentityRole> GetByName(string name) =>
        await _roleManager.FindByNameAsync(name) ??
            throw new AppException("Perfil não encontrado!", HttpStatusCode.NotFound);

    public async new Task<IdentityRole> Add(IdentityRole role)
    {
        var result = await _roleManager.CreateAsync(role);

        return result.Succeeded ? role :
            throw new AppException("Não foi possível criar o perfil!", HttpStatusCode.BadRequest);
    }

    public async new Task<IdentityRole> Update(IdentityRole role)
    {
        var result = await _roleManager.UpdateAsync(role);

        return result.Succeeded ? role :
            throw new AppException("Não foi possível atualizar o perfil!", HttpStatusCode.BadRequest);
    }

    public async Task<IdentityRole> Remove(string name)
    {
        var role = await _roleManager.FindByNameAsync(name) ??
            throw new AppException("Perfil não encontrado!", HttpStatusCode.NotFound);

        var result = await _roleManager.DeleteAsync(role);

        return result.Succeeded ? role :
            throw new AppException("Não foi possível remover o perfil!", HttpStatusCode.BadRequest);
    }
}