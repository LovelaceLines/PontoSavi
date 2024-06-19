using System.Net;

using PontoSavi.API.InputModels;
using PontoSavi.Domain.DTOs;
using PontoSavi.Test.DTOs;
using PontoSavi.Test.Global;

namespace PontoSavi.Test.API;

public class AuthControllerTest : GlobalClientRequest
{
    [Fact]
    public async Task Post_Login_Desenvolvedor_ReturnsToken()
    {
        var login = new LoginIM { UserName = "dev", Password = "!23L6(bNi.22T71,%4vfR{<~tA.]" };

        var res = await PostFromBody<UserToken>(_loginClient, login);

        Assert.NotNull(res.AuthToken.AccessToken);
        Assert.NotNull(res.AuthToken.RefreshToken);
    }

    [Fact]
    public async Task Post_Login_ValidLogin_ReturnsToken()
    {
        var user = await GetUser();
        var login = new LoginIM { UserName = user.UserName, Password = user.Password };

        var res = await PostFromBody<UserToken>(_loginClient, login);

        Assert.NotNull(res.AuthToken.AccessToken);
        Assert.NotNull(res.AuthToken.RefreshToken);
        Assert.Equal(user.UserName, res.User.UserName);
    }

    [Fact]
    public async Task Post_Login_InvalidUserName_ReturnsUnauthorized()
    {
        var user = await GetUser();
        var login = new LoginIM { UserName = new Bogus.Faker().Internet.UserName(), Password = user.Password };

        var res = await PostFromBody<AppHttpResponse>(_loginClient, login);

        Assert.Equal(HttpStatusCode.Unauthorized, res.StatusCode);
    }

    [Fact]
    public async Task Post_Login_InvalidPassword_ReturnsUnauthorized()
    {
        var user = await GetUser();
        var login = new LoginIM { UserName = user.UserName, Password = new Bogus.Faker().Internet.Password() };

        var res = await PostFromBody<AppHttpResponse>(_loginClient, login);

        Assert.Equal(HttpStatusCode.Unauthorized, res.StatusCode);
    }

    [Fact]
    public async Task Get_RefreshToken_ValidToken_ReturnsToken()
    {
        var token = await GetToken();
        _accessToken = token.AuthToken.RefreshToken;

        var res = await Get<AuthToken>(_refreshTokenClient);

        Assert.NotNull(res.AccessToken);
        Assert.NotNull(res.RefreshToken);
    }

    [Fact]
    public async Task Get_RefreshToken_InvalidToken_ReturnsBadRequest()
    {
        _accessToken = new Bogus.Faker().Internet.Password();

        var res = await Get<AppHttpResponse>(_refreshTokenClient);

        Assert.Equal(HttpStatusCode.Unauthorized, res.StatusCode);
    }

    [Fact]
    public async Task Get_User_ValidToken_ReturnsUser()
    {
        var token = await GetToken();
        _accessToken = token.AuthToken.AccessToken;

        var res = await Get<UserDTO>(_authUserClient);

        Assert.NotNull(res.UserName);
        Assert.NotNull(res.Email);
        Assert.NotNull(res.PhoneNumber);
        Assert.NotNull(res.Roles);
    }

    [Fact]
    public async Task Get_User_InvalidToken_ReturnsUnauthorized()
    {
        _accessToken = new Bogus.Faker().Internet.Password();

        await Assert.ThrowsAsync<Exception>(() => Get<UserDTO>(_authUserClient));
    }
}