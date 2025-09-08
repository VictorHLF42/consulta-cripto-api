using Domain.Interfaces;
using Domain;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Infra.Data.Repositories
{
    public class CryptoRepository : ICryptoRepository
    {
        private readonly CryptoDbContext _context;

        public CryptoRepository(CryptoDbContext context)
        {
            _context = context;
        }

        public async Task<CryptoCurrency?> GetBySymbolAsync(string symbol)
        {
            return await _context.Cryptocurrencies
                                .FirstOrDefaultAsync(c => c.Symbol == symbol);
        }

        public async Task AddAsync(CryptoCurrency crypto)
        {
            await _context.Cryptocurrencies.AddAsync(crypto);
            await _context.SaveChangesAsync();
        }
    }
}
