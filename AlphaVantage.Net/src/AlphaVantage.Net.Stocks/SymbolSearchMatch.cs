using System;
using JetBrains.Annotations;
using NodaTime;

namespace AlphaVantage.Net.Stocks
{
    [UsedImplicitly]
    public class SymbolSearchMatch
    {
        public string Symbol { get; set; } = String.Empty;
        
        public string Name { get; set; } = String.Empty;
        
        public string Type { get; set; } = String.Empty;
        
        public string Region { get; set; } = String.Empty;

        public LocalTime MarketOpen { get; set; }
        
        public LocalTime MarketClose { get; set; }
        
        public string Timezone { get; set; } = String.Empty;
        
        public string Currency { get; set; } = String.Empty;
        
        public decimal MatchScore { get; set; }
    }
}