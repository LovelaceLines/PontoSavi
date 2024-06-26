using Bogus;

using PontoSavi.Domain.Entities;

namespace PontoSavi.Test.Fakers;

public class UserWorkShiftFake : Faker<UserWorkShift>
{
    public UserWorkShiftFake(int? userId = null, int? workShiftId = null, int? companyId = null)
    {
        RuleFor(x => x.UserId, f => userId ?? 0);
        RuleFor(x => x.WorkShiftId, f => workShiftId ?? 0);
        RuleFor(x => x.CompanyId, f => companyId ?? 0);
    }
}
