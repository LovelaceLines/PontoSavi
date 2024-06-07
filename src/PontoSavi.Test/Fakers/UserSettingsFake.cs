using Bogus;

using PontoSavi.API.ViewModels;

namespace PontoSavi.Test.Fakers;

public class UserSettingsFake : Faker<UserSettingsVM>
{
    public UserSettingsFake(string? userPublicId = null, TimeOnly? checkIn = null, int? checkInToleranceMinutes = null, TimeOnly? checkOut = null, int? checkOutToleranceMinutes = null)
    {
        RuleFor(x => x.UserPublicId, f => userPublicId ?? null);
        RuleFor(x => x.CheckIn, f => checkIn ?? TimeOnly.FromDateTime(DateTime.Now));
        RuleFor(x => x.CheckInToleranceMinutes, f => checkInToleranceMinutes ?? 5);
        RuleFor(x => x.CheckOut, f => checkOut ?? TimeOnly.FromDateTime(DateTime.Now.AddHours(8)));
        RuleFor(x => x.CheckOutToleranceMinutes, f => checkOutToleranceMinutes ?? 5);
    }
}
