using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;

namespace Infra.ExternalServices
{
    public class CoinMarketCapClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public CoinMarketCapClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<decimal?> GetPriceFromExternalApiAsync(string symbol)
        {
            var response = await _httpClient.GetAsync($"https://pro-api.coinmarketcap.com/v2/cryptocurrency/quotes/latest?symbol={symbol}");

            if (response.IsSuccessStatusCode)
            {
                var assetResponse = await response.Content.ReadFromJsonAsync<CoinMarketCapAssetResponse>();
                return assetResponse.Data[symbol.ToUpper()][0].Quote["USD"].Price;

            }
            return null;
        }

    }
}