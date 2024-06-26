using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using PontoSavi.Domain.Entities;

namespace PontoSavi.Infra.Data.Configurations;

public class DayOffConfiguration : IEntityTypeConfiguration<DayOff>
{
    public void Configure(EntityTypeBuilder<DayOff> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Tenant)
            .WithMany()
            .HasForeignKey(x => x.TenantId);

        builder.HasIndex(x => new { x.Date, x.TenantId })
            .IsUnique();

        builder.Property(p => p.CreatedAt)
            .ValueGeneratedOnAdd()
            .HasValueGenerator<DateTimeNowValueGenerator>();

        builder.Property(p => p.UpdatedAt)
            .ValueGeneratedOnUpdate()
            .HasValueGenerator<DateTimeNowValueGenerator>();
    }
}
