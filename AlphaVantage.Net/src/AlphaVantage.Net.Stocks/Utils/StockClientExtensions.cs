using System;
using AlphaVantage.Net.Core;
using AlphaVantage.Net.Core.Exceptions;
using AlphaVantage.Net.Stocks.TimeSeries;

namespace AlphaVantage.Net.Stocks.Utils
{
    internal static class StockClientExtensions
    {
        public static string ConvertToString(this TimeSeriesSize sizeEnum)
        {
            return sizeEnum == TimeSeriesSize.Compact ? "compact" : "full";
        }

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

        public static ApiFunction ConvertToApiFunction(this TimeSeriesType timeSeriesType, bool isAdjusted)
        {
            return (timeSeriesType, isAdjusted) switch
            {
                (TimeSeriesType.Daily, false) => ApiFunction.TIME_SERIES_DAILY,
                (TimeSeriesType.Daily, true) => ApiFunction.TIME_SERIES_DAILY_ADJUSTED,
                
                (TimeSeriesType.Weekly, false) => ApiFunction.TIME_SERIES_WEEKLY,
                (TimeSeriesType.Weekly, true) => ApiFunction.TIME_SERIES_WEEKLY_ADJUSTED,
                
                (TimeSeriesType.Monthly, false) => ApiFunction.TIME_SERIES_MONTHLY,
                (TimeSeriesType.Monthly, true) => ApiFunction.TIME_SERIES_MONTHLY_ADJUSTED,
                
                // unreachable
                _ => throw new AlphaVantageException(
                    $"Unpredictable combination: seriesType = {timeSeriesType}, isAdjusted = {isAdjusted}")
            };
        }
    }
}