using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PontoSavi.Domain.Entities;

namespace PontoSavi.Infra.Data.Configurations;

public class UserSettingsConfiguration : IEntityTypeConfiguration<UserSettings>
{
    public void Configure(EntityTypeBuilder<UserSettings> builder)
    {
        builder.HasKey(us => us.UserId);

        builder.HasOne(us => us.User)
            .WithOne()
            .HasForeignKey<UserSettings>(us => us.UserId);

        builder.Property(us => us.CheckIn)
            .IsRequired();

        builder.Property(us => us.CheckInToleranceMinutes)
            .IsRequired();

        builder.Property(us => us.CheckOut)
            .IsRequired();

        builder.Property(us => us.CheckOutToleranceMinutes)
            .IsRequired();
    }
}
