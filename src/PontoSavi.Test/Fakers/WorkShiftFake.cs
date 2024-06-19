using Bogus;

using PontoSavi.Domain.Entities;

namespace PontoSavi.Test.Fakers;

public class WorkShiftFake : Faker<WorkShift>
{
    public WorkShiftFake(int? id = null, TimeOnly? checkIn = null, int? checkInToleranceMinutes = null, TimeOnly? checkOut = null, int? checkOutToleranceMinutes = null, string? description = null)
    {
        RuleFor(x => x.Id, f => id ?? 0);
        RuleFor(x => x.CheckIn, f => checkIn ?? new TimeOnly(f.Random.Number(0, 23), f.Random.Number(0, 59)));
        RuleFor(x => x.CheckInToleranceMinutes, f => checkInToleranceMinutes ?? f.Random.Number(0, 10));
        RuleFor(x => x.CheckOut, f => checkOut ?? new TimeOnly(f.Random.Number(0, 23), f.Random.Number(0, 59)));
        RuleFor(x => x.CheckOutToleranceMinutes, f => checkOutToleranceMinutes ?? f.Random.Number(0, 10));
        RuleFor(x => x.Description, f => description ?? f.Lorem.Sentence());
    }
}
