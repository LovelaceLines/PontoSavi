using System.Net;

using PontoSavi.Domain.Entities;
using PontoSavi.Test.DTOs;
using PontoSavi.Test.Fakers;
using PontoSavi.Test.Global;

namespace PontoSavi.Test.API;

public class PointControllerTest : GlobalClientRequest
{
    [Fact]
    public async Task Post_ValidAutoPoint_ReturnsOk()
    {
        var user = await GetUser();
        var token = await GetToken(user.UserName, user.Password);
        _accessToken = token.AuthToken.AccessToken;

        var point = new PointFake().Generate();

        var response = await PostFromBody<Point>(_pointAutoClient, point.CheckInDescription!);

        Assert.Equal(user.Id, response.UserId);
        Assert.Equal(point.CheckInDescription, response.CheckInDescription);
        Assert.Equal(DateTime.Now.Hour, response.CheckIn.Hour);
        Assert.Equal(DateTime.Now.Minute, response.CheckIn.Minute);
        Assert.Null(response.ManagerId);
    }

    [Fact]
    public async Task Post_ValidManualPoint_ReturnsOk()
    {
        var user = await GetUser();
        var token = await GetToken(user.UserName, user.Password);
        _accessToken = token.AuthToken.AccessToken;

        var point = new PointFake().Generate();

        var response = await PostFromBody<Point>(_pointManualClient, point);

        Assert.Equal(user.Id, response.UserId);
        Assert.Equal(point.CheckInDescription, response.CheckInDescription);
        Assert.Equal(point.CheckIn, response.CheckIn);
        Assert.Null(response.ManagerId);
    }

    [Fact]
    public async Task Post_InvalidPoint_ReturnsBadRequest()
    {
        var user = await GetUser();
        var token = await GetToken(user.UserName, user.Password);
        _accessToken = token.AuthToken.AccessToken;

        var point = await GetOpenPoint(user: user);

        var resultAuto = await PostFromBody<AppHttpResponse>(_pointAutoClient, point.CheckInDescription!);
        var resultManual = await PostFromBody<AppHttpResponse>(_pointManualClient, point);

        Assert.Equal(HttpStatusCode.BadRequest, resultAuto.StatusCode);
        Assert.Equal(HttpStatusCode.BadRequest, resultManual.StatusCode);
    }


    [Fact]
    public async Task Put_InvalidPoint_ReturnsBadRequest()
    {
        var user = await GetUser();
        var token = await GetToken(user.UserName, user.Password);
        _accessToken = token.AuthToken.AccessToken;

        var point = new PointFake().Generate();

        var resultAuto = await PutFromBody<AppHttpResponse>(_pointAutoClient, point.CheckOutDescription!);
        var resultManual = await PutFromBody<AppHttpResponse>(_pointManualClient, point);

        Assert.Equal(HttpStatusCode.BadRequest, resultAuto.StatusCode);
        Assert.Equal(HttpStatusCode.BadRequest, resultManual.StatusCode);
    }

    [Fact]
    public async Task Put_ValidAutoPoint_ReturnsOk()
    {
        var user = await GetUser();
        var token = await GetToken(user.UserName, user.Password);
        _accessToken = token.AuthToken.AccessToken;

        var point = await GetOpenPoint(user: user);
        point.CheckOutDescription = new PointFake().Generate().CheckOutDescription;

        var response = await PutFromBody<Point>(_pointAutoClient, point.CheckOutDescription!);

        Assert.Equal(point.CheckInDescription, response.CheckInDescription);
    }

    [Fact]
    public async Task Put_ValidManualPoint_ReturnsOk()
    {
        var user = await GetUser();
        var token = await GetToken(user.UserName, user.Password);
        _accessToken = token.AuthToken.AccessToken;

        var point = await GetOpenPoint(user: user);
        point = new PointFake(id: point.Id, userId: point.UserId).Generate();

        var response = await PutFromBody<Point>(_pointManualClient, point);

        Assert.Equal(point.CheckOutDescription, response.CheckOutDescription);
    }

    [Fact]
    public async Task PostApprove_ValidPoint_ReturnsOk()
    {
        var point = await GetClosedPoint();

        var user = await GetUser();
        var userRole = await GetUserRole(userId: user.Id, roleId: 2);
        var token = await GetToken(user.UserName, user.Password);
        _accessToken = token.AuthToken.AccessToken;

        var response = await PostFromBody<Point>(_pointApproveClient, point.Id);

        Assert.Equal(PointStatus.Approved, response.CheckInStatus);
        Assert.Equal(PointStatus.Approved, response.CheckOutStatus);
    }

    [Fact]
    public async Task PostReject_ValidPoint_ReturnsOk()
    {
        var point = await GetClosedPoint();

        var user = await GetUser();
        var userRole = await GetUserRole(userId: user.Id, roleId: 2);
        var token = await GetToken(user.UserName, user.Password);
        _accessToken = token.AuthToken.AccessToken;

        var response = await PostFromBody<Point>(_pointRejectClient, point.Id);

        Assert.Equal(PointStatus.Rejected, response.CheckInStatus);
        Assert.Equal(PointStatus.Rejected, response.CheckOutStatus);
    }
}
