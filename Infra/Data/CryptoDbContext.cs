using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data
{
    public class CryptoDbContext : DbContext
    {
        public CryptoDbContext(DbContextOptions<CryptoDbContext> options) : base(options)
        {
        }

        public DbSet<CryptoCurrency> Cryptocurrencies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CryptoCurrency>(entity =>
            {
                entity.ToTable("Cryptocurrency");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Symbol).HasColumnType("TEXT").IsRequired();
                entity.Property(e => e.Price).HasColumnType("REAL").IsRequired();
                entity.Property(e => e.CreatedAt).HasColumnType("DATETIME").IsRequired();
            });
        }
    }
}
