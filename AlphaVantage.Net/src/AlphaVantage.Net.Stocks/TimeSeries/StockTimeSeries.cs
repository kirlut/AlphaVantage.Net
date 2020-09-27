using System;
using System.Collections.Generic;
using AlphaVantage.Net.Common.Intervals;

namespace AlphaVantage.Net.Stocks.TimeSeries
{
    public class StockTimeSeries
    {
        public Interval Interval { get; set; }
        
        public bool IsAdjusted { get; set; }

        public ICollection<StockDataPoint> DataPoints {get; set;} = new List<StockDataPoint>();

        public Dictionary<string, string> MetaData { get; set; } = new Dictionary<string, string>();

        #region Obsolete
        [Obsolete("This field is no longer in use in 2.0 and will be removed in 2.1. " +
                  "Consider to get metadata from the 'Metadata' field")]
        public TimeSeriesType Type { get; set; }

        [Obsolete("This field is no longer in use in 2.0 and will be removed in 2.1. " +
                  "Consider to get metadata from the 'Metadata' field")]
        public DateTime LastRefreshed { get; set; }

        [Obsolete("This field is no longer in use in 2.0 and will be removed in 2.1. " +
                  "Consider to get metadata from the 'Metadata' field")]
        public string Symbol { get; set; } = String.Empty;

        #endregion
    }
}