using System;
using System.Collections.Generic;
// ReSharper disable CheckNamespace

namespace AlphaVantage.Net.Stocks.TimeSeries
{
    [Obsolete("This class is from the old version of AlfaVantage.Net library and will be removed in version 3.1. " +
              "Consider using classes from the latest version: https://github.com/LutsenkoKirill/AlphaVantage.Net")]
    public class StockTimeSeries
    {
        public TimeSeriesType Type {get; set;}
        
        public bool IsAdjusted {get; set;}
        
        public ICollection<StockDataPoint> DataPoints {get; set;} = new List<StockDataPoint>();

        public Dictionary<string, string> MetaData { get; set; } = new Dictionary<string, string>();
        
        #region Obsolete
        [Obsolete("This field is no longer in use in 3.0 and will be removed in 3.1. " +
                  "Consider to get metadata from 'Metadata' field")]
        public DateTime LastRefreshed {get; set;}
        
        [Obsolete("This field is no longer in use in 3.0 and will be removed in 3.1. " +
                  "Consider to get metadata from 'Metadata' field")]
        public string Symbol {get; set;} = String.Empty;
        #endregion        
    }
}