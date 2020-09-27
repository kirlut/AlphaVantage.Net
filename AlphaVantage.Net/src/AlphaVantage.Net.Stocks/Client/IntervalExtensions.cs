using AlphaVantage.Net.Common;
using AlphaVantage.Net.Common.Intervals;

namespace AlphaVantage.Net.Stocks.Client
{
    internal static class IntervalExtensions
    {
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