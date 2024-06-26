using PontoSavi.Application.Services;
using PontoSavi.Test.Fakers;

namespace PontoSavi.Test.Services;

public class PointServiceTest
{
    [Fact]
    public void ValidPoint_ReturnApproved()
    {
        var defaultDate = GetDefaultDate();
        var checkIn = new Bogus.Faker().Date.Between(start: defaultDate.AddHours(-8), end: defaultDate.AddHours(-4));
        var checkOut = new Bogus.Faker().Date.Between(start: defaultDate.AddHours(-3), end: defaultDate.AddHours(-1));

        var point = new PointFake(checkIn: checkIn, checkOut: checkOut).Generate();
        var workShifts = new WorkShiftFake(checkIn: new TimeOnly(checkIn.Hour, checkIn.Minute), checkInToleranceMinutes: 1, checkOut: new TimeOnly(checkOut.Hour, checkOut.Minute), checkOutToleranceMinutes: 1).Generate(1);

        var result = PointService.IsStatusApproved(point, workShifts);

        Assert.True(result, userMessage: $"CheckIn: {checkIn}, CheckOut: {checkOut}");
    }

    [Fact]
    public void InvalidPoint_ReturnDisapproved()
    {
        var defaultDate = GetDefaultDate();
        var checkIn = new Bogus.Faker().Date.Between(start: defaultDate.AddHours(-8), end: defaultDate.AddHours(-4));
        var checkOut = new Bogus.Faker().Date.Between(start: defaultDate.AddHours(-3), end: defaultDate.AddHours(-1));

        var point = new PointFake(checkIn: checkIn, checkOut: checkOut).Generate();
        var workShifts = new WorkShiftFake(checkIn: new Bogus.Faker().Date.RecentTimeOnly(), checkInToleranceMinutes: 1, checkOut: new Bogus.Faker().Date.RecentTimeOnly(), checkOutToleranceMinutes: 1).Generate(1);

        var result = PointService.IsStatusApproved(point, workShifts);

        Assert.False(result);
    }

    public static DateTime GetDefaultDate()
    {
        var defaultDate = new Bogus.Faker().Date.Recent(days: 5);
        return new DateTime(defaultDate.Year, defaultDate.Month, defaultDate.Day, 12, 0, 0);
    }
}
