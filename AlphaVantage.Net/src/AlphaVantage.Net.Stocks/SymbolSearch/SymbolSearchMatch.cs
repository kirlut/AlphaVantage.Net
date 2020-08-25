using System;

namespace AlphaVantage.Net.Stocks.SymbolSearch
{
    public class SymbolSearchMatch
    {
        public string Symbol { get; set; } = String.Empty;
        
        public string Name { get; set; } = String.Empty;
        
        public string Type { get; set; } = String.Empty;
        
        public string Region { get; set; } = String.Empty;

        public string MarketOpen { get; set; } = String.Empty;
        
        public string MarketClose { get; set; } = String.Empty;
        
        public string Timezone { get; set; } = String.Empty;
        
        public string Currency { get; set; } = String.Empty;
        
        public decimal MatchScore { get; set; }
    }
}