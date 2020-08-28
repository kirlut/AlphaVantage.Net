using System;
using JetBrains.Annotations;

namespace AlphaVantage.Net.Stocks
{
    [UsedImplicitly]
    public class GlobalQuote
    {
        public string Symbol { get; set; } = String.Empty;
        
        public decimal OpeningPrice {get; set;}
        
        public decimal PreviousClosingPrice {get; set;}

        public decimal HighestPrice {get; set;}
        
        public decimal LowestPrice {get; set;}
        
        public decimal Price {get; set;}
        
        public long Volume {get; set;}
        
        public DateTime LatestTradingDay {get; set;}
        
        public decimal Change {get; set;}

        public decimal ChangePercent {get; set;}
    }
}