using PontoSavi.Domain.Entities;
using PontoSavi.Test.Global;

namespace PontoSavi.Test.API;

public class CompanyControllerTests : GlobalClientRequest
{
    [Fact]
    public async Task Get_GetByCurrentCompanyId_ReturnsOk()
    {
        var result = await Get<Company>(_companyClient);

        Assert.NotNull(result);
    }

    [Fact]
    public async Task Post_AddWorkShift_ReturnsOk()
    {
        var workShift = await GetWorkShift();
        var companyWorkShift = new CompanyWorkShift { WorkShiftId = workShift.Id };

        var result = await PostFromBody<CompanyWorkShift>(_companyAddWorkShiftClient, companyWorkShift);

        Assert.Equal(workShift.Id, result.WorkShiftId);
    }

    [Fact]
    public async Task Delete_RemoveWorkShift_ReturnsOk()
    {
        var companyWorkShift = await GetCompanyWorkShift();

        var result = await DeleteFromBody<CompanyWorkShift>(_companyRemoveWorkShiftClient, companyWorkShift);

        Assert.Equal(companyWorkShift.WorkShiftId, result.WorkShiftId);
    }
}
