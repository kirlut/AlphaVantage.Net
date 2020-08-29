using System;
using System.Collections.Generic;
// ReSharper disable CheckNamespace

namespace AlphaVantage.Net.Stocks.TimeSeries
{
    public class StockTimeSeries
    {
        public TimeSeriesType Type { get; set; } = TimeSeriesType.Unknown;
        
        public bool IsAdjusted {get; set;}
        
        public ICollection<StockDataPoint> DataPoints {get; set;} = new List<StockDataPoint>();

        public Dictionary<string, string> MetaData { get; set; } = new Dictionary<string, string>();
        
        #region Obsolete
        [Obsolete("This field is no longer in use in 2.0 and will be removed in 2.1. " +
                  "Consider to get metadata from 'Metadata' field")]
        public DateTime LastRefreshed {get; set;}
        
        [Obsolete("This field is no longer in use in 2.0 and will be removed in 2.1. " +
                  "Consider to get metadata from 'Metadata' field")]
        public string Symbol {get; set;} = String.Empty;
        #endregion        
    }
}