using System;
#pragma warning disable 618

// ReSharper disable once CheckNamespace
namespace AlphaVantage.Net.Stocks.TimeSeries
{
    [Obsolete("This class is from the old version of AlfaVantage.Net library and will be removed in version 2.1. " +
              "Consider using classes from the latest version: https://github.com/LutsenkoKirill/AlphaVantage.Net")]
    public enum IntradayInterval
    {
        OneMin, 
        FiveMin, 
        FifteenMin, 
        ThirtyMin, 
        SixtyMin
    }

    internal static class IntradayTimeSeriesExtension
    {
        [Obsolete("This method is from the old version of AlfaVantage.Net library and will be removed in version 2.1. " +
                  "Consider using classes from the latest version: https://github.com/LutsenkoKirill/AlphaVantage.Net")]
        public static string ConvertToString(this IntradayInterval interval)
        {
            return interval switch
            {
                IntradayInterval.OneMin => "1min",
                IntradayInterval.FiveMin => "5min",
                IntradayInterval.FifteenMin => "15min",
                IntradayInterval.ThirtyMin => "30min",
                IntradayInterval.SixtyMin => "60min",
                _ => String.Empty
            };
        }
    }
}