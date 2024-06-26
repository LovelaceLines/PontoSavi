using Microsoft.AspNetCore.Mvc;

using PontoSavi.API.ServiceFilters;
using PontoSavi.Application.Interfaces;
using PontoSavi.Domain.DTOs;
using PontoSavi.Domain.Entities;
using PontoSavi.Domain.Filters;

namespace PontoSavi.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[ServiceFilter(typeof(AuthAndUserExtractionFilter))]

public class WorkShiftController : ControllerBase
{
    private readonly IWorkShiftService _service;

    public WorkShiftController(IWorkShiftService workShiftService) =>
        _service = workShiftService;

    [HttpGet]
    public async Task<ActionResult<QueryResult<WorkShiftDTO>>> Get([FromQuery] WorkShiftFilter filter)
    {
        var currentTenantId = (int)HttpContext.Items["CurrentTenantId"]!;
        filter.TenantId = currentTenantId;
        return await _service.Query(filter);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<WorkShift>> GetById(int id)
    {
        var currentTenantId = (int)HttpContext.Items["CurrentTenantId"]!;
        return await _service.GetById(id, currentTenantId);
    }

    [HttpPost]
    public async Task<ActionResult<WorkShift>> Post([FromBody] WorkShift workShift)
    {
        var currentTenantId = (int)HttpContext.Items["CurrentTenantId"]!;
        workShift.TenantId = currentTenantId;
        return await _service.Create(workShift);
    }

    [HttpPut]
    public async Task<ActionResult<WorkShift>> Put([FromBody] WorkShift workShift)
    {
        var currentTenantId = (int)HttpContext.Items["CurrentTenantId"]!;
        workShift.TenantId = currentTenantId;
        return await _service.Update(workShift);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<WorkShift>> Delete(int id)
    {
        var currentTenantId = (int)HttpContext.Items["CurrentTenantId"]!;
        var workShift = await _service.GetById(id, currentTenantId);
        return await _service.Delete(workShift);
    }
}
