using Microsoft.AspNetCore.Identity;
using System.Net;

using PontoSavi.Application.Interfaces;
using PontoSavi.Domain.Exceptions;
using PontoSavi.Domain.Entities;
using PontoSavi.Domain.Repositories;

namespace PontoSavi.Application.Services;

public class UserRoleService : IUserRoleService
{
    private readonly IUserRoleRepository _repository;

    public UserRoleService(IUserRoleRepository repository) =>
        _repository = repository;

    public async Task<bool> AddToRole(User user, Role role)
    {
        if (await _repository.Exists(user.Id, role.Id, user.CompanyId))
            throw new AppException("Usuário já está nesta função!", HttpStatusCode.BadRequest);

        await _repository.Add(user.Id, role.Id, user.CompanyId);

        return true;
    }

    public async Task<bool> RemoveFromRole(User user, Role role)
    {
        if (!await _repository.Exists(user.Id, role.Id, user.CompanyId))
            throw new AppException("Usuário não está nesta função!", HttpStatusCode.BadRequest);

        var userRole = await _repository.Get(user.Id, role.Id, user.CompanyId);

        await _repository.Remove(userRole);

        return true;
    }
}