using System;
using AlphaVantage.Net.Common;
using AlphaVantage.Net.Common.Intervals;

namespace AlphaVantage.Net.Forex.Client
{
    internal static class IntervalExtensions
    {
        public static ApiFunction ConvertToApiFunction(this Interval interval)
        {
            switch (interval)
            {
                case Interval.Daily:
                    return ApiFunction.FX_DAILY;
                case Interval.Weekly:
                    return ApiFunction.FX_WEEKLY;
                case Interval.Monthly:
                    return ApiFunction.FX_MONTHLY;
                default:
                    return ApiFunction.FX_INTRADAY;
            }
        }
    }
}