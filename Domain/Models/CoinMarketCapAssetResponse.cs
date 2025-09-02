using System.Text.Json.Serialization;

public class CoinMarketCapAssetResponse
{
    [JsonPropertyName("data")]
    public Dictionary<string, Asset[]> Data { get; set; }

    public class Asset
    {
        
        [JsonPropertyName("quote")]
        public Dictionary<string, AssetInformation> Quote { get; set; }
    }

    public class AssetInformation
    {
        [JsonPropertyName("price")]
        public decimal? Price { get; set; }
    }
}