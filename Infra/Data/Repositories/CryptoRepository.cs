using Domain.Interfaces;
using Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

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

        public async Task<List<CryptoCurrency>> GetHistoryBySymbolAsync(string symbol, DateTime? dateFrom = null, DateTime? dateTo = null)
        {
            var query = _context.Cryptocurrencies
            .AsNoTracking()
            .Where(c => c.Symbol == symbol);

            if (dateFrom.HasValue)
            {
                query = query.Where(c => c.CreatedAt >= dateFrom.Value);
            }

            if (dateTo.HasValue)
            {
                query = query.Where(c => c.CreatedAt <= dateTo.Value);
            }

            return await query.OrderBy(c => c.CreatedAt).ToListAsync();
        }
    }
}