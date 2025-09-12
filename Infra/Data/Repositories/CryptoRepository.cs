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

        // Implementação do novo método de histórico
        public async Task<List<CryptoCurrency>> GetHistoryBySymbolAsync(string symbol, DateTime? dateFrom = null, DateTime? dateTo = null)
        {
            var query = _context.Cryptocurrencies
                                .AsNoTracking()
                                .Where(c => c.Symbol == symbol);

            // Adiciona o filtro de data inicial, se fornecido
            if (dateFrom.HasValue)
            {
                query = query.Where(c => c.CreatedAt >= dateFrom.Value);
            }

            // Adiciona o filtro de data final, se fornecido
            if (dateTo.HasValue)
            {
                query = query.Where(c => c.CreatedAt <= dateTo.Value);
            }

            // Ordena os resultados por data e executa a consulta
            return await query.OrderBy(c => c.CreatedAt).ToListAsync();
        }
    }
}