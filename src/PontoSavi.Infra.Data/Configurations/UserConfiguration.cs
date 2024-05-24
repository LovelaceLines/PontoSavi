using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PontoSavi.Infra.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<IdentityUser>
{
    public void Configure(EntityTypeBuilder<IdentityUser> builder)
    {
        builder.Property(u => u.Id)
            .ValueGeneratedOnAdd();

        builder.HasData(
            new IdentityUser
            {
                Id = "1",
                UserName = "dev",
                NormalizedUserName = "DEV",
                Email = "dev@gmail.com",
                NormalizedEmail = "DEV@GMAIL.COM",
                PhoneNumber = "(55) 85 9 9999-9999",
                PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(new IdentityUser("dev"), "!23L6(bNi.22T71,%4vfR{<~tA.]"),
                SecurityStamp = Guid.NewGuid().ToString(),
                ConcurrencyStamp = Guid.NewGuid().ToString()
            },
            new IdentityUser
            {
                Id = "2",
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@gmail.com",
                NormalizedEmail = "ADMIN@GMAIL.COM",
                PhoneNumber = "(55) 85 9 9999-9998",
                PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(new IdentityUser("admin"), "!23L6(bNi.22T71,%4vfR{<~tA.]"),
                SecurityStamp = Guid.NewGuid().ToString(),
                ConcurrencyStamp = Guid.NewGuid().ToString()
            },
            new IdentityUser
            {
                Id = "3",
                UserName = "super",
                NormalizedUserName = "SUPER",
                Email = "super@gmail.com",
                NormalizedEmail = "SUPER@GMAIL.COM",
                PhoneNumber = "(55) 85 9 9999-9997",
                PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(new IdentityUser("super"), "!23L6(bNi.22T71,%4vfR{<~tA.]"),
                SecurityStamp = Guid.NewGuid().ToString(),
                ConcurrencyStamp = Guid.NewGuid().ToString()
            }
        );
    }
}