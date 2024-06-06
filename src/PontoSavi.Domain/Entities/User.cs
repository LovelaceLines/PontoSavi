using Microsoft.AspNetCore.Identity;

namespace PontoSavi.Domain.Entities;

public class User : IdentityUser<int>
{
    public string PublicId { get; set; } = null!;
    public string Name { get; set; } = null!;

    public User() { }

    public User(string userName)
    {
        UserName = userName;
    }
}
