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
        var currentCompanyId = (int)HttpContext.Items["CurrentCompanyId"]!;
        filter.CompanyId = currentCompanyId;
        return await _service.Query(filter);
    }

    [HttpGet("{id}")]
    [Authorize(Policy = "BaseUserRolesPolicy")]
    public async Task<ActionResult<Point>> GetById(int id)
    {
        var currentCompanyId = (int)HttpContext.Items["CurrentCompanyId"]!;
        return await _service.GetById(id, currentCompanyId);
    }

    [HttpGet("current")]
    [Authorize(Policy = "BaseUserRolesPolicy")]
    public async Task<ActionResult<Point>> GetCurrent()
    {
        var currentUserId = (int)HttpContext.Items["CurrentUserId"]!;
        var currentCompanyId = (int)HttpContext.Items["CurrentCompanyId"]!;
        return await _service.GetOpenPoint(currentUserId, currentCompanyId);
    }

    [HttpPost("auto")]
    [Authorize(Policy = "BaseUserRolesPolicy")]
    public async Task<ActionResult<Point>> AutoCheckIn([FromBody] string? description)
    {
        var currentUserId = (int)HttpContext.Items["CurrentUserId"]!;
        var currentCompanyId = (int)HttpContext.Items["CurrentCompanyId"]!;
        return await _service.AutoCheckIn(currentUserId, currentCompanyId, description);
    }

    [HttpPut("auto")]
    [Authorize(Policy = "BaseUserRolesPolicy")]
    public async Task<ActionResult<Point>> AutoCheckOut([FromBody] string? description)
    {
        var currentUserId = (int)HttpContext.Items["CurrentUserId"]!;
        var currentCompanyId = (int)HttpContext.Items["CurrentCompanyId"]!;
        return await _service.AutoCheckOut(currentUserId, currentCompanyId, description);
    }

    [HttpPost("manual")]
    [Authorize(Policy = "BaseUserRolesPolicy")]
    public async Task<ActionResult<Point>> ManualCheckIn([FromBody] Point point)
    {
        var currentUserId = (int)HttpContext.Items["CurrentUserId"]!;
        var currentCompanyId = (int)HttpContext.Items["CurrentCompanyId"]!;
        point.UserId = currentUserId;
        point.CompanyId = currentCompanyId;
        return await _service.ManualCheckIn(point);
    }

    [HttpPut("manual")]
    [Authorize(Policy = "BaseUserRolesPolicy")]
    public async Task<ActionResult<Point>> ManualCheckOut([FromBody] Point point)
    {
        var currentUserId = (int)HttpContext.Items["CurrentUserId"]!;
        var currentCompanyId = (int)HttpContext.Items["CurrentCompanyId"]!;
        point.UserId = currentUserId;
        point.CompanyId = currentCompanyId;
        return await _service.ManualCheckOut(point);
    }

    [HttpPut]
    [Authorize(Policy = "BaseUserRolesPolicy")]
    public async Task<ActionResult<Point>> UpdateDescription([FromBody] Point point)
    {
        var currentUserId = (int)HttpContext.Items["CurrentUserId"]!;
        var currentCompanyId = (int)HttpContext.Items["CurrentCompanyId"]!;
        point.UserId = currentUserId;
        point.CompanyId = currentCompanyId;
        return await _service.UpdateDescription(point);
    }

    [HttpPut("full")]
    [Authorize(Policy = "SuperUserRolesPolicy")]
    public async Task<ActionResult<Point>> UpdateFull([FromBody] Point point)
    {
        var currentUserId = (int)HttpContext.Items["CurrentUserId"]!;
        var currentCompanyId = (int)HttpContext.Items["CurrentCompanyId"]!;
        point.ManagerId = currentUserId;
        point.CompanyId = currentCompanyId;
        return await _service.UpdateFull(point);
    }

    [HttpPost("approve")]
    [Authorize(Policy = "SuperUserRolesPolicy")]
    public async Task<ActionResult<Point>> Approve([FromBody] int id)
    {
        var currentCompanyId = (int)HttpContext.Items["CurrentCompanyId"]!;
        var currentManagerUserId = (int)HttpContext.Items["CurrentUserId"]!;
        return await _service.Approve(id, currentManagerUserId, currentCompanyId);
    }

    [HttpPost("reject")]
    [Authorize(Policy = "SuperUserRolesPolicy")]
    public async Task<ActionResult<Point>> Reject([FromBody] int id)
    {
        var currentCompanyId = (int)HttpContext.Items["CurrentCompanyId"]!;
        var currentManagerUserId = (int)HttpContext.Items["CurrentUserId"]!;
        return await _service.Reject(id, currentManagerUserId, currentCompanyId);
    }
}
