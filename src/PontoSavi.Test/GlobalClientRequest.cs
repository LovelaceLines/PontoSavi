using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

using PontoSavi.API.InputModels;
using PontoSavi.Domain.DTOs;
using PontoSavi.Test.Fakers;
using PontoSavi.Test.Utils;

[assembly: CollectionBehavior(CollectionBehavior.CollectionPerAssembly)]

namespace PontoSavi.Test.Global;

public class GlobalClientRequest : HttpClientUtil
{
    public const string BaseUrl = "http://localhost:5178/api/";
    public readonly HttpClient _authClient = new() { BaseAddress = new Uri($"{BaseUrl}Auth/") };
    public readonly HttpClient _loginClient = new() { BaseAddress = new Uri($"{BaseUrl}Auth/login") };
    public readonly HttpClient _refreshTokenClient = new() { BaseAddress = new Uri($"{BaseUrl}Auth/refresh-token") };
    public readonly HttpClient _authUserClient = new() { BaseAddress = new Uri($"{BaseUrl}Auth/user") };
    public readonly HttpClient _userClient = new() { BaseAddress = new Uri($"{BaseUrl}User/") };
    public readonly HttpClient _userPasswordClient = new() { BaseAddress = new Uri($"{BaseUrl}User/password/") };
    public readonly HttpClient _addUserToRoleClient = new() { BaseAddress = new Uri($"{BaseUrl}User/add-to-role/") };
    public readonly HttpClient _removeUserFromRoleClient = new() { BaseAddress = new Uri($"{BaseUrl}User/remove-from-role/") };
    public readonly HttpClient _roleClient = new() { BaseAddress = new Uri($"{BaseUrl}Role/") };

    #region GetEntityFake

    public async Task<UserToken> GetToken(string? userName = null, string? password = null)
    {
        LoginIM login = new();

        if (!userName.IsNullOrEmpty() && !password.IsNullOrEmpty())
        {
            login = new LoginIM { UserName = userName!, Password = password! };
        }
        else
        {
            var user = await GetUser();
            login = new LoginIM { UserName = user.UserName, Password = user.Password };
        }

        return await PostFromBody<UserToken>(_loginClient, login);
    }

    public async Task<UserDTO> GetUser(string? id = null, string? password = null, UserDTO? fake = null)
    {
        if (!id.IsNullOrEmpty())
        {
            var user = await GetFromUri<UserDTO>(_userClient, id!);
            user.Password = password ?? user.Password;
            return user;
        }

        fake ??= new UserFake().Generate();

        var userPosted = await PostFromBody<UserDTO>(_userClient, fake);
        userPosted.Password = fake.Password;

        return userPosted;
    }

    public async Task<RoleDTO> GetRole(string? id = null)
    {
        if (!id.IsNullOrEmpty())
            return await GetFromUri<RoleDTO>(_roleClient, id!);

        var roleFake = new RoleFake(id).Generate();
        return await PostFromBody<RoleDTO>(_roleClient, roleFake);
    }

    public async Task<UserRoleIM> GetUserRole(string? userId = null, string? roleId = null)
    {
        var user = await GetUser(id: userId);
        var role = await GetRole(id: roleId);
        var model = new UserRoleIM { UserId = user.Id!, RoleName = role.Name };

        await PostFromBody<UserDTO>(_addUserToRoleClient, model);

        return model;
    }

    #endregion
}
