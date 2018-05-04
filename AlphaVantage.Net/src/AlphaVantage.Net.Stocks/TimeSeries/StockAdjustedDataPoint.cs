using System;

namespace AlphaVantage.Net.Stocks.TimeSeries
{
    /// <summary>
    /// Represent single adjusted element of time series
    /// </summary>
    public sealed class StockAdjustedDataPoint : StockDataPoint
    {
        public double AdjustedClosingPrice {get; set;}
        
        public double DividendAmount {get; set;}
        
        public double SplitCoefficient {get; set;}
    }
}