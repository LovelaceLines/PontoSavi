using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using PontoSavi.Application.Interfaces;
using PontoSavi.Domain.DTOs;
using PontoSavi.Domain.Filters;

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
    [Authorize(Roles = "Desenvolvedor,Administrador,Supervisor")]
    public async Task<IActionResult> Query([FromQuery] RoleFilter filter) =>
        Ok(await _roleService.Query(filter));

    [HttpGet("{id}")]
    [Authorize(Roles = "Desenvolvedor,Administrador,Supervisor")]
    public async Task<IActionResult> GetById(string id) =>
        Ok(await _roleService.GetById(id));

    /// <summary>
    /// Creates a new role.
    /// </summary>
    [HttpPost]
    [Authorize(Roles = "Desenvolvedor,Administrador,Supervisor")]
    public async Task<IActionResult> Post([FromBody] RoleDTO role)
    {
        var identityRole = _mapper.Map<IdentityRole>(role);
        var roleCreated = await _roleService.Create(identityRole);
        return Ok(_mapper.Map<RoleDTO>(roleCreated));
    }

    /// <summary>
    /// Updates an existing role.
    /// </summary>
    [HttpPut]
    [Authorize(Roles = "Desenvolvedor,Administrador,Supervisor")]
    public async Task<IActionResult> Put([FromBody] RoleDTO role)
    {
        var identityRole = _mapper.Map<IdentityRole>(role);
        var roleUpdated = await _roleService.Update(identityRole);
        return Ok(_mapper.Map<RoleDTO>(roleUpdated));
    }

    /// <summary>
    /// Deletes a role by its ID.
    /// </summary>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Desenvolvedor,Administrador")]
    public async Task<IActionResult> Delete(string id)
    {
        await _roleService.Delete(id);
        return Ok();
    }
}