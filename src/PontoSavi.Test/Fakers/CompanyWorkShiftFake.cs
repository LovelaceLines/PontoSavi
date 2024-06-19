using Bogus;

using PontoSavi.Domain.Entities;

namespace PontoSavi.Test.Fakers;

public class CompanyWorkShiftFake : Faker<CompanyWorkShift>
{
    public CompanyWorkShiftFake(int? workShiftId = null, int? companyId = null)
    {
        RuleFor(x => x.WorkShiftId, f => workShiftId ?? 0);
        RuleFor(x => x.CompanyId, f => companyId ?? 0);
    }
}
