using AlphaVantage.Net.Common.TimeSeries;

namespace AlphaVantage.Net.Forex
{
    public class ForexDataPoint : DataPointBase
    {
        public decimal OpeningPrice {get; set;}
        
        public decimal ClosingPrice {get; set;}
        
        public decimal HighestPrice {get; set;}
        
        public decimal LowestPrice {get; set;}
    }
}