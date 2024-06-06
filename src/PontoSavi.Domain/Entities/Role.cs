using Microsoft.AspNetCore.Identity;

namespace PontoSavi.Domain.Entities;

public class Role : IdentityRole<int>
{
    public string PublicId { get; set; } = null!;

    public Role()
    {
        PublicId = Ulid.NewUlid().ToString();
    }

    public Role(string roleName)
    {
        PublicId = Ulid.NewUlid().ToString();
        Name = roleName;
    }
}
