using System.Net;

using PontoSavi.Application.Interfaces;
using PontoSavi.Application.Validators;
using PontoSavi.Domain.Constants;
using PontoSavi.Domain.DTOs;
using PontoSavi.Domain.Exceptions;
using PontoSavi.Domain.Filters;
using PontoSavi.Domain.Repositories;
using PontoSavi.Domain.Entities;

namespace PontoSavi.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IUserRoleRepository _userRoleRepository;
    private readonly UserValidator _userValidator;
    private readonly PasswordValidator _passwordValidator;
    private readonly IRolesSettingsService _rolesSettingsService;

    public UserService(IUserRepository userRepository,
        IUserRoleRepository userRoleRepository,
        UserValidator userValidator,
        PasswordValidator passwordValidator,
        IRolesSettingsService rolesSettingsService)
    {
        _userRepository = userRepository;
        _userRoleRepository = userRoleRepository;
        _userValidator = userValidator;
        _passwordValidator = passwordValidator;
        _rolesSettingsService = rolesSettingsService;
    }

    public async Task<QueryResult<UserDTO>> Query(UserFilter filter) =>
        await _userRepository.Query(filter);

    public async Task<User> GetByPublicId(string publicId) =>
        await _userRepository.GetByPublicId(publicId);

    public async Task<User> GetByUserName(string userName) =>
        await _userRepository.GetByUserName(userName);

    public async Task<User> Create(User user, string password)
    {
        var validationResult = _userValidator.Validate(user);
        if (!validationResult.IsValid)
            throw new AppException(validationResult.Errors.First().ErrorMessage, HttpStatusCode.BadRequest);

        validationResult = _passwordValidator.Validate(password);
        if (!validationResult.IsValid)
            throw new AppException(validationResult.Errors.First().ErrorMessage, HttpStatusCode.BadRequest);

        if (await _userRepository.ExistsByUserName(user.UserName!))
            throw new AppException("Nome de usuário já existe", HttpStatusCode.Conflict);

        // if (await _userRepository.ExistsByEmail(user.Email!))
        //     throw new AppException("Email já existe", HttpStatusCode.Conflict);

        // if (await _userRepository.ExistsByPhoneNumber(user.PhoneNumber!))
        //     throw new AppException("Número de telefone já existe", HttpStatusCode.Conflict);

        var newUser = await _userRepository.Add(user, password);

        foreach (var role in _rolesSettingsService.GetBaseUserRoles())
            await _userRoleRepository.Add(newUser, role);

        return newUser;
    }

    public async Task<User> Update(User newUser)
    {
        if (!await _userRepository.ExistsByPublicId(newUser.PublicId))
            throw new AppException("Usuário não encontrado", HttpStatusCode.NotFound);

        var oldUser = await _userRepository.GetByPublicId(newUser.PublicId);

        if (oldUser.PublicId != newUser.PublicId)
            throw new AppException("Você não tem permissão para alterar este usuário", HttpStatusCode.Forbidden);

        if (oldUser.UserName != newUser.UserName && await _userRepository.ExistsByUserName(newUser.UserName!))
            throw new AppException("Nome de usuário já existe!", HttpStatusCode.Conflict);

        // if (oldUser.Email != newUser.Email && await _userRepository.ExistsByEmail(newUser.Email!))
        //     throw new AppException("Email já existe!", HttpStatusCode.Conflict);

        // if (oldUser.PhoneNumber != newUser.PhoneNumber && await _userRepository.ExistsByPhoneNumber(newUser.PhoneNumber!))
        //     throw new AppException("Telefone já existe!", HttpStatusCode.Conflict);

        oldUser.Name = newUser.Name;
        oldUser.UserName = newUser.UserName;
        oldUser.Email = newUser.Email;
        oldUser.PhoneNumber = newUser.PhoneNumber;

        return await _userRepository.Update(oldUser);
    }

    public async Task<bool> UpdatePassword(int id, string oldPassword, string newPassword)
    {
        var validationResult = _passwordValidator.Validate(newPassword);
        if (!validationResult.IsValid)
            throw new AppException(validationResult.Errors.First().ErrorMessage, HttpStatusCode.BadRequest);

        return await _userRepository.UpdatePassword(id, oldPassword, newPassword);
    }

    public async Task<User> Delete(string publicId)
    {
        if (!await _userRepository.ExistsByPublicId(publicId))
            throw new AppException("Usuário não encontrado", HttpStatusCode.NotFound);

        var user = await _userRepository.GetByPublicId(publicId);

        return await _userRepository.Remove(user);
    }
}