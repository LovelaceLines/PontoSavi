using Bogus;

using PontoSavi.Domain.Entities;

namespace PontoSavi.Test.Fakers;

public class RoleFake : Faker<Role>
{
    public RoleFake(int? id = null, string? name = null, int? tenantId = null)
    {
        RuleFor(x => x.Id, f => id ?? 0);
        RuleFor(x => x.Name, f => name ?? f.Company.CompanyName() + f.Random.Replace("##**"));
        RuleFor(x => x.TenantId, f => tenantId ?? 0);
    }
}
