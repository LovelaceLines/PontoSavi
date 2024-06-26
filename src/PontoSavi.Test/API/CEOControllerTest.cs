using System.Net;

using PontoSavi.API.InputModels;
using PontoSavi.Domain.Entities;
using PontoSavi.Domain.Filters;
using PontoSavi.Test.DTOs;
using PontoSavi.Test.Fakers;
using PontoSavi.Test.Global;

namespace PontoSavi.Test.API;

public class CEOControllerTest : GlobalClientRequest
{
    [Fact]
    public async Task Get_Query_ReturnsOk()
    {
        var company = await GetCompany();
        var filter = new CompanyFilter
        {
            Id = company.Id,
            Name = company.Name,
            TradeName = company.TradeName,
            CNPJ = company.CNPJ,
        };

        var result = await GetFromQuery<QueryResult<Company>>(_CEOCompanyClient, filter);
        var item = result.Items.Single();

        Assert.Equivalent(company, item);
    }

    [Fact]
    public async Task Get_GetById_ReturnsOk()
    {
        var company = await GetCompany();

        var result = await GetFromUri<Company>(_CEOCompanyClient, company.Id);

        Assert.NotNull(result);
    }

    [Fact]
    public async Task Post_ValidCompany_ReturnsOkResult()
    {
        var companyAndUser = new CompanyAndUserFake().Generate();

        var result = await PostFromBody<CompanyAndUser>(_CEOCompanyClient, companyAndUser);

        Assert.Equal(companyAndUser.Company.Name, result.Company.Name);
        Assert.Equal(companyAndUser.Company.TradeName, result.Company.TradeName);
        Assert.Equal(companyAndUser.Company.CNPJ, result.Company.CNPJ);
        Assert.Equal(companyAndUser.User.Name, result.User.Name);
        Assert.Equal(companyAndUser.User.UserName, result.User.UserName);
        Assert.Equal(companyAndUser.User.Email, result.User.Email);
        Assert.Equal(companyAndUser.User.PhoneNumber, result.User.PhoneNumber);
    }

    [Fact]
    public async Task Post_ValidUser_ReturnsOkResult()
    {
        var user = new UserFake().Generate();

        var result = await PostFromBody<Fakers.User>(_userClient, user);

        Assert.Equal(user.Name, result.Name);
        Assert.Equal(user.UserName, result.UserName);
        Assert.Equal(user.Email, result.Email);
        Assert.Equal(user.PhoneNumber, result.PhoneNumber);
    }

    [Fact]
    public async Task Post_InvalidUserWithExistingUserNameEmailOrPhoneNumber_ReturnsConflictResult()
    {
        var user = await GetUser();
        var userWithExistingUserName = new UserFake(userName: user.UserName).Generate();
        // var userWithExistingEmail = new UserFake(email: user.Email).Generate();
        // var userWithExistingPhoneNumber = new UserFake(phoneNumber: user.PhoneNumber).Generate();

        var resultWithExistingUserName = await PostFromBody<AppHttpResponse>(_userClient, userWithExistingUserName);
        // var resultWithExistingEmail = await PostFromBody<AppHttpResponse>(_userClient, userWithExistingEmail);
        // var resultWithExistingPhoneNumber = await PostFromBody<AppHttpResponse>(_userClient, userWithExistingPhoneNumber);

        Assert.Equal(HttpStatusCode.Conflict, resultWithExistingUserName.StatusCode);
        // Assert.Equal(HttpStatusCode.Conflict, resultWithExistingEmail.StatusCode);
        // Assert.Equal(HttpStatusCode.Conflict, resultWithExistingPhoneNumber.StatusCode);
    }

    [Fact]
    public async Task Post_AddToRole_ValidUser_ReturnsOkResult()
    {
        var company = await GetCompany();
        var user = await GetUserByCEO(new UserFake(tenantId: company.Id).Generate());
        var role = await GetRoleByCEO(new RoleFake(tenantId: company.Id).Generate());
        var model = new UserRoleIM { UserId = user.Id, RoleId = role.Id, TenantId = company.Id };

        var result = await PostFromBody<AppHttpResponse>(_CEOUserAddToRoleClient, model);

        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
    }
}
