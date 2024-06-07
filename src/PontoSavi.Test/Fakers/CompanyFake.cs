using Bogus;

using PontoSavi.Domain.DTOs;

namespace PontoSavi.Test.Fakers;

public class CompanyFake : Faker<CompanyDTO>
{
    public CompanyFake(string? publicId = null, string? name = null, string? tradeName = null, string? cnpj = null)
    {
        RuleFor(x => x.PublicId, f => publicId ?? null);
        RuleFor(x => x.Name, f => name ?? f.Person.FullName);
        RuleFor(x => x.TradeName, f => tradeName ?? f.Person.FullName);
        RuleFor(x => x.CNPJ, f => cnpj ?? f.Random.Replace("##############"));
    }
}
