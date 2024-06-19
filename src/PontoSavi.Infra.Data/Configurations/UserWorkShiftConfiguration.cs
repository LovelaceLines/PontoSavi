﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using PontoSavi.Domain.Entities;

namespace PontoSavi.Infra.Data.Configurations;

public class UserWorkShiftConfiguration : IEntityTypeConfiguration<UserWorkShift>
{
    public void Configure(EntityTypeBuilder<UserWorkShift> builder)
    {
        builder.HasKey(x => x.WorkShiftId);

        builder.HasIndex(x => new { x.UserId, x.WorkShiftId })
            .IsUnique();

        builder.HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId);

        builder.HasOne(x => x.Company)
            .WithMany()
            .HasForeignKey(x => x.CompanyId);

        builder.HasOne(x => x.WorkShift)
            .WithOne()
            .HasForeignKey<UserWorkShift>(x => x.WorkShiftId);

        builder.Property(x => x.CreatedAt)
            .ValueGeneratedOnAdd()
            .HasValueGenerator<DateTimeNowValueGenerator>();

        builder.Property(x => x.UpdatedAt)
            .ValueGeneratedOnUpdate()
            .HasValueGenerator<DateTimeNowValueGenerator>();
    }
}
