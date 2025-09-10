using Domain;
using Domain.Interfaces;
using FluentResults;
using Infra.ExternalServices;

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

        public async Task<Result<decimal>> GetPriceBySymbolAsync(string symbol)
        {
            var crypto = await _cryptoRepository.GetBySymbolAsync(symbol);

            if (crypto != null)
            {
                return Result.Ok(crypto.Price);
            }

            var result = await _coinMarketCapClient.GetPriceFromExternalApiAsync(symbol);

            Console.WriteLine(result + "vai");

            if (!result.IsSuccess)
            {
                return result;
            }

            var newCrypto = new CryptoCurrency
            {
                Symbol = symbol,
                Price = result.Value,
            };

            await _cryptoRepository.AddAsync(newCrypto);

            return Result.Ok(newCrypto.Price);
        }
    }
}