using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PontoSavi.Infra.Data.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
{
    public void Configure(EntityTypeBuilder<IdentityRole> builder)
    {
        builder.Property(r => r.Id)
            .ValueGeneratedOnAdd();

        builder.HasData(
            new IdentityRole { Id = "1", Name = "Desenvolvedor", NormalizedName = "DESENVOLVEDOR", ConcurrencyStamp = Guid.NewGuid().ToString() },
            new IdentityRole { Id = "2", Name = "Administrador", NormalizedName = "ADMINISTRADOR", ConcurrencyStamp = Guid.NewGuid().ToString() },
            new IdentityRole { Id = "3", Name = "Supervisor", NormalizedName = "SUPERVISOR", ConcurrencyStamp = Guid.NewGuid().ToString() },
            new IdentityRole { Id = "4", Name = "Colaborador", NormalizedName = "COLABORADOR", ConcurrencyStamp = Guid.NewGuid().ToString() }
        );
    }
}