using System;
using System.Text.Json.Serialization;
using AlphaVantage.Net.Common.Currencies;
using JetBrains.Annotations;

namespace AlphaVantage.Net.Crypto
{
    [UsedImplicitly]
    public class CryptoRating
    {
        [JsonPropertyName("symbol")]
        public DigitalCurrency Symbol { get; set; }
        
        [JsonPropertyName("name")]
        public string Name { get; set; } = String.Empty;
        
        [JsonPropertyName("fcas rating")]
        public FcasRating Rating { get; set; }
        
        [JsonPropertyName("fcas score")]
        public int  FcasScore { get; set; }
        
        [JsonPropertyName("developer score")]
        public int DeveloperScore { get; set; }
        
        [JsonPropertyName("market maturity score")]
        public int MarketMaturityScore { get; set; }
        
        [JsonPropertyName("utility score")]
        public int UtilityScore { get; set; }
        
        [JsonPropertyName("last refreshed")]
        public DateTime LastRefreshed { get; set; }
        
        [JsonPropertyName("timezone")]
        public string TimeZone { get; set; } = String.Empty;
    }
}