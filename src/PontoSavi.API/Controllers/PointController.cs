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
public class PointController : ControllerBase
{
    private readonly IPointService _service;

    public PointController(IPointService pointService) =>
        _service = pointService;

    [HttpGet]
    [Authorize(Policy = "SuperUserRolesPolicy")]
    public async Task<ActionResult<QueryResult<Point>>> Get([FromQuery] PointFilter filter)
    {
        var currentTenantId = (int)HttpContext.Items["CurrentTenantId"]!;
        filter.TenantId = currentTenantId;
        return await _service.Query(filter);
    }

    [HttpGet("{id}")]
    [Authorize(Policy = "BaseUserRolesPolicy")]
    public async Task<ActionResult<Point>> GetById(int id)
    {
        var currentTenantId = (int)HttpContext.Items["CurrentTenantId"]!;
        return await _service.GetById(id, currentTenantId);
    }

    [HttpGet("current")]
    [Authorize(Policy = "BaseUserRolesPolicy")]
    public async Task<ActionResult<Point>> GetCurrent()
    {
        var currentUserId = (int)HttpContext.Items["CurrentUserId"]!;
        var currentTenantId = (int)HttpContext.Items["CurrentTenantId"]!;
        return await _service.GetOpenPoint(currentUserId, currentTenantId);
    }

    [HttpPost("auto")]
    [Authorize(Policy = "BaseUserRolesPolicy")]
    public async Task<ActionResult<Point>> AutoCheckIn([FromBody] string? description)
    {
        var currentUserId = (int)HttpContext.Items["CurrentUserId"]!;
        var currentTenantId = (int)HttpContext.Items["CurrentTenantId"]!;
        return await _service.AutoCheckIn(currentUserId, currentTenantId, description);
    }

    [HttpPut("auto")]
    [Authorize(Policy = "BaseUserRolesPolicy")]
    public async Task<ActionResult<Point>> AutoCheckOut([FromBody] string? description)
    {
        var currentUserId = (int)HttpContext.Items["CurrentUserId"]!;
        var currentTenantId = (int)HttpContext.Items["CurrentTenantId"]!;
        return await _service.AutoCheckOut(currentUserId, currentTenantId, description);
    }

    [HttpPost("manual")]
    [Authorize(Policy = "BaseUserRolesPolicy")]
    public async Task<ActionResult<Point>> ManualCheckIn([FromBody] Point point)
    {
        var currentUserId = (int)HttpContext.Items["CurrentUserId"]!;
        var currentTenantId = (int)HttpContext.Items["CurrentTenantId"]!;
        point.UserId = currentUserId;
        point.TenantId = currentTenantId;
        return await _service.ManualCheckIn(point);
    }

    [HttpPut("manual")]
    [Authorize(Policy = "BaseUserRolesPolicy")]
    public async Task<ActionResult<Point>> ManualCheckOut([FromBody] Point point)
    {
        var currentUserId = (int)HttpContext.Items["CurrentUserId"]!;
        var currentTenantId = (int)HttpContext.Items["CurrentTenantId"]!;
        point.UserId = currentUserId;
        point.TenantId = currentTenantId;
        return await _service.ManualCheckOut(point);
    }

    [HttpPut]
    [Authorize(Policy = "BaseUserRolesPolicy")]
    public async Task<ActionResult<Point>> UpdateDescription([FromBody] Point point)
    {
        var currentUserId = (int)HttpContext.Items["CurrentUserId"]!;
        var currentTenantId = (int)HttpContext.Items["CurrentTenantId"]!;
        point.UserId = currentUserId;
        point.TenantId = currentTenantId;
        return await _service.UpdateDescription(point);
    }

    [HttpPut("full")]
    [Authorize(Policy = "SuperUserRolesPolicy")]
    public async Task<ActionResult<Point>> UpdateFull([FromBody] Point point)
    {
        var currentUserId = (int)HttpContext.Items["CurrentUserId"]!;
        var currentTenantId = (int)HttpContext.Items["CurrentTenantId"]!;
        point.ManagerId = currentUserId;
        point.TenantId = currentTenantId;
        return await _service.UpdateFull(point);
    }

    [HttpPost("approve")]
    [Authorize(Policy = "SuperUserRolesPolicy")]
    public async Task<ActionResult<Point>> Approve([FromBody] int id)
    {
        var currentTenantId = (int)HttpContext.Items["CurrentTenantId"]!;
        var currentManagerUserId = (int)HttpContext.Items["CurrentUserId"]!;
        return await _service.Approve(id, currentManagerUserId, currentTenantId);
    }

    [HttpPost("reject")]
    [Authorize(Policy = "SuperUserRolesPolicy")]
    public async Task<ActionResult<Point>> Reject([FromBody] int id)
    {
        var currentTenantId = (int)HttpContext.Items["CurrentTenantId"]!;
        var currentManagerUserId = (int)HttpContext.Items["CurrentUserId"]!;
        return await _service.Reject(id, currentManagerUserId, currentTenantId);
    }
}
