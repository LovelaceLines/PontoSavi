using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using PontoSavi.Domain.Entities;

namespace PontoSavi.Infra.Data.Configurations;

public class CompanyWorkShiftConfiguration : IEntityTypeConfiguration<CompanyWorkShift>
{
    public void Configure(EntityTypeBuilder<CompanyWorkShift> builder)
    {
        builder.HasKey(x => x.WorkShiftId);

        builder.HasIndex(x => new { x.WorkShiftId, x.TenantId })
            .IsUnique();

        builder.HasOne(x => x.WorkShift)
            .WithOne()
            .HasForeignKey<CompanyWorkShift>(x => x.WorkShiftId);

        builder.HasOne(x => x.Tenant)
            .WithMany()
            .HasForeignKey(x => x.TenantId);

        builder.Property(x => x.CreatedAt)
            .ValueGeneratedOnAdd()
            .HasValueGenerator<DateTimeNowValueGenerator>();

        builder.Property(x => x.UpdatedAt)
            .ValueGeneratedOnUpdate()
            .HasValueGenerator<DateTimeNowValueGenerator>();
    }
}
