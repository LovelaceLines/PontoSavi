using Bogus;

namespace PontoSavi.Test.Fakers;

public class User : PontoSavi.API.InputModels.UserAndPassword { }

public class UserFake : Faker<User>
{
    public UserFake(int? id = null, string? userName = null, string? name = null, string? email = null, string? phoneNumber = null, string? password = null, int? tenantId = null)
    {
        RuleFor(x => x.Id, f => id ?? 0);
        RuleFor(x => x.UserName, f => userName ?? f.Person.UserName + f.Random.Replace("##**"));
        RuleFor(x => x.Name, f => name ?? f.Person.FullName);
        RuleFor(x => x.Email, f => email ?? f.Person.Email);
        RuleFor(x => x.PhoneNumber, f => phoneNumber ?? f.Person.Phone);
        RuleFor(x => x.Password, f => password ?? f.Person.FirstName + '@' + f.Random.Replace("##**"));
        RuleFor(x => x.TenantId, f => tenantId ?? 0);
    }
}
