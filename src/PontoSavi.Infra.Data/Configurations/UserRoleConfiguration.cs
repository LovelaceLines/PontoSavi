using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using PontoSavi.Domain.Entities;

namespace PontoSavi.Infra.Data.Configurations;

public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.ToTable("UserRoles");

        builder.HasKey(ur => new { ur.UserId, ur.RoleId, ur.TenantId });

        builder.HasOne(ur => ur.Role)
            .WithMany()
            .HasForeignKey(ur => ur.RoleId);

        builder.HasOne(ur => ur.User)
            .WithMany()
            .HasForeignKey(ur => ur.UserId);

        builder.HasOne(ur => ur.Tenant)
            .WithMany()
            .HasForeignKey(ur => ur.TenantId);

        builder.Property(p => p.CreatedAt)
            .ValueGeneratedOnAdd()
            .HasValueGenerator<DateTimeNowValueGenerator>();

        builder.Property(p => p.UpdatedAt)
            .ValueGeneratedOnUpdate()
            .HasValueGenerator<DateTimeNowValueGenerator>();

        builder.HasData(
            new UserRole
            {
                UserId = 1,
                RoleId = 1,
                TenantId = 1,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            },
            new UserRole
            {
                UserId = 1,
                RoleId = 2,
                TenantId = 1,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            },
            new UserRole
            {
                UserId = 1,
                RoleId = 3,
                TenantId = 1,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            },
            new UserRole
            {
                UserId = 1,
                RoleId = 4,
                TenantId = 1,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            },
            new UserRole
            {
                UserId = 1,
                RoleId = 5,
                TenantId = 1,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            },
            new UserRole
            {
                UserId = 2,
                RoleId = 3,
                TenantId = 1,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            },
            new UserRole
            {
                UserId = 2,
                RoleId = 5,
                TenantId = 1,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            },
            new UserRole
            {
                UserId = 3,
                RoleId = 4,
                TenantId = 1,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            },
            new UserRole
            {
                UserId = 3,
                RoleId = 5,
                TenantId = 1,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            },
            new UserRole
            {
                UserId = 4,
                RoleId = 5,
                TenantId = 1,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            }
        );
    }
}