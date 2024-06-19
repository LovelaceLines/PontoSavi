using Bogus;

using PontoSavi.API.InputModels;

namespace PontoSavi.Test.Fakers;

public class CompanyAndUserFake : Faker<CompanyAndUser>
{
    public CompanyAndUserFake(string? companyName = null, string? tradeName = null, string? cnpj = null, string? userName = null, string? email = null, string? phoneNumber = null, string? password = null)
    {
        RuleFor(x => x.Company, f => new CompanyFake(name: companyName, tradeName: tradeName, cnpj: cnpj).Generate());
        RuleFor(x => x.User, f => new UserFake(name: userName, userName: userName, email: email, phoneNumber: phoneNumber, password: password).Generate());
    }
}
