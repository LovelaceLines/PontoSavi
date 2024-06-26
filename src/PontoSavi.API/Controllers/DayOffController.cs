using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using PontoSavi.API.ServiceFilters;
using PontoSavi.Application.Interfaces;
using PontoSavi.Domain.Entities;
using PontoSavi.Domain.Filters;

namespace PontoSavi.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[ServiceFilter(typeof(AuthAndUserExtractionFilter))]
public class DayOffController : ControllerBase
{
    private readonly IDayOffService _dayOffService;

    public DayOffController(IDayOffService dayOffService) =>
        _dayOffService = dayOffService;

    [HttpGet]
    public async Task<ActionResult<QueryResult<DayOff>>> Query([FromQuery] DayOffFilter filter)
    {
        var currentTenantId = (int)HttpContext.Items["CurrentTenantId"]!;
        filter.TenantId = currentTenantId;
        return await _dayOffService.Query(filter);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DayOff>> GetById(int id)
    {
        var currentTenantId = (int)HttpContext.Items["CurrentTenantId"]!;
        return await _dayOffService.GetById(id, currentTenantId);
    }

    [HttpPost]
    [Authorize(Policy = "SuperUserRolesPolicy")]
    public async Task<ActionResult<DayOff>> Post(DayOff dayOff)
    {
        var currentTenantId = (int)HttpContext.Items["CurrentTenantId"]!;
        dayOff.TenantId = currentTenantId;
        return await _dayOffService.Create(dayOff);
    }

    [HttpPut]
    [Authorize(Policy = "SuperUserRolesPolicy")]
    public async Task<ActionResult<DayOff>> Put(DayOff dayOff)
    {
        var currentTenantId = (int)HttpContext.Items["CurrentTenantId"]!;
        dayOff.TenantId = currentTenantId;
        return await _dayOffService.Update(dayOff);
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "SuperUserRolesPolicy")]
    public async Task<ActionResult<DayOff>> Delete(int id)
    {
        var currentTenantId = (int)HttpContext.Items["CurrentTenantId"]!;
        var dayOff = await _dayOffService.GetById(id, currentTenantId);
        return await _dayOffService.Delete(dayOff);
    }
}
