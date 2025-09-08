using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Infra.Data;


namespace Infra.Data.Factories
{
    public class CryptoDbContextFactory : IDesignTimeDbContextFactory<CryptoDbContext>
    {
        public CryptoDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CryptoDbContext>();
            optionsBuilder.UseSqlite("Data Source=crypto.db");

            return new CryptoDbContext(optionsBuilder.Options);
        }
    }
}