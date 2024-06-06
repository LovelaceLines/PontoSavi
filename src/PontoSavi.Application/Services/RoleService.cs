using System.Net;

using PontoSavi.Application.Interfaces;
using PontoSavi.Domain.Constants;
using PontoSavi.Domain.DTOs;
using PontoSavi.Domain.Exceptions;
using PontoSavi.Domain.Repositories;
using PontoSavi.Domain.Filters;
using PontoSavi.Domain.Entities;

namespace PontoSavi.Application.Services;

public class RoleService : IRoleService
{
    private readonly IRoleRepository _roleRepository;
    private readonly IRolesSettingsService _rolesSettingsService;

    public RoleService(IRoleRepository roleRepository,
        IRolesSettingsService rolesSettingsService)
    {
        _roleRepository = roleRepository;
        _rolesSettingsService = rolesSettingsService;
    }

    public async Task<QueryResult<RoleDTO>> Query(RoleFilter filter) =>
        await _roleRepository.Query(filter);

    public async Task<Role> GetByPublicId(string publicId) =>
        await _roleRepository.GetByPublicId(publicId);

    public async Task<List<Role>> GetByUser(User user) =>
        await _roleRepository.GetByUser(user);

    public async Task<Role> Create(Role role)
    {
        if (await _roleRepository.ExistsByName(role.Name!))
            throw new AppException("Função já existe", HttpStatusCode.Conflict);

        return await _roleRepository.Add(role);
    }

    public async Task<Role> Update(Role newRole)
    {
        if (_rolesSettingsService.IsStandardUser(newRole.Name!))
            throw new AppException("Não é possível alterar uma função padrão", HttpStatusCode.BadRequest);

        if (!await _roleRepository.ExistsByPublicId(newRole.PublicId))
            throw new AppException("Função não encontrada", HttpStatusCode.NotFound);

        if (await _roleRepository.ExistsByName(newRole.Name!))
            throw new AppException("Função já existe", HttpStatusCode.Conflict);

        var oldRole = await _roleRepository.GetByPublicId(newRole.PublicId);

        oldRole.Name = newRole.Name;

        return await _roleRepository.Update(oldRole);
    }

    public async Task<Role> Delete(string publicId)
    {
        if (!await _roleRepository.ExistsByPublicId(publicId))
            throw new AppException("Função não encontrada", HttpStatusCode.NotFound);

        var role = await _roleRepository.GetByPublicId(publicId);

        if (_rolesSettingsService.IsStandardUser(role.Name!))
            throw new AppException("Não é possível excluir uma função padrão", HttpStatusCode.BadRequest);

        return await _roleRepository.Remove(role);
    }
}