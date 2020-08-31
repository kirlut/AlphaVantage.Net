using System;
using System.Text.Json.Serialization;
using JetBrains.Annotations;
using NodaTime;

namespace AlphaVantage.Net.Stocks
{
    [UsedImplicitly]
    public class GlobalQuote
    {
        public string Symbol { get; set; } = String.Empty;
        
        [JsonPropertyName("open")]
        public decimal OpeningPrice {get; set;}
        
        [JsonPropertyName("previous close")]
        public decimal PreviousClosingPrice {get; set;}

        [JsonPropertyName("high")]
        public decimal HighestPrice {get; set;}
        
        [JsonPropertyName("low")]
        public decimal LowestPrice {get; set;}
        
        [JsonPropertyName("price")]
        public decimal Price {get; set;}
        
        [JsonPropertyName("volume")]
        public long Volume {get; set;}
        
        [JsonPropertyName("latest trading day")]
        public LocalDate LatestTradingDay {get; set;}
        
        public decimal Change {get; set;}

        [JsonPropertyName("change percent")]
        public decimal ChangePercent {get; set;}
    }
}