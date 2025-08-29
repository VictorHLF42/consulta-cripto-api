using Application.ExternalServices;
using Domain;
using Domain.Interfaces;
using System;
using System.ComponentModel.Design;
using System.Threading.Tasks;

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

        public async Task<decimal?> GetPriceBySymbolAsync(string symbol)
        {
           
            var crypto = await _cryptoRepository.GetBySymbolAsync(symbol);

            if (crypto != null)
            {
              
                return crypto.Price;
            }

           
            var externalPrice = await _coinMarketCapClient.GetPriceFromExternalApiAsync(symbol);

            if (externalPrice != null)
            {
                var newCrypto = new CryptoCurrency
                {
                    Symbol = symbol,
                    Price = externalPrice.Value,
                    CreatedAt = DateTime.UtcNow
                };

           

            

                return newCrypto.Price;
            }

         
            return null;
        }
    }
}