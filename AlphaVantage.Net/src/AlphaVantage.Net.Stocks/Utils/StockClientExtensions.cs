using System;
using AlphaVantage.Net.Core;
using AlphaVantage.Net.Core.Exceptions;
using AlphaVantage.Net.Core.Intervals;
using AlphaVantage.Net.Stocks.TimeSeries;

namespace AlphaVantage.Net.Stocks.Utils
{
    internal static class StockClientExtensions
    {
        public static string ConvertToString(this TimeSeriesSize sizeEnum)
        {
            return sizeEnum == TimeSeriesSize.Compact ? "compact" : "full";
        }

        public static ApiFunction ConvertToApiFunction(this Interval interval, bool isAdjusted)
        {
            return (timeSeriesType: interval, isAdjusted) switch
            {
                (Interval.Daily, false) => ApiFunction.TIME_SERIES_DAILY,
                (Interval.Daily, true) => ApiFunction.TIME_SERIES_DAILY_ADJUSTED,
                
                (Interval.Weekly, false) => ApiFunction.TIME_SERIES_WEEKLY,
                (Interval.Weekly, true) => ApiFunction.TIME_SERIES_WEEKLY_ADJUSTED,
                
                (Interval.Monthly, false) => ApiFunction.TIME_SERIES_MONTHLY,
                (Interval.Monthly, true) => ApiFunction.TIME_SERIES_MONTHLY_ADJUSTED,
                
                _ => ApiFunction.TIME_SERIES_INTRADAY
            };
        }
    }
}