using Bogus;

using PontoSavi.Domain.Entities;

namespace PontoSavi.Test.Fakers;

public class CompanyFake : Faker<Company>
{
    public CompanyFake(int? id = null, string? name = null, string? tradeName = null, string? cnpj = null)
    {
        RuleFor(x => x.Id, f => id ?? 0);
        RuleFor(x => x.Name, f => name ?? f.Person.FullName);
        RuleFor(x => x.TradeName, f => tradeName ?? f.Person.FullName);
        RuleFor(x => x.CNPJ, f => cnpj ?? f.Random.Replace("##############"));
    }
}
