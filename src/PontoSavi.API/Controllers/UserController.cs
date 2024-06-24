using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Net;

using PontoSavi.API.InputModels;
using PontoSavi.API.ServiceFilters;
using PontoSavi.Application.Interfaces;
using PontoSavi.Domain.DTOs;
using PontoSavi.Domain.Filters;
using PontoSavi.Domain.Entities;
using PontoSavi.Domain.Exceptions;
using PontoSavi.Domain.Constants;

namespace PontoSavi.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[ServiceFilter(typeof(AuthAndUserExtractionFilter))]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IUserWorkShiftService _userWorkShiftService;
    private readonly IRoleService _roleService;
    private readonly IRolesSettingsService _rolesSettingsService;
    private readonly IUserRoleService _userRoleService;
    private readonly IWorkShiftService _workShiftService;

    public UserController(IUserService userService,
        IUserWorkShiftService userWorkShiftService,
        IRoleService roleService,
        IRolesSettingsService rolesSettingsService,
        IUserRoleService userRoleService,
        IWorkShiftService workShiftService)
    {
        _userService = userService;
        _userWorkShiftService = userWorkShiftService;
        _roleService = roleService;
        _rolesSettingsService = rolesSettingsService;
        _userRoleService = userRoleService;
        _workShiftService = workShiftService;
    }

    /// <summary>
    /// Retrieves a list of users based on the specified filter.
    /// </summary>
    [HttpGet]
    [Authorize(Policy = "SuperUserRolesPolicy")]
    public async Task<ActionResult<QueryResult<UserDTO>>> Query([FromQuery] UserFilter filter)
    {
        var currentCompanyId = (int)HttpContext.Items["CurrentCompanyId"]!;
        filter.CompanyId = currentCompanyId;
        return Ok(await _userService.Query(filter));
    }

    /// <summary>
    /// Gets a user by ID.
    /// </summary>
    [HttpGet("{id}")]
    [Authorize(Policy = "SuperUserRolesPolicy")]
    public async Task<ActionResult<UserDTO>> GetById(int id)
    {
        var currentCompanyId = (int)HttpContext.Items["CurrentCompanyId"]!;
        var user = await _userService.GetById(id, currentCompanyId);
        var roles = await _roleService.GetByUser(user.Id, user.CompanyId);

        return new UserDTO(user, roles);
    }

    /// <summary>
    /// Creates a new user.
    /// </summary>
    [HttpPost]
    [Authorize(Policy = "SuperUserRolesPolicy")]
    public async Task<ActionResult<User>> Post([FromBody] UserAndPassword userAndPassword)
    {
        var currentCompanyId = (int)HttpContext.Items["CurrentCompanyId"]!;

        User user = userAndPassword;
        user.CompanyId = currentCompanyId;
        user = await _userService.Create(user, userAndPassword.Password);

        foreach (var roleName in _rolesSettingsService.GetBaseUserRoles())
        {
            var role = await _roleService.GetByName(roleName, user.CompanyId);
            await _userRoleService.AddToRole(user, role);
        }

        return user;
    }

    /// <summary>
    /// Updates a user.
    /// </summary>
    [HttpPut]
    public async Task<ActionResult<User>> Put([FromBody] User user)
    {
        var currentUserId = (int)HttpContext.Items["CurrentUserId"]!;

        if (user.Id != currentUserId) throw new AppException("Você não tem permissão para alterar este usuário!", HttpStatusCode.Forbidden);

        var currentCompanyId = (int)HttpContext.Items["CurrentCompanyId"]!;
        user.CompanyId = currentCompanyId;

        return await _userService.Update(user);
    }

    /// <summary>
    /// Updates the password of the current user.
    /// </summary>
    [HttpPut("password")]
    public async Task<ActionResult<User>> Put([FromBody] UpdatePasswordIM model)
    {
        var currentUserId = (int)HttpContext.Items["CurrentUserId"]!;
        var currentCompanyId = (int)HttpContext.Items["CurrentCompanyId"]!;
        var user = await _userService.GetById(currentUserId, currentCompanyId);
        await _userService.UpdatePassword(user, model.OldPassword, model.NewPassword);
        return user;
    }

    /// <summary>
    /// Deletes a user by ID.
    /// </summary>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Desenvolvedor,Administrador")]
    public async Task<ActionResult<User>> Delete(int id)
    {
        var currentCompanyId = (int)HttpContext.Items["CurrentCompanyId"]!;
        var user = await _userService.GetById(id, currentCompanyId);
        return await _userService.Delete(user);
    }

    /// <summary>
    /// Adds a user to a role.
    /// </summary>
    [HttpPost("add-to-role")]
    [Authorize(Policy = "SuperUserRolesPolicy")]
    public async Task<ActionResult<bool>> AddToRole([FromBody] UserRoleIM model)
    {
        var currentCompanyId = (int)HttpContext.Items["CurrentCompanyId"]!;
        var user = await _userService.GetById(model.UserId, currentCompanyId);
        var role = await _roleService.GetById(model.RoleId, currentCompanyId);

        await _userRoleService.AddToRole(user, role);

        return Ok();
    }

    /// <summary>
    /// Removes a user from a role.
    /// </summary>
    [HttpDelete("remove-from-role")]
    [Authorize(Policy = "SuperUserRolesPolicy")]
    public async Task<ActionResult<bool>> RemoveFromRole([FromBody] UserRoleIM model)
    {
        var currentCompanyId = (int)HttpContext.Items["CurrentCompanyId"]!;
        var user = await _userService.GetById(model.UserId, currentCompanyId);
        var role = await _roleService.GetById(model.RoleId, currentCompanyId);

        await _userRoleService.RemoveFromRole(user, role);

        return Ok();
    }

    [HttpPost("add-work-shift")]
    [Authorize(Policy = "SuperUserRolesPolicy")]
    public async Task<ActionResult<UserWorkShift>> AddWorkShift([FromBody] UserWorkShift userWorkShift)
    {
        var currentCompanyId = (int)HttpContext.Items["CurrentCompanyId"]!;
        userWorkShift.CompanyId = currentCompanyId;
        return await _userWorkShiftService.Create(userWorkShift);
    }

    [HttpDelete("remove-work-shift")]
    [Authorize(Policy = "SuperUserRolesPolicy")]
    public async Task<ActionResult<UserWorkShift>> RemoveWorkShift([FromBody] UserWorkShift userWorkShift)
    {
        var currentCompanyId = (int)HttpContext.Items["CurrentCompanyId"]!;
        userWorkShift.CompanyId = currentCompanyId;
        return await _userWorkShiftService.Delete(userWorkShift);
    }
}