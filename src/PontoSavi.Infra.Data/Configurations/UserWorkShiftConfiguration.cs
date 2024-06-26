using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using PontoSavi.Domain.Entities;

namespace PontoSavi.Infra.Data.Configurations;

public class UserWorkShiftConfiguration : IEntityTypeConfiguration<UserWorkShift>
{
    public void Configure(EntityTypeBuilder<UserWorkShift> builder)
    {
        builder.HasKey(x => new { x.UserId, x.WorkShiftId });

        builder.HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId);

        builder.HasOne(x => x.WorkShift)
            .WithMany()
            .HasForeignKey(x => x.WorkShiftId);

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
