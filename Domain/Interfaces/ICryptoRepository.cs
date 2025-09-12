using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ICryptoRepository
    {
        Task<CryptoCurrency?> GetBySymbolAsync(string symbol);
        Task AddAsync(CryptoCurrency crypto);

        //Task<List<CryptoCurrency>> GetHistoryBySymbolAsync(string symbol, DateTime? dateFrom = null, DateTime? dateTo = null);
    }
}
