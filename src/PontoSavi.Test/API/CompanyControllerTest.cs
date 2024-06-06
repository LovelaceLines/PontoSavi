using System.Net;

using PontoSavi.Domain.DTOs;
using PontoSavi.Domain.Exceptions;
using PontoSavi.Domain.Filters;
using PontoSavi.Test.DTOs;
using PontoSavi.Test.Fakers;
using PontoSavi.Test.Global;

namespace PontoSavi.Test.API;

public class CompanyControllerTests : GlobalClientRequest
{
    [Fact]
    public async Task Get_QueryByPublicId_ReturnsCompany()
    {
        var company = await GetCompany();

        var result = await GetFromQuery<QueryResult<CompanyDTO>>(_companyClient, new CompanyFilter { PublicId = company.PublicId });
        var companyGet = result.Items.Single();

        Assert.Equivalent(company.Name, companyGet.Name);
        Assert.Equivalent(company.TradeName, companyGet.TradeName);
        Assert.Equivalent(company.CNPJ, companyGet.CNPJ);
    }

    [Fact]
    public async Task Post_ValidCompany_ReturnsOkResult()
    {
        var company = new CompanyFake().Generate();

        var result = await PostFromBody<CompanyDTO>(_companyClient, company);

        Assert.Equivalent(company.Name, result.Name);
        Assert.Equivalent(company.TradeName, result.TradeName);
        Assert.Equivalent(company.CNPJ, result.CNPJ);
    }

    [Fact]
    public async Task Put_ValidCompany_ReturnsOkResult()
    {
        var company = await GetCompany();
        var updatedCompany = new CompanyFake(publicId: company.PublicId, name: company.Name, cnpj: company.CNPJ).Generate();

        var result = await PutFromBody<CompanyDTO>(_companyClient, updatedCompany);

        Assert.Equivalent(updatedCompany.PublicId, result.PublicId);
        Assert.Equivalent(updatedCompany.Name, result.Name);
        Assert.Equivalent(updatedCompany.TradeName, result.TradeName);
        Assert.Equivalent(updatedCompany.CNPJ, result.CNPJ);
    }

    [Fact]
    public async Task Put_InvalidCompany_ReturnsBadRequestResult()
    {
        var company = await GetCompany();
        var updatedCompany = new CompanyFake(publicId: company.PublicId).Generate();

        var result = await PutFromBody<AppHttpResponse>(_companyClient, updatedCompany);

        Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
    }

    [Fact]
    public async Task Delete_ValidId_ReturnsOkResult()
    {
        var company = await GetCompany();

        var result = await DeleteFromUri<CompanyDTO>(_companyClient, company.PublicId!);

        Assert.Equal(company.PublicId, result.PublicId);
    }

    [Fact]
    public async Task Delete_InvalidPublicId_ReturnsNotFoundResult()
    {
        var publicId = Guid.NewGuid().ToString();

        var result = await DeleteFromUri<AppHttpResponse>(_companyClient, publicId);

        Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
    }
}
