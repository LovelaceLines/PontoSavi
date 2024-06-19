using System.Net;

using PontoSavi.Domain.Entities;
using PontoSavi.Domain.Filters;
using PontoSavi.Test.DTOs;
using PontoSavi.Test.Fakers;
using PontoSavi.Test.Global;

namespace PontoSavi.Test.API;

public class DayOffControllerTest : GlobalClientRequest
{
    [Fact]
    public async Task Get_Query_ReturnsOk()
    {
        var dayOff = await GetDayOff();
        var filter = new DayOffFilter
        {
            Id = dayOff.Id,
            Date = dayOff.Date,
            Description = dayOff.Description,
            Search = dayOff.Description
        };

        var result = await GetFromQuery<QueryResult<DayOff>>(_dayOffClient, filter);
        var item = result.Items.FirstOrDefault();

        Assert.Equivalent(dayOff, item);
    }

    [Fact]
    public async Task Get_GetById_ReturnsOk()
    {
        var dayOff = await GetDayOff();

        var result = await GetFromUri<DayOff>(_dayOffClient, dayOff.Id);

        Assert.Equivalent(dayOff, result);
    }

    [Fact]
    public async Task Post_ValidDayOff_ReturnsOk()
    {
        var fake = new DayOffFake().Generate();

        var result = await PostFromBody<DayOff>(_dayOffClient, fake);

        Assert.Equal(fake.Date, result.Date);
        Assert.Equal(fake.Description, result.Description);
    }

    [Fact]
    public async Task Post_DuplicatedDayOff_ReturnsBadRequest()
    {
        var dayOff = await GetDayOff();
        var fake = new DayOffFake(date: dayOff.Date).Generate();

        var result = await PostFromBody<AppHttpResponse>(_dayOffClient, fake);

        Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
    }

    [Fact]
    public async Task Put_ValidDayOff_ReturnsOk()
    {
        var dayOff = await GetDayOff();
        dayOff = new DayOffFake(id: dayOff.Id).Generate();

        var result = await PutFromBody<DayOff>(_dayOffClient, dayOff);

        Assert.Equal(dayOff.Id, result.Id);
        Assert.Equal(dayOff.Date, result.Date);
        Assert.Equal(dayOff.Description, result.Description);
    }

    [Fact]
    public async Task Put_DuplicatedDayOff_ReturnsBadRequest()
    {
        var dayOff1 = await GetDayOff();
        var dayOff2 = await GetDayOff();
        var fake = new DayOffFake(id: dayOff1.Id, date: dayOff2.Date).Generate();

        var result = await PutFromBody<AppHttpResponse>(_dayOffClient, fake);

        Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
    }

    [Fact]
    public async Task Delete_ValidDayOff_ReturnsOk()
    {
        var dayOff = await GetDayOff();

        var result = await DeleteFromUri<DayOff>(_dayOffClient, dayOff.Id);

        Assert.Equivalent(dayOff, result);
    }
}
