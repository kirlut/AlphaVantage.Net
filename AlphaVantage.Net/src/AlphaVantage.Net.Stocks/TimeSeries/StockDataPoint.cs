using AlphaVantage.Net.Common.TimeSeries;

namespace AlphaVantage.Net.Stocks.TimeSeries
{
    /// <summary>
    /// Represent single element of time series
    /// </summary>
    public class StockDataPoint : DataPointBase
    {
        public decimal OpeningPrice {get; set;}
        
        public decimal ClosingPrice {get; set;}
        
        public decimal HighestPrice {get; set;}
        
        public decimal LowestPrice {get; set;}
        
        public long Volume {get; set;}
    }
}