using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using PontoSavi.Application.Interfaces;
using PontoSavi.Domain.Filters;
using PontoSavi.Domain.Entities;
using PontoSavi.API.ServiceFilters;

namespace PontoSavi.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[ServiceFilter(typeof(AuthAndUserExtractionFilter))]
public class RoleController : ControllerBase
{
    private readonly IRoleService _service;

    public RoleController(IRoleService service) =>
        _service = service;

    /// <summary>
    /// Queries roles by filter.
    /// </summary>
    [HttpGet]
    [Authorize(Policy = "SuperUserRolesPolicy")]
    public async Task<ActionResult<QueryResult<Role>>> Query([FromQuery] RoleFilter filter)
    {
        var currentCompanyId = (int)HttpContext.Items["CurrentCompanyId"]!;
        filter.CompanyId = currentCompanyId;
        return await _service.Query(filter);
    }

    /// <summary>
    /// Gets a role by its public id.
    /// </summary>
    [HttpGet("{id}")]
    [Authorize(Policy = "SuperUserRolesPolicy")]
    public async Task<ActionResult<Role>> GetById(int id)
    {
        var currentCompanyId = (int)HttpContext.Items["CurrentCompanyId"]!;
        return await _service.GetById(id, currentCompanyId);
    }

    /// <summary>
    /// Creates a new role.
    /// </summary>
    [HttpPost]
    [Authorize(Policy = "SuperUserRolesPolicy")]
    public async Task<ActionResult<Role>> Post([FromBody] Role role)
    {
        var currentCompanyId = (int)HttpContext.Items["CurrentCompanyId"]!;
        role.CompanyId = currentCompanyId;
        return await _service.Create(role);
    }

    /// <summary>
    /// Updates an existing role.
    /// </summary>
    [HttpPut]
    [Authorize(Policy = "SuperUserRolesPolicy")]
    public async Task<ActionResult<Role>> Put([FromBody] Role role)
    {
        var currentCompanyId = (int)HttpContext.Items["CurrentCompanyId"]!;
        role.CompanyId = currentCompanyId;
        return await _service.Update(role);
    }

    /// <summary>
    /// Deletes a role by its public id.
    /// </summary>
    [HttpDelete("{id}")]
    [Authorize(Policy = "AdminUserRolesPolicy")]
    public async Task<ActionResult<Role>> Delete(int id)
    {
        var currentCompanyId = (int)HttpContext.Items["CurrentCompanyId"]!;
        var role = await _service.GetById(id, currentCompanyId);
        return await _service.Delete(role);
    }
}