using Bogus;

using PontoSavi.Domain.Entities;

namespace PontoSavi.Test.Fakers;

public class PointFake : Faker<Point>
{
    public PointFake(int? id = null, int? userId = null, DateTime? checkIn = null, string? checkInDescription = null, DateTime? checkOut = null, string? checkOutDescription = null)
    {
        RuleFor(x => x.Id, f => id ?? 0);
        RuleFor(x => x.UserId, f => userId ?? 0);
        RuleFor(x => x.CheckIn, f => checkIn ?? f.Date.Past());
        RuleFor(x => x.CheckInDescription, f => checkInDescription ?? f.Lorem.Sentence());
        RuleFor(x => x.CheckOut, f => checkOut ?? f.Date.Past());
        RuleFor(x => x.CheckOutDescription, f => checkOutDescription ?? f.Lorem.Sentence());
    }
}
