using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using PontoSavi.Domain.Entities;

namespace PontoSavi.Infra.Data.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("Roles");

        builder.HasKey(r => r.Id);

        builder.HasIndex(r => new { r.Name, r.CompanyId })
            .IsUnique();

        builder.HasOne(r => r.Company)
            .WithMany()
            .HasForeignKey(r => r.CompanyId);

        builder.Property(r => r.ConcurrencyStamp)
            .IsConcurrencyToken();

        builder.Property(p => p.CreatedAt)
            .ValueGeneratedOnAdd()
            .HasValueGenerator<DateTimeNowValueGenerator>();

        builder.Property(p => p.UpdatedAt)
            .ValueGeneratedOnUpdate()
            .HasValueGenerator<DateTimeNowValueGenerator>();

        builder.HasData(
            new Role
            {
                Id = 1,
                CompanyId = 1,
                Name = "Desenvolvedor",
                NormalizedName = "DESENVOLVEDOR",
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            },
            new Role
            {
                Id = 2,
                CompanyId = 1,
                Name = "CEO",
                NormalizedName = "CEO",
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            },
            new Role
            {
                Id = 3,
                CompanyId = 1,
                Name = "Administrador",
                NormalizedName = "ADMINISTRADOR",
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            },
            new Role
            {
                Id = 4,
                CompanyId = 1,
                Name = "Supervisor",
                NormalizedName = "SUPERVISOR",
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            },
            new Role
            {
                Id = 5,
                CompanyId = 1,
                Name = "Colaborador",
                NormalizedName = "COLABORADOR",
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            }
        );
    }
}