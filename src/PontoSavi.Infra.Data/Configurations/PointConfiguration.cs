using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using PontoSavi.Domain.Entities;

namespace PontoSavi.Infra.Data.Configurations;

public class PointConfiguration : IEntityTypeConfiguration<Point>
{
    public void Configure(EntityTypeBuilder<Point> builder)
    {
        builder.HasKey(p => p.Id);

        builder.HasOne(p => p.User)
            .WithMany()
            .HasForeignKey(p => p.UserId);

        builder.HasOne(p => p.Manager)
            .WithMany()
            .HasForeignKey(p => p.ManagerId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(p => p.Tenant)
            .WithMany()
            .HasForeignKey(p => p.TenantId);

        builder.Property(p => p.CreatedAt)
            .ValueGeneratedOnAdd()
            .HasValueGenerator<DateTimeNowValueGenerator>();

        builder.Property(p => p.UpdatedAt)
            .ValueGeneratedOnUpdate()
            .HasValueGenerator<DateTimeNowValueGenerator>();
    }
}
