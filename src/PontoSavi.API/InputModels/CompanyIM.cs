using PontoSavi.Domain.Entities;

namespace PontoSavi.API.InputModels;

public class CompanyAndUser
{
    public Company Company { get; set; } = null!;
    public UserAndPassword User { get; set; } = null!;

    public CompanyAndUser() { }

    public CompanyAndUser(Company company, User user)
    {
        Company = company;
        User = (UserAndPassword)user;
    }

}