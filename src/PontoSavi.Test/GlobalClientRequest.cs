﻿using Microsoft.IdentityModel.Tokens;

using PontoSavi.API.InputModels;
using PontoSavi.Domain.DTOs;
using PontoSavi.Domain.Entities;
using PontoSavi.Test.DTOs;
using PontoSavi.Test.Fakers;
using PontoSavi.Test.Utils;

using User = PontoSavi.Test.Fakers.User;

[assembly: CollectionBehavior(CollectionBehavior.CollectionPerAssembly)]

namespace PontoSavi.Test.Global;

public class GlobalClientRequest : HttpClientUtil
{
    public const string BaseUrl = "http://localhost:5178/api/";
    public readonly HttpClient _authClient = new() { BaseAddress = new Uri($"{BaseUrl}Auth/") };
    public readonly HttpClient _loginClient = new() { BaseAddress = new Uri($"{BaseUrl}Auth/login/") };
    public readonly HttpClient _refreshTokenClient = new() { BaseAddress = new Uri($"{BaseUrl}Auth/refresh-token/") };
    public readonly HttpClient _authUserClient = new() { BaseAddress = new Uri($"{BaseUrl}Auth/user/") };
    public readonly HttpClient _CEOClient = new() { BaseAddress = new Uri($"{BaseUrl}CEO/") };
    public readonly HttpClient _CEOCompanyClient = new() { BaseAddress = new Uri($"{BaseUrl}CEO/company/") };
    public readonly HttpClient _CEOUserClient = new() { BaseAddress = new Uri($"{BaseUrl}CEO/user/") };
    public readonly HttpClient _CEOUserAddToRoleClient = new() { BaseAddress = new Uri($"{BaseUrl}CEO/user/add-to-role/") };
    public readonly HttpClient _CEORoleClient = new() { BaseAddress = new Uri($"{BaseUrl}CEO/role/") };
    public readonly HttpClient _userClient = new() { BaseAddress = new Uri($"{BaseUrl}User/") };
    public readonly HttpClient _userPasswordClient = new() { BaseAddress = new Uri($"{BaseUrl}User/password/") };
    public readonly HttpClient _addUserToRoleClient = new() { BaseAddress = new Uri($"{BaseUrl}User/add-to-role/") };
    public readonly HttpClient _removeUserFromRoleClient = new() { BaseAddress = new Uri($"{BaseUrl}User/remove-from-role/") };
    public readonly HttpClient _userAddWorkShiftClient = new() { BaseAddress = new Uri($"{BaseUrl}User/add-work-shift/") };
    public readonly HttpClient _userRemoveWorkShiftClient = new() { BaseAddress = new Uri($"{BaseUrl}User/remove-work-shift/") };
    public readonly HttpClient _roleClient = new() { BaseAddress = new Uri($"{BaseUrl}Role/") };
    public readonly HttpClient _companyClient = new() { BaseAddress = new Uri($"{BaseUrl}Company/") };
    public readonly HttpClient _companyAddWorkShiftClient = new() { BaseAddress = new Uri($"{BaseUrl}Company/add-work-shift/") };
    public readonly HttpClient _companyRemoveWorkShiftClient = new() { BaseAddress = new Uri($"{BaseUrl}Company/remove-work-shift/") };
    public readonly HttpClient _dayOffClient = new() { BaseAddress = new Uri($"{BaseUrl}DayOff/") };
    public readonly HttpClient _pointClient = new() { BaseAddress = new Uri($"{BaseUrl}Point/") };
    public readonly HttpClient _pointAutoClient = new() { BaseAddress = new Uri($"{BaseUrl}Point/auto/") };
    public readonly HttpClient _pointManualClient = new() { BaseAddress = new Uri($"{BaseUrl}Point/manual/") };
    public readonly HttpClient _pointFullClient = new() { BaseAddress = new Uri($"{BaseUrl}Point/full/") };
    public readonly HttpClient _pointApproveClient = new() { BaseAddress = new Uri($"{BaseUrl}Point/approve/") };
    public readonly HttpClient _pointRejectClient = new() { BaseAddress = new Uri($"{BaseUrl}Point/reject/") };
    public readonly HttpClient _workShiftClient = new() { BaseAddress = new Uri($"{BaseUrl}WorkShift/") };

