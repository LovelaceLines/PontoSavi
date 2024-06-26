using System.Net;

using PontoSavi.Application.Interfaces;
using PontoSavi.Domain.Constants;
using PontoSavi.Domain.Exceptions;
using PontoSavi.Domain.Repositories;
using PontoSavi.Domain.Filters;
using PontoSavi.Domain.Entities;

namespace PontoSavi.Application.Services;

public class RoleService : IRoleService
{
    private readonly IRoleRepository _repository;
    private readonly IRolesSettingsService _rolesSettingsService;

    public RoleService(IRoleRepository repository,
        IRolesSettingsService rolesSettingsService)
    {
        _repository = repository;
        _rolesSettingsService = rolesSettingsService;
    }

    public async Task<QueryResult<Role>> Query(RoleFilter filter) =>
        await _repository.Query(filter);

    public async Task<Role> GetById(int id, int tenantId) =>
        await _repository.GetById(id, tenantId);

    public async Task<Role> GetByName(string name, int tenantId) =>
        await _repository.GetByName(name, tenantId);

    public async Task<List<Role>> GetByUser(int userId, int tenantId) =>
        await _repository.GetByUser(userId, tenantId);

    public async Task<Role> Create(Role role)
    {
        if (await _repository.ExistsByName(role.Name, role.TenantId))
            throw new AppException("Função já existe!", HttpStatusCode.Conflict);

        return await _repository.Add(role);
    }

    public async Task<Role> Update(Role newRole)
    {
        if (_rolesSettingsService.IsStandardUser(newRole.Name))
            throw new AppException("Não é possível alterar uma função padrão!", HttpStatusCode.BadRequest);

        if (await _repository.ExistsByName(newRole.Name, newRole.TenantId))
            throw new AppException("Função já existe!", HttpStatusCode.Conflict);

        var oldRole = await _repository.GetById(newRole.Id);

        oldRole.Name = newRole.Name;

        return await _repository.Update(oldRole);
    }

    public async Task<Role> Delete(Role role)
    {
        if (_rolesSettingsService.IsStandardUser(role.Name))
            throw new AppException("Não é possível excluir uma função padrão!", HttpStatusCode.BadRequest);

        return await _repository.Remove(role);
    }
}