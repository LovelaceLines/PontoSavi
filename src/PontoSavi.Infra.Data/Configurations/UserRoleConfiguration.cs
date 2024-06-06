using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PontoSavi.Infra.Data.Configurations;

public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<int>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<int>> builder)
    {
        builder.HasKey(ur => new { ur.UserId, ur.RoleId });

        builder.HasData(
            new IdentityUserRole<int> { UserId = 1, RoleId = 1 },
            new IdentityUserRole<int> { UserId = 1, RoleId = 4 },

            new IdentityUserRole<int> { UserId = 2, RoleId = 2 },
            new IdentityUserRole<int> { UserId = 2, RoleId = 4 },

            new IdentityUserRole<int> { UserId = 3, RoleId = 3 },
            new IdentityUserRole<int> { UserId = 3, RoleId = 4 }
        );
    }
}