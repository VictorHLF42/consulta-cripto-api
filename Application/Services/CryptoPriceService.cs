using Domain.Interfaces;
using System.Threading.Tasks;
using Infra.ExternalServices;
using Domain;
using System;
using FluentResults;

namespace Application.Services
{
    public class CryptoPriceService
    {
        private readonly ICryptoRepository _cryptoRepository;
        private readonly CoinMarketCapClient _coinMarketCapClient;

        public CryptoPriceService(ICryptoRepository cryptoRepository, CoinMarketCapClient coinMarketCapClient)
        {
            _cryptoRepository = cryptoRepository;
            _coinMarketCapClient = coinMarketCapClient;
        }

        public async Task<Result<decimal?>> GetPriceBySymbolAsync(string symbol)
        {
            var crypto = await _cryptoRepository.GetBySymbolAsync(symbol);

            if (crypto != null)
            {
                return Result.Ok<decimal?>(crypto.Price);
            }

            var externalPrice = await _coinMarketCapClient.GetPriceFromExternalApiAsync(symbol);

            if (externalPrice != null)
            {
                var newCrypto = new CryptoCurrency
                {
                    Symbol = symbol,
                    Price = externalPrice.Value,
                };

                await _cryptoRepository.AddAsync(newCrypto);

                return Result.Ok<decimal?>(newCrypto.Price);
            }
            return Result.Fail("Criptomoeda não encontrada.");
        }
    }
}