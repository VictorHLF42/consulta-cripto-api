using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Configurations;

public class CryptoCurrencyConfiguration : IEntityTypeConfiguration<CryptoCurrency>
{
    public void Configure(EntityTypeBuilder<CryptoCurrency> builder)
    {
        builder
            .ToTable("Cryptocurrency")
            .HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName("Id").IsRequired().ValueGeneratedOnAdd();
        builder.Property(x => x.Symbol).HasColumnName("Symbol").IsRequired();
        builder.Property(x => x.Price).HasColumnName("Price").IsRequired();
        builder.Property(x => x.CreatedAt).HasColumnName("CreatedAt").IsRequired();
    }
}
