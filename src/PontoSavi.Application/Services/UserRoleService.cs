using Microsoft.AspNetCore.Identity;
using System.Net;

using PontoSavi.Application.Interfaces;
using PontoSavi.Domain.DTOs;
using PontoSavi.Domain.Exceptions;
using PontoSavi.Domain.Repositories;
using PontoSavi.Domain.Entities;

namespace PontoSavi.Application.Services;

public class UserRoleService : IUserRoleService
{
    private readonly UserManager<User> _userManager;
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;

    public UserRoleService(UserManager<User> userManager,
        IUserRepository userRepository,
        IRoleRepository roleRepository)
    {
        _userManager = userManager;
        _userRepository = userRepository;
        _roleRepository = roleRepository;
    }

    public async Task<UserDTO> AddToRole(string userPublicId, string roleName)
    {
        if (!await _userRepository.ExistsByPublicId(userPublicId))
            throw new AppException("Usuário não encontrado", HttpStatusCode.NotFound);

        if (!await _roleRepository.ExistsByName(roleName))
            throw new AppException("Função não encontrada", HttpStatusCode.NotFound);

        var user = await _userRepository.GetByPublicId(userPublicId);
        var role = await _roleRepository.GetByName(roleName);

        if (await _userManager.IsInRoleAsync(user, role.Name!))
            throw new AppException("Usuário já está nesta função", HttpStatusCode.BadRequest);

        var result = await _userManager.AddToRoleAsync(user, role.Name!);

        if (!result.Succeeded)
            throw new AppException(result.Errors.First().Description, HttpStatusCode.BadRequest);

        return new UserDTO(user, await _userRepository.GetRoles(user));
    }

    public async Task<UserDTO> RemoveFromRole(string userPublicId, string roleName)
    {
        if (!await _userRepository.ExistsByPublicId(userPublicId))
            throw new AppException("Usuário não encontrado", HttpStatusCode.NotFound);

        if (!await _roleRepository.ExistsByName(roleName))
            throw new AppException("Função não encontrada", HttpStatusCode.NotFound);

        var user = await _userRepository.GetByPublicId(userPublicId);
        var role = await _roleRepository.GetByName(roleName);

        if (!await _userManager.IsInRoleAsync(user, role.Name!))
            throw new AppException("Usuário não está nesta função", HttpStatusCode.BadRequest);

        var result = await _userManager.RemoveFromRoleAsync(user, role.Name!);

        if (!result.Succeeded)
            throw new AppException(result.Errors.First().Description, HttpStatusCode.BadRequest);

        return new UserDTO(user, await _userRepository.GetRoles(user));
    }
}