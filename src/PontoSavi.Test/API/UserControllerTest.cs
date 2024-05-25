using System.Net;

using PontoSavi.API.InputModels;
using PontoSavi.Domain.DTOs;
using PontoSavi.Domain.Exceptions;
using PontoSavi.Test.Fakers;
using PontoSavi.Test.Global;

namespace PontoSavi.Test.API;

public class UserControllerTests : GlobalClientRequest
{
    [Fact]
    public async Task Post_ValidUser_ReturnsOkResult()
    {
        var user = new UserFake().Generate();

        var result = await PostFromBody<UserDTO>(_userClient, user);

        Assert.Equivalent(user.UserName, result.UserName);
        Assert.Equivalent(user.Email, result.Email);
        Assert.Equivalent(user.PhoneNumber, result.PhoneNumber);
        Assert.Equivalent(user.Roles, result.Roles);
    }

    [Fact]
    public async Task Post_InvalidUserWithExistingUserNameEmailOrPhoneNumber_ReturnsConflictResult()
    {
        var user = await GetUser();
        var userWithExistingUserName = new UserFake(userName: user.UserName).Generate();
        var userWithExistingEmail = new UserFake(email: user.Email).Generate();
        var userWithExistingPhoneNumber = new UserFake(phoneNumber: user.PhoneNumber).Generate();

        var resultWithExistingUserName = await PostFromBody<AppException>(_userClient, userWithExistingUserName);
        var resultWithExistingEmail = await PostFromBody<AppException>(_userClient, userWithExistingEmail);
        var resultWithExistingPhoneNumber = await PostFromBody<AppException>(_userClient, userWithExistingPhoneNumber);

        Assert.Equal(HttpStatusCode.Conflict, resultWithExistingUserName.StatusCode);
        Assert.Equal(HttpStatusCode.Conflict, resultWithExistingEmail.StatusCode);
        Assert.Equal(HttpStatusCode.Conflict, resultWithExistingPhoneNumber.StatusCode);
    }

    [Fact]
    public async Task Put_ValidUser_ReturnsOkResult()
    {
        var user = await GetUser();
        var token = await GetToken(userId: user.Id, userName: user.UserName, password: user.Password);
        _accessToken = token.AuthToken.AccessToken;
        var updatedUser = new UserFake(id: user.Id).Generate();

        var result = await PutFromBody<UserDTO>(_userClient, updatedUser);

        Assert.Equivalent(updatedUser.UserName, result.UserName);
        Assert.Equivalent(updatedUser.Email, result.Email);
        Assert.Equivalent(updatedUser.PhoneNumber, result.PhoneNumber);
        Assert.Equivalent(updatedUser.Roles, result.Roles);
    }

    [Fact]
    public async Task Put_InvalidUser_ReturnsNotFoundResult()
    {
        var user = new UserFake(id: Guid.NewGuid().ToString()).Generate();

        var result = await PutFromBody<AppException>(_userClient, user);

        Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
    }

    [Fact]
    public async Task Put_InvalidUserWithExistingUserNameEmailOrPhoneNumber_ReturnsConflictResult()
    {
        var user1 = await GetUser();
        var user2 = await GetUser();
        var token = await GetToken(userId: user1.Id, userName: user1.UserName, password: user1.Password);
        _accessToken = token.AuthToken.AccessToken;
        var userWithExistingUserName = new UserFake(id: user1.Id, userName: user2.UserName).Generate();
        var userWithExistingEmail = new UserFake(id: user1.Id, email: user2.Email).Generate();
        var userWithExistingPhoneNumber = new UserFake(id: user1.Id, phoneNumber: user2.PhoneNumber).Generate();

        var resultWithExistingUserName = await PutFromBody<AppException>(_userClient, userWithExistingUserName);
        var resultWithExistingEmail = await PutFromBody<AppException>(_userClient, userWithExistingEmail);
        var resultWithExistingPhoneNumber = await PutFromBody<AppException>(_userClient, userWithExistingPhoneNumber);

        Assert.Equal(HttpStatusCode.Conflict, resultWithExistingUserName.StatusCode);
        Assert.Equal(HttpStatusCode.Conflict, resultWithExistingEmail.StatusCode);
        Assert.Equal(HttpStatusCode.Conflict, resultWithExistingPhoneNumber.StatusCode);
    }

