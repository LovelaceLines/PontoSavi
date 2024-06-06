using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PontoSavi.Domain.Entities;
using PontoSavi.Infra.Data.Configurations.Util;

namespace PontoSavi.Infra.Data.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(r => r.Id);

        builder.Property(r => r.Id)
            .ValueGeneratedOnAdd();

        builder.Property(r => r.PublicId)
            .IsUnicode()
            .HasValueGenerator<UlidValueGenerator>();

        builder.HasData(
            new Role
            {
                Id = 1,
                PublicId = Ulid.NewUlid().ToString(),
                Name = "Desenvolvedor",
                NormalizedName = "DESENVOLVEDOR",
                ConcurrencyStamp = Guid.NewGuid().ToString()
            },
            new Role
            {
                Id = 2,
                PublicId = Ulid.NewUlid().ToString(),
                Name = "Administrador",
                NormalizedName = "ADMINISTRADOR",
                ConcurrencyStamp = Guid.NewGuid().ToString()
            },
            new Role
            {
                Id = 3,
                PublicId = Ulid.NewUlid().ToString(),
                Name = "Supervisor",
                NormalizedName = "SUPERVISOR",
                ConcurrencyStamp = Guid.NewGuid().ToString()
            },
            new Role
            {
                Id = 4,
                PublicId = Ulid.NewUlid().ToString(),
                Name = "Colaborador",
                NormalizedName = "COLABORADOR",
                ConcurrencyStamp = Guid.NewGuid().ToString()
            }
        );
    }
}