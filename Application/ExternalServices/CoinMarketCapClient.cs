using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System.Linq;
using Domain;

namespace Application.ExternalServices
{
    public class CoinMarketCapClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public CoinMarketCapClient(HttpClient httpClient, string apiKey)
        {
            _httpClient = httpClient;
            _apiKey = apiKey;
            _httpClient.DefaultRequestHeaders.Add("X-CMC_PRO_API_KEY", _apiKey);
        }

        public async Task<decimal?> GetPriceFromExternalApiAsync(string symbol)
        {
            var response = await _httpClient.GetAsync($"https://pro-api.coinmarketcap.com/v2/cryptocurrency/quotes/latest?symbol={symbol}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var jsonDocument = JsonDocument.Parse(content);
                var quote = jsonDocument.RootElement.GetProperty("data").EnumerateObject().FirstOrDefault();

                if (quote.Value.GetProperty("quote").TryGetProperty("USD", out var usdQuote))
                {
                    if (usdQuote.TryGetProperty("price", out var priceElement))
                    {
                        return priceElement.GetDecimal();
                    }
                }
            }
            return null;
        }
    }
}