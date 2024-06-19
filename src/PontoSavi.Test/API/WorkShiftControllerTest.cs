using PontoSavi.Domain.Entities;
using PontoSavi.Test.Fakers;
using PontoSavi.Test.Global;

namespace PontoSavi.Test.API;

public class WorkShiftControllerTest : GlobalClientRequest
{
    [Fact]
    public async Task Post_ValidWorkShift_ReturnsOk()
    {
        var workShift = new WorkShiftFake().Generate();

        var result = await PostFromBody<WorkShift>(_workShiftClient, workShift);

        Assert.Equal(workShift.CheckIn, result.CheckIn);
        Assert.Equal(workShift.CheckInToleranceMinutes, result.CheckInToleranceMinutes);
        Assert.Equal(workShift.CheckOut, result.CheckOut);
        Assert.Equal(workShift.CheckOutToleranceMinutes, result.CheckOutToleranceMinutes);
        Assert.Equal(workShift.Description, result.Description);
    }

    [Fact]
    public async Task Put_ValidWorkShift_ReturnsOk()
    {
        var workShift = await GetWorkShift();
        var fake = new WorkShiftFake(id: workShift.Id).Generate();

        var result = await PutFromBody<WorkShift>(_workShiftClient, fake);

        Assert.Equal(fake.Id, result.Id);
        Assert.Equal(fake.CheckIn, result.CheckIn);
        Assert.Equal(fake.CheckInToleranceMinutes, result.CheckInToleranceMinutes);
        Assert.Equal(fake.CheckOut, result.CheckOut);
        Assert.Equal(fake.CheckOutToleranceMinutes, result.CheckOutToleranceMinutes);
        Assert.Equal(fake.Description, result.Description);
    }

    [Fact]
    public async Task Delete_ValidWorkShift_ReturnsOk()
    {
        var workShift = await GetWorkShift();

        var result = await DeleteFromUri<WorkShift>(_workShiftClient, workShift.Id);

        Assert.Equivalent(workShift, result);
    }
}
