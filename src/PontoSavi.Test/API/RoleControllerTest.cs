using System.Net;

using PontoSavi.Domain.Entities;
using PontoSavi.Domain.Filters;
using PontoSavi.Test.DTOs;
using PontoSavi.Test.Fakers;
using PontoSavi.Test.Global;

namespace PontoSavi.Test.API;

public class RoleControllerTest : GlobalClientRequest
{
    [Fact]
    public async Task Get_Query_ReturnsOk()
    {
        var role = await GetRole();
        var filter = new RoleFilter
        {
            Id = role.Id,
            Name = role.Name
        };

        var result = await GetFromQuery<QueryResult<Role>>(_roleClient, filter);
        var item = result.Items.Single();

        Assert.Equal(role.Id, item.Id);
        Assert.Equal(role.Name, item.Name);
    }

    [Fact]
    public async Task Get_GetById_ReturnsOk()
    {
        var role = await GetRole();

        var result = await GetFromUri<Role>(_roleClient, role.Id);

        Assert.Equal(role.Id, result.Id);
        Assert.Equal(role.Name, result.Name);
    }

    [Fact]
    public async Task Post_ValidRole_ReturnsOk()
    {
        var role = new RoleFake().Generate();

        var result = await PostFromBody<Role>(_roleClient, role);

        Assert.Equal(role.Name, result.Name);
    }

    [Fact]
    public async Task Post_InvalidRoleWithExistingName_ReturnsConflictResult()
    {
        var role = await GetRole();
        var fake = new RoleFake(name: role.Name).Generate();

        var result = await PostFromBody<AppHttpResponse>(_roleClient, fake);

        Assert.Equal(HttpStatusCode.Conflict, result.StatusCode);
    }

    [Fact]
    public async Task Put_ValidRole_ReturnsOkResult()
    {
        var role = await GetRole();
        var fake = new RoleFake(id: role.Id).Generate();

        var result = await PutFromBody<Role>(_roleClient, fake);

        Assert.Equal(fake.Id, result.Id);
        Assert.Equal(fake.Name, result.Name);
    }

    [Fact]
    public async Task Put_InvalidRoleWithExistingName_ReturnsConflictResult()
    {
        var role1 = await GetRole();
        var role2 = await GetRole();
        var fake = new RoleFake(id: role1.Id, name: role2.Name).Generate();

        var result = await PutFromBody<AppHttpResponse>(_roleClient, fake);

        Assert.Equal(HttpStatusCode.Conflict, result.StatusCode);
    }

    [Fact]
    public async Task Delete_ValidRole_ReturnsOkResult()
    {
        var role = await GetRole();

        var result = await DeleteFromUri<AppHttpResponse>(_roleClient, role.Id);

        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
    }
}
