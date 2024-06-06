using AutoMapper;
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

namespace PontoSavi.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[ServiceFilter(typeof(AuthAndUserExtractionFilter))]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IRoleService _roleService;
    private readonly IUserRoleService _userRoleService;
    private readonly IMapper _mapper;

    public UserController(IUserService userService,
        IRoleService roleService,
        IUserRoleService userRoleService,
        IMapper mapper)
    {
        _userService = userService;
        _roleService = roleService;
        _userRoleService = userRoleService;
        _mapper = mapper;
    }

    /// <summary>
    /// Retrieves a list of users based on the specified filter.
    /// </summary>
    [HttpGet]
    [Authorize(Policy = "SuperUserRolesPolicy")]
    public async Task<ActionResult<QueryResult<UserDTO>>> Query([FromQuery] UserFilter filter) =>
        Ok(await _userService.Query(filter));

    /// <summary>
    /// Gets a user by ID.
    /// </summary>
    [HttpGet("{publicId}")]
    [Authorize(Policy = "SuperUserRolesPolicy")]
    public async Task<ActionResult<UserDTO>> GetByPublicId(string publicId)
    {
        var user = await _userService.GetByPublicId(publicId);
        var roles = await _roleService.GetByUser(user);
        return Ok(new UserDTO(user, roles));
    }

    /// <summary>
    /// Gets a user by username.
    /// </summary>
    [HttpGet("username/{userName}")]
    [Authorize(Policy = "SuperUserRolesPolicy")]
    public async Task<ActionResult<UserDTO>> Get(string userName)
    {
        var user = await _userService.GetByUserName(userName);
        var roles = await _roleService.GetByUser(user);
        return Ok(new UserDTO(user, roles));
    }

    /// <summary>
    /// Creates a new user.
    /// </summary>
    [HttpPost]
    [Authorize(Policy = "SuperUserRolesPolicy")]
    public async Task<ActionResult<UserDTO>> Post([FromBody] UserDTO userDTO)
    {
        var user = _mapper.Map<User>(userDTO);
        user = await _userService.Create(user, userDTO.Password);
        return Ok(new UserDTO(user));
    }

    /// <summary>
    /// Updates a user.
    /// </summary>
    [HttpPut]
    public async Task<ActionResult<UserDTO>> Put([FromBody] UserDTO userDTO)
    {
        var currentUserPublicId = (string)HttpContext.Items["CurrentUserPublicId"]!;

        if (userDTO.PublicId != currentUserPublicId)
            throw new AppException("Você não tem permissão para alterar este usuário", HttpStatusCode.Forbidden);

        var user = _mapper.Map<User>(userDTO);
        user = await _userService.Update(user);
        return Ok(new UserDTO(user));
    }

    /// <summary>
    /// Updates the password of the current user.
    /// </summary>
    [HttpPut("password")]
    public async Task<ActionResult<UserDTO>> Put([FromBody] UpdatePasswordIM model)
    {
        var currentUserPublicId = (string)HttpContext.Items["CurrentUserPublicId"]!;
        var user = await _userService.GetByPublicId(currentUserPublicId);
        await _userService.UpdatePassword(user.Id, model.OldPassword, model.NewPassword);
        return Ok(new UserDTO(user));
    }

    /// <summary>
    /// Deletes a user by ID.
    /// </summary>
    [HttpDelete("{publicId}")]
    [Authorize(Roles = "Desenvolvedor,Administrador")]
    public async Task<ActionResult<UserDTO>> Delete(string publicId)
    {
        var user = await _userService.Delete(publicId);
        return Ok(new UserDTO(user));
    }

    /// <summary>
    /// Adds a user to a role.
    /// </summary>
    [HttpPost("add-to-role")]
    [Authorize(Policy = "SuperUserRolesPolicy")]
    public async Task<ActionResult<UserDTO>> AddToRole([FromBody] UserRoleIM model) =>
        Ok(await _userRoleService.AddToRole(model.UserId, model.RoleName));

    /// <summary>
    /// Removes a user from a role.
    /// </summary>
    [HttpDelete("remove-from-role")]
    [Authorize(Policy = "SuperUserRolesPolicy")]
    public async Task<ActionResult<UserDTO>> RemoveFromRole([FromBody] UserRoleIM model) =>
        Ok(await _userRoleService.RemoveFromRole(model.UserId, model.RoleName));
}