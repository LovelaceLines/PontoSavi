using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PontoSavi.Domain.Entities;

namespace PontoSavi.Infra.Data.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.Property(r => r.Id)
            .ValueGeneratedOnAdd();

        builder.HasData(
            new Role { Id = "1", Name = "Desenvolvedor", NormalizedName = "DESENVOLVEDOR", ConcurrencyStamp = Guid.NewGuid().ToString() },
            new Role { Id = "2", Name = "Administrador", NormalizedName = "ADMINISTRADOR", ConcurrencyStamp = Guid.NewGuid().ToString() },
            new Role { Id = "3", Name = "Supervisor", NormalizedName = "SUPERVISOR", ConcurrencyStamp = Guid.NewGuid().ToString() },
            new Role { Id = "4", Name = "Colaborador", NormalizedName = "COLABORADOR", ConcurrencyStamp = Guid.NewGuid().ToString() }
        );
    }
}