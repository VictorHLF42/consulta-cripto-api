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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.EnableDetailedErrors();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CryptoDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

    }
}
