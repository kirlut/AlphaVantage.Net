using System;
using System.Text.Json.Serialization;

namespace AlphaVantage.Net.Common.Currencies
{
    public abstract class ExchangeRateBase
    {
        [JsonPropertyName("From_Currency Name")]
        public string FromCurrencyName { get; set; } = String.Empty;

        [JsonPropertyName("To_Currency Code")]
        public PhysicalCurrency ToCurrencyCode { get; set; }
        
        [JsonPropertyName("To_Currency Name")]
        public string ToCurrencyName { get; set; } = String.Empty;
        
        [JsonPropertyName("Exchange Rate")]
        public decimal ExchangeRate { get; set; }
        
        [JsonPropertyName("Last Refreshed")]
        public DateTime LastRefreshed { get; set; }
        
        [JsonPropertyName("Time Zone")]
        public string TimeZone { get; set; } = String.Empty;
        
        [JsonPropertyName("Bid Price")]
        public decimal BidPrice { get; set; }
        
        [JsonPropertyName("Ask Price")]
        public decimal AskPrice { get; set; }
    }
}