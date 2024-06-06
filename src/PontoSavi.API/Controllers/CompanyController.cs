using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using PontoSavi.API.ServiceFilters;
using PontoSavi.Application.Interfaces;
using PontoSavi.Domain.DTOs;
using PontoSavi.Domain.Filters;
using PontoSavi.Domain.Entities;

namespace PontoSavi.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[ServiceFilter(typeof(AuthAndUserExtractionFilter))]
public class CompanyController : ControllerBase
{
    private readonly ICompanyService _service;
    private readonly IMapper _mapper;

    public CompanyController(ICompanyService service,
        IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    /// <summary>
    /// Queries companies by filter.
    /// </summary>
    [HttpGet]
    [Authorize(Policy = "SuperUserRolesPolicy")]
    public async Task<ActionResult<QueryResult<CompanyDTO>>> Query([FromQuery] CompanyFilter filter) =>
        Ok(await _service.Query(filter));

    /// <summary>
    /// Gets a company by its publicId.
    /// </summary>
    [HttpGet("{publicId}")]
    [Authorize(Policy = "SuperUserRolesPolicy")]
    public async Task<ActionResult<CompanyDTO>> GetByPublicId(string publicId) =>
        Ok(new CompanyDTO(await _service.GetByPublicId(publicId)));

    /// <summary>
    /// Creates a company.
    /// </summary>
    [HttpPost]
    [Authorize(Policy = "SuperUserRolesPolicy")]
    public async Task<ActionResult<CompanyDTO>> Post([FromBody] CompanyDTO companyDTO)
    {
        var company = _mapper.Map<Company>(companyDTO);
        company = await _service.Create(company);
        return Ok(new CompanyDTO(company));
    }

    /// <summary>
    /// Updates a company.
    /// </summary>
    [HttpPut]
    [Authorize(Policy = "SuperUserRolesPolicy")]
    public async Task<ActionResult<CompanyDTO>> Put([FromBody] CompanyDTO companyDTO)
    {
        var company = _mapper.Map<Company>(companyDTO);
        company = await _service.Update(company);
        return Ok(new CompanyDTO(company));
    }

    /// <summary>
    /// Removes a company by its publicId.
    /// </summary>
    [HttpDelete("{publicId}")]
    [Authorize(Policy = "SuperUserRolesPolicy")]
    public async Task<ActionResult<CompanyDTO>> Delete(string publicId)
    {
        var company = await _service.Delete(publicId);
        return Ok(new CompanyDTO(company));
    }
}
