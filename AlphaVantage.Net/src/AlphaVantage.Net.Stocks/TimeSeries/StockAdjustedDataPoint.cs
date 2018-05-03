using System;

namespace AlphaVantage.Net.Stocks.TimeSeries
{
    /// <summary>
    /// Represent single adjusted element of time series
    /// </summary>
    public sealed class StockAdjustedDataPoint : StockDataPoint
    {
        internal StockAdjustedDataPoint(
            DateTime time, 
            StockTimeSeries timeSeries, 
            double openingPrice, 
            double closingPrice, 
            double highestPrice, 
            double lowestPrice, 
            long volume,
            double adjustedClosingPrice,
            double dividendAmount,
            double splitCoefficient) 
            : base(time, timeSeries, openingPrice, closingPrice, highestPrice, lowestPrice, volume)
        {
            AdjustedClosingPrice = adjustedClosingPrice;
            DividendAmount = dividendAmount;
            SplitCoefficient = splitCoefficient;
        }
        
        public double AdjustedClosingPrice { get; }
        
        public double DividendAmount { get; }
        
        public double SplitCoefficient { get; }
    }
}