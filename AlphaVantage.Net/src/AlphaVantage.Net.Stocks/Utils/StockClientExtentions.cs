using System;
using AlphaVantage.Net.Stocks.TimeSeries;

namespace AlphaVantage.Net.Stocks.Utils
{
    internal static class StockClientExtentions
    {
        public static string ConvertToString(this TimeSeriesSize sizeEnum)
        {
            if(sizeEnum == TimeSeriesSize.Compact)
                    return "compact";

            return "full";
        }

        public static string ConvertToString(this IntradayInterval interval)
        {
            switch (interval)
            {
                case IntradayInterval.OneMin:
                    return "1min";
                case IntradayInterval.FiveMin:
                    return "5min";
                case IntradayInterval.FifteenMin:
                    return "15min";
                case IntradayInterval.ThirtyMin:
                    return "30min";
                case IntradayInterval.SixtyMin:
                    return "60min";
                    
                //unreachable:
                default:
                    return String.Empty;
            }
        }
    }
}