    [Fact]
    public async Task Put_Password_ValidUser_ReturnsOkResult()
    {
        var user = await GetUser();
        var token = await GetToken(userId: user.Id, userName: user.UserName, password: user.Password);
        _accessToken = token.AuthToken.AccessToken;
        var model = new UpdatePasswordIM { OldPassword = user.Password, NewPassword = new UserFake().Generate().Password };

        var result = await PutFromBody<UserDTO>(_userPasswordClient, model);

        Assert.Equal(user.Id, result.Id);
    }

    [Fact]
    public async Task Put_Password_InvalidOldPassword_ReturnsBadRequestResult()
    {
        var model = new UpdatePasswordIM { OldPassword = new UserFake().Generate().Password, NewPassword = new UserFake().Generate().Password };

        var result = await PutFromBody<AppException>(_userPasswordClient, model);

        Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
    }

    [Fact]
    public async Task Delete_ValidId_ReturnsOkResult()
    {
        var user = await GetUser();

        var result = await DeleteFromUri<UserDTO>(_userClient, user.Id!);

        Assert.Equal(user.Id, result.Id);
    }

    [Fact]
    public async Task Delete_InvalidId_ReturnsNotFoundResult()
    {
        var id = Guid.NewGuid().ToString();

        var result = await DeleteFromUri<AppException>(_userClient, id);

        Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
    }

    [Fact]
    public async Task Post_AddToRole_ValidUser_ReturnsOkResult()
    {
        var user = await GetUser();
        var role = await GetRole();
        var model = new UserRoleIM { UserId = user.Id!, RoleName = role.Name };

        var result = await PostFromBody<UserDTO>(_addUserToRoleClient, model);

        Assert.Contains(role.Name, result.Roles);
    }

    [Fact]
    public async Task Post_AddToRole_InvalidUser_ReturnsNotFoundResult()
    {
        var role = await GetRole();
        var userRole = new { UserId = Guid.NewGuid().ToString(), RoleName = role.Name };

        var result = await PostFromBody<AppException>(_addUserToRoleClient, userRole);

        Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
    }

    [Fact]
    public async Task Post_AddToRole_InvalidRole_ReturnsNotFoundResult()
    {
        var user = await GetUser();
        var userRole = new { UserId = user.Id, RoleName = new RoleFake().Generate().Name };

        var result = await PostFromBody<AppException>(_addUserToRoleClient, userRole);

        Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
    }

    [Fact]
    public async Task Post_AddToRole_RepeatedRole_ReturnsBadRequestResult()
    {
        var userRole = await GetUserRole();

        var result = await PostFromBody<AppException>(_addUserToRoleClient, userRole);

        Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
    }

    [Fact]
    public async Task Delete_RemoveFromRole_ValidUser_ReturnsOkResult()
    {
        var userRole = await GetUserRole();

        var result = await DeleteFromBody<UserDTO>(_removeUserFromRoleClient, userRole);

        Assert.DoesNotContain(userRole.RoleName, result.Roles);
    }

    [Fact]
    public async Task Delete_RemoveFromRole_InvalidUser_ReturnsNotFoundResult()
    {
        var role = await GetRole();
        var userRole = new UserRoleIM { UserId = Guid.NewGuid().ToString(), RoleName = role.Name };

        var result = await DeleteFromBody<AppException>(_removeUserFromRoleClient, userRole);

        Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
    }

    [Fact]
    public async Task Delete_RemoveFromRole_InvalidRole_ReturnsNotFoundResult()
    {
        var user = await GetUser();
        var userRole = new UserRoleIM { UserId = user.Id!, RoleName = new RoleFake().Generate().Name };

        var result = await DeleteFromBody<AppException>(_removeUserFromRoleClient, userRole);

        Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
    }

    [Fact]
    public async Task Delete_RemoveFromRole_NotInRole_ReturnsBadRequestResult()
    {
        var user = await GetUser();
        var role = await GetRole();
        var userRole = new UserRoleIM { UserId = user.Id!, RoleName = role.Name };

        var result = await DeleteFromBody<AppException>(_removeUserFromRoleClient, userRole);

        Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
    }
}