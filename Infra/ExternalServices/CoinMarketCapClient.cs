using FluentResults;
using System.Net.Http.Json;
using System.Linq;

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

            if (!assetResponse.Data.ContainsKey(symbol.ToUpper()))
            {
                return Result.Fail("Símbolo não encontrado na resposta da API.");
            }

            var currencyInfo = assetResponse.Data[symbol.ToUpper()];

            if (currencyInfo.Count() == 0 || currencyInfo[0].Quote.Count == 0 || !currencyInfo[0].Quote.ContainsKey("USD"))
            {
                return Result.Fail("Criptomoeda não encontrada ou sem dados de cotação em USD.");
            }

            var quote = currencyInfo[0].Quote["USD"];

            if (!quote.Price.HasValue)
            {
                return Result.Fail("Preço da criptomoeda não disponível.");
            }

            return Result.Ok(quote.Price.Value);
        }
    }
}