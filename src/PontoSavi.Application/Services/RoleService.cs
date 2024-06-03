using System.Net;

using PontoSavi.Application.Interfaces;
using PontoSavi.Domain.DTOs;
using PontoSavi.Domain.Exceptions;
using PontoSavi.Domain.Repositories;
using PontoSavi.Domain.Filters;
using PontoSavi.Domain.Entities;

namespace PontoSavi.Application.Services;

public class RoleService : IRoleService
{
    private readonly IRoleRepository _roleRepository;

    public RoleService(IRoleRepository roleRepository) =>
        _roleRepository = roleRepository;

    public async Task<QueryResult<RoleDTO>> Query(RoleFilter filter) =>
        await _roleRepository.Query(filter);

    public async Task<RoleDTO> GetById(string id) =>
        new RoleDTO(await _roleRepository.GetById(id));

    public async Task<Role> Create(Role role)
    {
        if (await _roleRepository.ExistsByName(role.Name!))
            throw new AppException("Função já existe", HttpStatusCode.Conflict);

        return await _roleRepository.Add(role);
    }

    public async Task<Role> Update(Role role)
    {
        // TODO: Definy the method implementation
        // if (_roleRepository.IsDefault(role.Name!))
        //     throw new AppException("Não é possível alterar uma função padrão", HttpStatusCode.BadRequest);

        if (!await _roleRepository.ExistsById(role.Id))
            throw new AppException("Função não encontrada", HttpStatusCode.NotFound);

        if (await _roleRepository.ExistsByName(role.Name!))
            throw new AppException("Função já existe", HttpStatusCode.Conflict);

        return await _roleRepository.Update(role);
    }

    public async Task<bool> Delete(string id)
    {
        if (!await _roleRepository.ExistsById(id))
            throw new AppException("Função não encontrada", HttpStatusCode.NotFound);

        var role = await _roleRepository.GetById(id);

        // TODO: Definy the method implementation
        // if (_roleRepository.IsDefault(role.Name!))
        //     throw new AppException("Não é possível excluir uma função padrão", HttpStatusCode.BadRequest);

        await _roleRepository.Remove(role);

        return true;
    }
}