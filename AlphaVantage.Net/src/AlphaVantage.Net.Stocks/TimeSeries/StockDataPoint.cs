using System;

namespace AlphaVantage.Net.Stocks.TimeSeries
{
    /// <summary>
    /// Represent single element of time series
    /// </summary>
    public class StockDataPoint
    {
        public DateTime Time {get; set;}
        
        public StockTimeSeries TimeSeries {get; set;}
        
        public double OpeningPrice {get; set;}
        
        public double ClosingPrice {get; set;}
        
        public double HighestPrice {get; set;}
        
        public double LowestPrice {get; set;}
        
        public long Volume {get; set;}
    }
}