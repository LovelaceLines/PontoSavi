using Bogus;

using PontoSavi.Domain.DTOs;

namespace PontoSavi.Test.Fakers;

public class RoleFake : Faker<RoleDTO>
{
    public RoleFake(string? id = null, string? name = null)
    {
        RuleFor(x => x.Id, f => id ?? null);
        RuleFor(x => x.Name, f => name ?? f.Company.CompanyName());
    }
}
