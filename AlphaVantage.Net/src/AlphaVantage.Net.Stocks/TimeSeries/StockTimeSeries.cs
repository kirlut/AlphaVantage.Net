using System;
using System.Collections.Generic;

namespace AlphaVantage.Net.Stocks.TimeSeries
{
    public class StockTimeSeries
    {
        internal StockTimeSeries(
            TimeSeriesType type, 
            TimeSeriesSize size,
            DateTime lastRefreshed, 
            string symbol, 
            bool isAdjusted,
            ICollection<StockDataPoint> elements)
        {
            Type = type;
            Size = size;
            LastRefreshed = lastRefreshed;
            Symbol = symbol;
            IsAdjusted = isAdjusted;
            Elements = elements;
        }

        public TimeSeriesType Type { get; }
        
        public TimeSeriesSize Size { get; }
        
        public DateTime LastRefreshed { get; }
        
        public string Symbol { get; }
        
        public bool IsAdjusted { get; }
        
        public ICollection<StockDataPoint> Elements { get; }
    }
}