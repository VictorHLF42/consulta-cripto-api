using Domain;
using Domain.Interfaces;
using FluentResults;
using Infra.ExternalServices;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic; // Certifique-se de incluir este using
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

        public async Task<Result<decimal>> GetPriceBySymbolAsync(string symbol)
        {
            // A lógica existente para o primeiro desafio continua aqui
            if (_cache.TryGetValue<decimal>(symbol, out decimal cachedPrice))
            {
                return Result.Ok(cachedPrice);
            }

            var crypto = await _cryptoRepository.GetBySymbolAsync(symbol);

            if (crypto != null)
            {
                _cache.Set(symbol, crypto.Price, new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(10)));

                return Result.Ok(crypto.Price);
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

                _cache.Set(symbol, newCrypto.Price, new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(10)));

                return Result.Ok(newCrypto.Price);
            }

            return Result.Fail("Criptomoeda não encontrada.");
        }

        // Novo método para buscar o histórico de preços
        public async Task<Result<List<CryptoCurrency>>> GetHistoryBySymbolAsync(string symbol, DateTime? dateFrom, DateTime? dateTo)
        {
            // Chamar o repositório para obter os dados históricos
            var history = await _cryptoRepository.GetHistoryBySymbolAsync(symbol, dateFrom, dateTo);

            // Verificar se algum registro foi encontrado
            if (history == null || history.Count == 0)
            {
                // Retornar um resultado de falha se não houver registros
                return Result.Fail("Nenhum registro encontrado para esta criptomoeda.");
            }

            // Retornar a lista de registros se a operação foi bem-sucedida
            return Result.Ok(history);
        }
    }
}