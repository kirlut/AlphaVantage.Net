using System;

namespace AlphaVantage.Net.Stocks.TimeSeries
{
    /// <summary>
    /// Represent single element of time series
    /// </summary>
    public class StockDataPoint
    {
        internal StockDataPoint(
            DateTime time, 
            StockTimeSeries timeSeries, 
            double openingPrice, 
            double closingPrice,
            double highestPrice, 
            double lowestPrice, 
            long volume)
        {
            Time = time;
            TimeSeries = timeSeries;
            OpeningPrice = openingPrice;
            ClosingPrice = closingPrice;
            HighestPrice = highestPrice;
            LowestPrice = lowestPrice;
            Volume = volume;
        }

        public DateTime Time { get; }
        
        public StockTimeSeries TimeSeries { get; }
        
        public double OpeningPrice { get; }
        
        public double ClosingPrice { get; }
        
        public double HighestPrice { get; }
        
        public double LowestPrice { get; }
        
        public long Volume { get; }
    }
}