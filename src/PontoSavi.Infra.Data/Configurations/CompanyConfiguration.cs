using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PontoSavi.Domain.Entities;
using PontoSavi.Infra.Data.Configurations.Util;

namespace PontoSavi.Infra.Data.Configurations;

public class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .ValueGeneratedOnAdd();

        builder.Property(c => c.PublicId)
            .IsUnicode()
            .HasValueGenerator<UlidValueGenerator>();

        builder.Property(c => c.Name)
            // TODO .IsUnicode()
            .IsRequired();

        builder.Property(c => c.TradeName)
            .IsRequired();

        builder.Property(c => c.CNPJ)
            // TODO .IsUnicode()
            .IsRequired();
    }
}
