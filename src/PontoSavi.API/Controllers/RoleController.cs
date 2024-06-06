using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using PontoSavi.Application.Interfaces;
using PontoSavi.Domain.DTOs;
using PontoSavi.Domain.Filters;
using PontoSavi.Domain.Entities;

namespace PontoSavi.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RoleController : ControllerBase
{
    private readonly IRoleService _roleService;
    private readonly IMapper _mapper;

    public RoleController(IRoleService roleService, IMapper mapper)
    {
        _roleService = roleService;
        _mapper = mapper;
    }

    /// <summary>
    /// Queries roles by filter.
    /// </summary>
    [HttpGet]
    [Authorize(Policy = "SuperUserRolesPolicy")]
    public async Task<ActionResult<QueryResult<RoleDTO>>> Query([FromQuery] RoleFilter filter) =>
        Ok(await _roleService.Query(filter));

    /// <summary>
    /// Gets a role by its public id.
    /// </summary>
    [HttpGet("{publicId}")]
    [Authorize(Policy = "SuperUserRolesPolicy")]
    public async Task<ActionResult<RoleDTO>> GetByPublicId(string publicId) =>
        Ok(await _roleService.GetByPublicId(publicId));

    /// <summary>
    /// Creates a new role.
    /// </summary>
    [HttpPost]
    [Authorize(Policy = "SuperUserRolesPolicy")]
    public async Task<ActionResult<RoleDTO>> Post([FromBody] RoleDTO roleDTO)
    {
        var role = _mapper.Map<Role>(roleDTO);
        role = await _roleService.Create(role);
        return Ok(new RoleDTO(role));
    }

    /// <summary>
    /// Updates an existing role.
    /// </summary>
    [HttpPut]
    [Authorize(Policy = "SuperUserRolesPolicy")]
    public async Task<ActionResult<RoleDTO>> Put([FromBody] RoleDTO roleDTO)
    {
        var role = _mapper.Map<Role>(roleDTO);
        role = await _roleService.Update(role);
        return Ok(new RoleDTO(role));
    }

    /// <summary>
    /// Deletes a role by its public id.
    /// </summary>
    [HttpDelete("{publicId}")]
    [Authorize(Policy = "AdminUserRolesPolicy")]
    public async Task<ActionResult<RoleDTO>> Delete(string publicId) =>
        Ok(new RoleDTO(await _roleService.Delete(publicId)));
}