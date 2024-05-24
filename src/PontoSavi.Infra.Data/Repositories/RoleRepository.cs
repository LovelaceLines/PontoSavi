using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net;

using PontoSavi.Domain.Exceptions;
using PontoSavi.Domain.Repositories;
using PontoSavi.Infra.Data.Context;

namespace PontoSavi.Infra.Data.Repositories;

public class RoleRepository : BaseRepository<IdentityRole>, IRoleRepository
{
    private readonly RoleManager<IdentityRole> _roleManager;

    public RoleRepository(AppDbContext context, RoleManager<IdentityRole> roleManager) : base(context) =>
        _roleManager = roleManager;

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