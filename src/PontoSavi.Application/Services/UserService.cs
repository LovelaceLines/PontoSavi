using System.Net;

using PontoSavi.Application.Interfaces;
using PontoSavi.Application.Validators;
using PontoSavi.Domain.DTOs;
using PontoSavi.Domain.Exceptions;
using PontoSavi.Domain.Filters;
using PontoSavi.Domain.Repositories;
using PontoSavi.Domain.Entities;

namespace PontoSavi.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly UserValidator _userValidator;
    private readonly PasswordValidator _passwordValidator;

    public UserService(IUserRepository userRepository,
        UserValidator userValidator,
        PasswordValidator passwordValidator)
    {
        _userRepository = userRepository;
        _userValidator = userValidator;
        _passwordValidator = passwordValidator;
    }

    public async Task<QueryResult<UserDTO>> Query(UserFilter filter) =>
        await _userRepository.Query(filter);

    public async Task<User> GetById(int id, int companyId) =>
        await _userRepository.GetById(id, companyId);

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

        if (await _userRepository.ExistsByUserName(user.UserName))
            throw new AppException("Nome de usuário já existe!", HttpStatusCode.Conflict);

        // if (await _userRepository.ExistsByEmail(user.Email))
        //     throw new AppException("Email já existe!", HttpStatusCode.Conflict);

        // if (await _userRepository.ExistsByPhoneNumber(user.PhoneNumber))
        //     throw new AppException("Número de telefone já existe!", HttpStatusCode.Conflict);

        return await _userRepository.Add(user, password);
    }

    public async Task<User> Update(User newUser)
    {
        var validationResult = _userValidator.Validate(newUser);
        if (!validationResult.IsValid)
            throw new AppException(validationResult.Errors.First().ErrorMessage, HttpStatusCode.BadRequest);

        var oldUser = await _userRepository.GetById(newUser.Id);

        if (oldUser.UserName != newUser.UserName && await _userRepository.ExistsByUserName(newUser.UserName))
            throw new AppException("Nome de usuário já existe!", HttpStatusCode.Conflict);

        // if (oldUser.Email != newUser.Email && await _userRepository.ExistsByEmail(newUser.Email))
        //     throw new AppException("Email já existe!", HttpStatusCode.Conflict);

        // if (oldUser.PhoneNumber != newUser.PhoneNumber && await _userRepository.ExistsByPhoneNumber(newUser.PhoneNumber))
        //     throw new AppException("Telefone já existe!", HttpStatusCode.Conflict);

        oldUser.Name = newUser.Name;
        oldUser.UserName = newUser.UserName;
        oldUser.Email = newUser.Email;
        oldUser.PhoneNumber = newUser.PhoneNumber;

        return await _userRepository.Update(oldUser);
    }

    public async Task<bool> UpdatePassword(User user, string oldPassword, string newPassword)
    {
        var validationResult = _passwordValidator.Validate(newPassword);
        if (!validationResult.IsValid)
            throw new AppException(validationResult.Errors.First().ErrorMessage, HttpStatusCode.BadRequest);

        return await _userRepository.UpdatePassword(user, oldPassword, newPassword);
    }

    public async Task<User> Delete(User user) =>
        await _userRepository.Remove(user);
}