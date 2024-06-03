using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PontoSavi.Domain.Entities;

namespace PontoSavi.Infra.Data.Configurations;

public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.HasData(
            new UserRole { UserId = "1", RoleId = "1" },
            new UserRole { UserId = "1", RoleId = "4" },

            new UserRole { UserId = "2", RoleId = "2" },
            new UserRole { UserId = "2", RoleId = "4" },

            new UserRole { UserId = "3", RoleId = "3" },
            new UserRole { UserId = "3", RoleId = "4" }
        );
    }
}