    #region GetEntityFake

    public async Task<UserToken> GetToken()
    {
        var user = await GetUser();
        return await GetToken(user.UserName, user.Password);
    }

    public async Task<UserToken> GetToken(string userName, string password)
    {
        var login = new LoginIM { UserName = userName, Password = password };
        return await PostFromBody<UserToken>(_loginClient, login);
    }

    public async Task<User> GetUser(User? fake = null)
    {
        fake ??= new UserFake().Generate();
        return await PostFromBody<User>(_userClient, fake);
    }

    public async Task<User> GetUserByCEO(User? fake = null)
    {
        fake ??= new UserFake().Generate();
        return await PostFromBody<User>(_CEOUserClient, fake);
    }

    public async Task<Role> GetRole(Role? fake = null)
    {
        fake ??= new RoleFake().Generate();
        return await PostFromBody<Role>(_roleClient, fake);
    }

    public async Task<Role> GetRoleByCEO(Role? fake = null)
    {
        fake ??= new RoleFake().Generate();
        return await PostFromBody<Role>(_CEORoleClient, fake);
    }

    public async Task<UserRoleIM> GetUserRole()
    {
        var user = await GetUser();
        var role = await GetRole();

        return await GetUserRole(user.Id, role.Id);
    }

    public async Task<UserRoleIM> GetUserRole(int userId, int roleId)
    {
        var model = new UserRoleIM { UserId = userId, RoleId = roleId };

        await PostFromBody<AppHttpResponse>(_addUserToRoleClient, model);

        return model;
    }

    public async Task<Company> GetCompany(CompanyAndUser? fake = null)
    {
        fake ??= new CompanyAndUserFake().Generate();
        var companyAndUser = await PostFromBody<CompanyAndUser>(_CEOCompanyClient, fake);
        return companyAndUser.Company;
    }

    public async Task<DayOff> GetDayOff(DayOff? fake = null)
    {
        fake ??= new DayOffFake().Generate();
        return await PostFromBody<DayOff>(_dayOffClient, fake);
    }

    public async Task<Point> GetOpenPoint(Point? pointFake = null, User? user = null)
    {
        user ??= await GetUser();
        var token = await GetToken(user.UserName, user.Password);
        _accessToken = token.AuthToken.AccessToken;

        pointFake ??= new PointFake(userId: user.Id).Generate();
        return await PostFromBody<Point>(_pointAutoClient, pointFake.CheckInDescription!);
    }

    public async Task<Point> GetClosedPoint(Point? pointFake = null, User? user = null)
    {
        user ??= await GetUser();
        var token = await GetToken(user.UserName, user.Password);
        _accessToken = token.AuthToken.AccessToken;

        var openPoint = await GetOpenPoint(pointFake, user);
        openPoint = new PointFake(id: openPoint.Id, userId: openPoint.UserId, checkIn: openPoint.CheckIn, checkInDescription: openPoint.CheckInDescription).Generate();
        return await PutFromBody<Point>(_pointManualClient, openPoint);
    }

    public async Task<WorkShift> GetWorkShift(WorkShift? fake = null)
    {
        fake ??= new WorkShiftFake().Generate();
        return await PostFromBody<WorkShift>(_workShiftClient, fake);
    }

    public async Task<CompanyWorkShift> GetCompanyWorkShift(CompanyWorkShift? fake = null)
    {
        if (fake is null)
        {
            var workShift = await GetWorkShift();
            fake = new CompanyWorkShift { WorkShiftId = workShift.Id };
        }

        return await PostFromBody<CompanyWorkShift>(_companyAddWorkShiftClient, fake);
    }

    public async Task<UserWorkShift> GetUserWorkShift(UserWorkShift? fake = null)
    {
        if (fake is null)
        {
            var user = await GetUser();
            var workShift = await GetWorkShift();
            fake = new UserWorkShift { UserId = user.Id, WorkShiftId = workShift.Id };
        }

        return await PostFromBody<UserWorkShift>(_userAddWorkShiftClient, fake);
    }

    #endregion
}
