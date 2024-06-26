﻿using Microsoft.AspNetCore.Mvc;

using PontoSavi.API.ServiceFilters;
using PontoSavi.Application.Interfaces;
using PontoSavi.Domain.Entities;

namespace PontoSavi.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[ServiceFilter(typeof(AuthAndUserExtractionFilter))]
public class CompanyController : ControllerBase
{
    private readonly ICompanyService _companyService;
    private readonly ICompanyWorkShiftService _companyWorkShiftService;
    private readonly IWorkShiftService _workShiftService;

    public CompanyController(ICompanyService companyService,
        ICompanyWorkShiftService companyWorkShiftService,
        IWorkShiftService workShiftService)
    {
        _companyService = companyService;
        _companyWorkShiftService = companyWorkShiftService;
        _workShiftService = workShiftService;
    }

    [HttpGet]
    public async Task<ActionResult<Company>> Get()
    {
        var currentTenantId = (int)HttpContext.Items["CurrentTenantId"]!;
        return await _companyService.GetById(currentTenantId);
    }

    [HttpPut]
    public async Task<ActionResult<Company>> Put([FromBody] Company company)
    {
        var currentTenantId = (int)HttpContext.Items["CurrentTenantId"]!;
        company.Id = currentTenantId;
        return await _companyService.Update(company);
    }

    [HttpPost("add-work-shift")]
    public async Task<ActionResult<CompanyWorkShift>> AddWorkShift([FromBody] CompanyWorkShift companyWorkShift)
    {
        var currentTenantId = (int)HttpContext.Items["CurrentTenantId"]!;
        companyWorkShift.TenantId = currentTenantId;
        return await _companyWorkShiftService.Create(companyWorkShift);
    }

    [HttpDelete("remove-work-shift")]
    public async Task<ActionResult<CompanyWorkShift>> RemoveWorkShift([FromBody] CompanyWorkShift companyWorkShift)
    {
        var currentTenantId = (int)HttpContext.Items["CurrentTenantId"]!;
        companyWorkShift.TenantId = currentTenantId;
        return await _companyWorkShiftService.Delete(companyWorkShift);
    }
}
