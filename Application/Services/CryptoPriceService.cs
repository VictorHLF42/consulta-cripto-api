using Domain;
using Domain.Interfaces;
using FluentResults;
using Infra.ExternalServices;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic; 
using System.Threading.Tasks;

namespace Application.Services
{
    public class CryptoPriceService
    {
        private readonly ICryptoRepository _cryptoRepository;
        private readonly CoinMarketCapClient _coinMarketCapClient;
        private readonly IMemoryCache _cache;

        public CryptoPriceService(ICryptoRepository cryptoRepository, CoinMarketCapClient coinMarketCapClient, IMemoryCache cache)
        {
            _cryptoRepository = cryptoRepository;
            _coinMarketCapClient = coinMarketCapClient;
            _cache = cache;
        }

        public async Task<Result<CryptoCurrency>> GetPriceBySymbolAsync(string symbol)
        {
            if (_cache.TryGetValue<CryptoCurrency>(symbol, out CryptoCurrency cryptoCurrency))
            {
                return Result.Ok(cryptoCurrency);
            }
            
            var result = await _coinMarketCapClient.GetPriceFromExternalApiAsync(symbol);

            if (result.IsSuccess)
            {
                var newCrypto = new CryptoCurrency
                {
                    Symbol = symbol,
                    Price = result.Value,
                };

                await _cryptoRepository.AddAsync(newCrypto);

                _cache.Set(symbol, newCrypto, new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(5)));

                return Result.Ok(newCrypto);
            }

            return Result.Fail("Criptomoeda não encontrada.");
        }
        public async Task<Result<List<CryptoCurrency>>> GetHistoryBySymbolAsync(string symbol, DateTime? dateFrom, DateTime? dateTo)
        {
            var history = await _cryptoRepository.GetHistoryBySymbolAsync(symbol, dateFrom, dateTo);

            
            if (history == null || history.Count == 0)
            {
                return Result.Fail("Nenhum registro encontrado para esta criptomoeda.");
            }
            return Result.Ok(history);
        }
    }
}