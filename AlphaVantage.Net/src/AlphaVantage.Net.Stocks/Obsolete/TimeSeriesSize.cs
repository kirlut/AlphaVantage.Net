// ReSharper disable CheckNamespace

using System;
#pragma warning disable 618

namespace AlphaVantage.Net.Stocks.TimeSeries
{
    [Obsolete("This class is from the old version of AlfaVantage.Net library and will be removed in version 2.1. " +
              "Consider using classes from the latest version: https://github.com/LutsenkoKirill/AlphaVantage.Net")]
    public enum TimeSeriesSize
    {
        /// <summary>
        /// Latest 100 data points
        /// </summary>
        Compact,
        
        /// <summary>
        /// Full-length time series
        /// </summary>
        Full
    }
    
    internal static class OutputSizeExtensions
    {
        public static string ConvertToString(this TimeSeriesSize sizeEnum)
        {
            return sizeEnum == TimeSeriesSize.Compact ? "compact" : "full";
        }
    }
}