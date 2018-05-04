using System;
using System.Collections.Generic;

namespace AlphaVantage.Net.Stocks.TimeSeries
{
    public class StockTimeSeries
    {
        public TimeSeriesType Type {get; set;}
        
        public TimeSeriesSize Size {get; set;}
        
        public DateTime LastRefreshed {get; set;}
        
        public string Symbol {get; set;}
        
        public bool IsAdjusted {get; set;}
        
        public ICollection<StockDataPoint> Elements {get; set;}
    }
}