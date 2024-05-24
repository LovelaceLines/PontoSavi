using Bogus;

using PontoSavi.Domain.DTOs;

namespace PontoSavi.Test.Fakers;

public class UserFake : Faker<UserDTO>
{
    public UserFake(string? id = null, string? userName = null, string? email = null, string? phoneNumber = null, string? password = null, List<string>? roles = null)
    {
        RuleFor(x => x.Id, f => id ?? null);
        RuleFor(x => x.UserName, f => userName ?? f.Person.UserName);
        RuleFor(x => x.Email, f => email ?? f.Person.Email);
        RuleFor(x => x.PhoneNumber, f => phoneNumber ?? f.Person.Phone);
        RuleFor(x => x.Password, f => password ?? f.Person.FirstName + '@' + f.Random.Replace("##**"));
        RuleFor(x => x.Roles, f => roles ?? []);
    }
}
