using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using PontoSavi.API.InputModels;
using PontoSavi.Application.Interfaces;
using PontoSavi.Domain.Constants;
using PontoSavi.Domain.Entities;
using PontoSavi.Domain.Filters;

namespace PontoSavi.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Policy = "CEOUserRolesPolicy")]
public class CEOController : ControllerBase
{
    private readonly ICompanyService _companyService;
    private readonly IUserService _userService;
    private readonly IRoleService _roleService;
    private readonly IRolesSettingsService _rolesSettingsService;
    private readonly IUserRoleService _userRoleService;

    public CEOController(ICompanyService companyService,
        IUserService userService,
        IRoleService roleService,
        IRolesSettingsService rolesSettingsService,
        IUserRoleService userRoleService)
    {
        _userService = userService;
        _companyService = companyService;
        _roleService = roleService;
        _rolesSettingsService = rolesSettingsService;
        _userRoleService = userRoleService;
    }

    [HttpGet("company")]
    public async Task<ActionResult<QueryResult<Company>>> Query([FromQuery] CompanyFilter filter) =>
        await _companyService.Query(filter);

    [HttpGet("company/{id}")]
    public async Task<ActionResult<Company>> GetById(int id) =>
        await _companyService.GetById(id);

    [HttpPost("company")]
    public async Task<ActionResult<CompanyAndUser>> Post([FromBody] CompanyAndUser companyIM)
    {
        var company = await _companyService.Create(companyIM.Company);

        var roles = new List<Role>();

        foreach (var roleName in _rolesSettingsService.GetStandardUserRoles())
        {
            var role = new Role(roleName) { CompanyId = company.Id };
            role = await _roleService.Create(role);
            roles.Add(role);
        }

        companyIM.User.CompanyId = company.Id;
        var user = await _userService.Create(companyIM.User, companyIM.User.Password);

        foreach (var role in roles)
            await _userRoleService.AddToRole(user, role);

        return new CompanyAndUser(company, user);
    }

    [HttpPost("user")]
    public async Task<ActionResult<User>> PostUserByCeo([FromBody] UserAndPassword userAndPassword)
    {
        User user = userAndPassword;
        return await _userService.Create(user, userAndPassword.Password);
    }

    [HttpPost("user/add-to-role")]
    public async Task<ActionResult<bool>> AddToRole([FromBody] UserRoleIM model)
    {
        var user = await _userService.GetById(model.UserId, model.CompanyId);
        var role = await _roleService.GetById(model.RoleId, model.CompanyId);

        await _userRoleService.AddToRole(user, role);

        return Ok();
    }

    [HttpPost("role")]
    public async Task<ActionResult<Role>> PostRole([FromBody] Role role)
    {
        return await _roleService.Create(role);
    }
}
