using Bogus;

using PontoSavi.Domain.DTOs;

namespace PontoSavi.Test.Fakers;

public class RoleFake : Faker<RoleDTO>
{
    public RoleFake(string? publicId = null, string? name = null)
    {
        RuleFor(x => x.PublicId, f => publicId ?? null);
        RuleFor(x => x.Name, f => name ?? f.Company.CompanyName() + f.Random.Replace("##**"));
    }
}
