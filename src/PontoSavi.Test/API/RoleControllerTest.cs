using System.Net;

using PontoSavi.Domain.DTOs;
using PontoSavi.Domain.Exceptions;
using PontoSavi.Domain.Filters;
using PontoSavi.Test.DTOs;
using PontoSavi.Test.Fakers;
using PontoSavi.Test.Global;

namespace PontoSavi.Test.API;

public class RoleControllerTest : GlobalClientRequest
{
    [Fact]
    public async Task Get_QueryById_ReturnsRole()
    {
        var role = await GetRole();

        var result = await GetFromQuery<QueryResult<RoleDTO>>(_roleClient, new RoleFilter { PublicId = role.PublicId });
        var roleGet = result.Items.Single();

        Assert.Equivalent(role.PublicId, roleGet.PublicId);
        Assert.Equivalent(role.Name, roleGet.Name);
    }

    [Fact]
    public async Task Post_ValidRole_ReturnsOkResult()
    {
        var role = new RoleFake().Generate();

        var result = await PostFromBody<RoleDTO>(_roleClient, role);

        Assert.Equivalent(role.Name, result.Name);
    }

    [Fact]
    public async Task Post_InvalidRoleWithExistingName_ReturnsConflictResult()
    {
        var role = await GetRole();
        var roleWithExistingName = new RoleFake(name: role.Name).Generate();

        var resultWithExistingName = await PostFromBody<AppException>(_roleClient, roleWithExistingName);

        Assert.Equal(HttpStatusCode.Conflict, resultWithExistingName.StatusCode);
    }

    [Fact]
    public async Task Put_ValidRole_ReturnsOkResult()
    {
        var role = await GetRole();
        var updatedRole = new RoleFake(publicId: role.PublicId).Generate();

        var result = await PutFromBody<RoleDTO>(_roleClient, updatedRole);

        Assert.Equivalent(updatedRole.PublicId, result.PublicId);
        Assert.Equivalent(updatedRole.Name, result.Name);
    }

    [Fact]
    public async Task Put_InvalidRoleWithNonExistingId_ReturnsNotFoundResult()
    {
        var role = new RoleFake(publicId: Guid.NewGuid().ToString()).Generate();

        var result = await PutFromBody<AppException>(_roleClient, role);

        Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
    }

    [Fact]
    public async Task Put_InvalidRoleWithExistingName_ReturnsConflictResult()
    {
        var role1 = await GetRole();
        var role2 = await GetRole();
        var roleWithExistingName = new RoleFake(publicId: role1.PublicId, name: role2.Name).Generate();

        var resultWithExistingName = await PutFromBody<AppException>(_roleClient, roleWithExistingName);

        Assert.Equal(HttpStatusCode.Conflict, resultWithExistingName.StatusCode);
    }

    [Fact]
    public async Task Delete_ValidRole_ReturnsOkResult()
    {
        var role = await GetRole();

        var result = await DeleteFromUri<AppHttpResponse>(_roleClient, role.PublicId!);

        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
    }

    [Fact]
    public async Task Delete_InvalidRole_ReturnsNotFoundResult()
    {
        var result = await DeleteFromUri<AppException>(_roleClient, Guid.NewGuid().ToString());

        Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
    }
}
