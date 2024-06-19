using Bogus;

using PontoSavi.Domain.Entities;

namespace PontoSavi.Test.Fakers;

public class DayOffFake : Faker<DayOff>
{
    public DayOffFake(int? id = null, DateTime? date = null, string? description = null)
    {
        RuleFor(x => x.Id, f => id ?? 0);
        RuleFor(x => x.Date, f => date ?? f.Date.Past());
        RuleFor(x => x.Description, f => description ?? f.Lorem.Sentence());
    }
}
