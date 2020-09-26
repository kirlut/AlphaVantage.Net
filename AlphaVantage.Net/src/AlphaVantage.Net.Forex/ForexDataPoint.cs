using System;

namespace AlphaVantage.Net.Forex
{
    public class ForexDataPoint
    {
        public DateTime Time {get; set;}
                
        public decimal OpeningPrice {get; set;}
        
        public decimal ClosingPrice {get; set;}
        
        public decimal HighestPrice {get; set;}
        
        public decimal LowestPrice {get; set;}
    }
}