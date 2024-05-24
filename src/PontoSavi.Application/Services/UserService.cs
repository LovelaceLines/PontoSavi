using Microsoft.AspNetCore.Identity;
using System.Net;

using PontoSavi.Application.Interfaces;
using PontoSavi.Application.Validators;
using PontoSavi.Domain.DTOs;
using PontoSavi.Domain.Exceptions;
using PontoSavi.Domain.Repositories;

namespace PontoSavi.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IUserRoleRepository _userRoleRepository;
    private readonly UserValidator _userValidator;
    private readonly PasswordValidator _passwordValidator;

    public UserService(IUserRepository userRepository,
        IUserRoleRepository userRoleRepository,
        UserValidator userValidator,
        PasswordValidator passwordValidator)
    {
        _userRepository = userRepository;
        _userRoleRepository = userRoleRepository;
        _userValidator = userValidator;
        _passwordValidator = passwordValidator;
    }

    public async Task<IdentityUser> GetById(string id) =>
        await _userRepository.GetById(id);

    public async Task<UserDTO> GetByUserName(string userName)
    {
        var user = await _userRepository.GetByUserName(userName);
        var roles = await _userRoleRepository.GetRolesByUserId(user.Id);
        return new UserDTO(user, roles);
    }

    public async Task<IdentityUser> Create(IdentityUser user, string password)
    {
        var validationResult = _userValidator.Validate(user);
        if (!validationResult.IsValid)
            throw new AppException(validationResult.Errors.First().ErrorMessage, HttpStatusCode.BadRequest);

        validationResult = _passwordValidator.Validate(password);
        if (!validationResult.IsValid)
            throw new AppException(validationResult.Errors.First().ErrorMessage, HttpStatusCode.BadRequest);

        if (await _userRepository.ExistsByUserName(user.UserName!))
            throw new AppException("Nome de usuário já existe", HttpStatusCode.Conflict);

        if (await _userRepository.ExistsByEmail(user.Email!))
            throw new AppException("Email já existe", HttpStatusCode.Conflict);

        if (await _userRepository.ExistsByPhoneNumber(user.PhoneNumber!))
            throw new AppException("Número de telefone já existe", HttpStatusCode.Conflict);

        var newUser = await _userRepository.Add(user, password);
        // TODO - Add default role

        return newUser;
    }

    public async Task<IdentityUser> Update(IdentityUser newUser, string userId)
    {
        if (!await _userRepository.ExistsById(newUser.Id))
            throw new AppException("Usuário não encontrado", HttpStatusCode.NotFound);

        var oldUser = await _userRepository.GetById(newUser.Id);

        if (oldUser.Id != userId)
            throw new AppException("Você não tem permissão para alterar este usuário", HttpStatusCode.Forbidden);

        if (oldUser.UserName != newUser.UserName && await _userRepository.ExistsByUserName(newUser.UserName!))
            throw new AppException("Nome de usuário já existe!", HttpStatusCode.Conflict);

        if (oldUser.Email != newUser.Email && await _userRepository.ExistsByEmail(newUser.Email!))
            throw new AppException("Email já existe!", HttpStatusCode.Conflict);

        if (oldUser.PhoneNumber != newUser.PhoneNumber && await _userRepository.ExistsByPhoneNumber(newUser.PhoneNumber!))
            throw new AppException("Telefone já existe!", HttpStatusCode.Conflict);

        oldUser.UserName = newUser.UserName;
        oldUser.Email = newUser.Email;
        oldUser.PhoneNumber = newUser.PhoneNumber;

        return await _userRepository.Update(oldUser);
    }

    public async Task<bool> UpdatePassword(string userId, string oldPassword, string newPassword)
    {
        var validationResult = _passwordValidator.Validate(newPassword);
        if (!validationResult.IsValid)
            throw new AppException(validationResult.Errors.First().ErrorMessage, HttpStatusCode.BadRequest);

        return await _userRepository.UpdatePassword(userId, oldPassword, newPassword);
    }

    public async Task<IdentityUser> Delete(string id)
    {
        if (!await _userRepository.ExistsById(id))
            throw new AppException("Usuário não encontrado", HttpStatusCode.NotFound);

        return await _userRepository.RemoveById(id);
    }
}