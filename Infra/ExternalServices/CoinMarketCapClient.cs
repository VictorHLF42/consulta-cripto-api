using FluentResults;
using System.Net.Http.Json;

namespace Infra.ExternalServices
{
    public class CoinMarketCapClient
    {
        private readonly HttpClient _httpClient;

        public CoinMarketCapClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Result<decimal>> GetPriceFromExternalApiAsync(string symbol)
        {
            var response = await _httpClient.GetAsync($"https://pro-api.coinmarketcap.com/v2/cryptocurrency/quotes/latest?symbol={symbol}");

            if (!response.IsSuccessStatusCode)
            {
                return Result.Fail(response.ReasonPhrase);
            }

            var assetResponse = await response.Content.ReadFromJsonAsync<CoinMarketCapAssetResponse>();

            var currencyInfo = assetResponse.Data[symbol.ToUpper()];
            if (currencyInfo.Count() == 0)
            {
                return Result.Fail("Criptomoeda não encontrada.");
            }

            return Result.Ok(currencyInfo[0].Quote["USD"].Price.Value);
        }
    }
}