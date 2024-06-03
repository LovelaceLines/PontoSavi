using Microsoft.AspNetCore.Identity;

namespace PontoSavi.Domain.Entities;

public class User : IdentityUser
{
    public string Name { get; set; } = null!;

    public User() { }

    public User(string userName)
    {
        UserName = userName;
    }
}
