using Bogus;

using PontoSavi.Domain.Entities;

namespace PontoSavi.Test.Fakers;

public class CompanyWorkShiftFake : Faker<CompanyWorkShift>
{
    public CompanyWorkShiftFake(int? workShiftId = null, int? tenantId = null)
    {
        RuleFor(x => x.WorkShiftId, f => workShiftId ?? 0);
        RuleFor(x => x.TenantId, f => tenantId ?? 0);
    }
}
