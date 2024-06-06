using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PontoSavi.Domain.Entities;
using PontoSavi.Infra.Data.Configurations.Util;

namespace PontoSavi.Infra.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id)
            .ValueGeneratedOnAdd();

        builder.Property(u => u.PublicId)
            .IsUnicode()
            .HasValueGenerator<UlidValueGenerator>();

        builder.Property(u => u.UserName)
            .IsUnicode();

        builder.HasData(
            new User
            {
                Id = 1,
                PublicId = Ulid.NewUlid().ToString(),
                UserName = "dev",
                Name = "Developer",
                NormalizedUserName = "DEV",
                Email = "dev@gmail.com",
                NormalizedEmail = "DEV@GMAIL.COM",
                PhoneNumber = "(55) 85 9 9999-9999",
                PasswordHash = new PasswordHasher<User>().HashPassword(new User("dev"), "!23L6(bNi.22T71,%4vfR{<~tA.]"),
                SecurityStamp = Guid.NewGuid().ToString(),
                ConcurrencyStamp = Guid.NewGuid().ToString()
            },
            new User
            {
                Id = 2,
                PublicId = Ulid.NewUlid().ToString(),
                UserName = "admin",
                Name = "Administrator",
                NormalizedUserName = "ADMIN",
                Email = "admin@gmail.com",
                NormalizedEmail = "ADMIN@GMAIL.COM",
                PhoneNumber = "(55) 85 9 9999-9998",
                PasswordHash = new PasswordHasher<User>().HashPassword(new User("admin"), "!23L6(bNi.22T71,%4vfR{<~tA.]"),
                SecurityStamp = Guid.NewGuid().ToString(),
                ConcurrencyStamp = Guid.NewGuid().ToString()
            },
            new User
            {
                Id = 3,
                PublicId = Ulid.NewUlid().ToString(),
                UserName = "super",
                Name = "Supervisor",
                NormalizedUserName = "SUPER",
                Email = "super@gmail.com",
                NormalizedEmail = "SUPER@GMAIL.COM",
                PhoneNumber = "(55) 85 9 9999-9997",
                PasswordHash = new PasswordHasher<User>().HashPassword(new User("super"), "!23L6(bNi.22T71,%4vfR{<~tA.]"),
                SecurityStamp = Guid.NewGuid().ToString(),
                ConcurrencyStamp = Guid.NewGuid().ToString()
            }
        );
    }
}