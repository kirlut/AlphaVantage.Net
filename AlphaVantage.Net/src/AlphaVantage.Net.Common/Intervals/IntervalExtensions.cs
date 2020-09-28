using System;
using System.Collections.Generic;

namespace AlphaVantage.Net.Common.Intervals
{
    public static class IntervalExtensions
    {
        private static readonly HashSet<Interval> IntradayIntervals = new HashSet<Interval>()
        {
            Interval.Min1, Interval.Min5, Interval.Min15, Interval.Min30, Interval.Min60
        };
        
        public static string ConvertToQueryString(this Interval interval)
        {
            return interval switch
            {
                Interval.Min1 => "1min",
                Interval.Min5 => "5min",
                Interval.Min15 => "15min",
                Interval.Min30 => "30min",
                Interval.Min60 => "60min",
                Interval.Daily => "daily",
                Interval.Weekly => "weekly",
                Interval.Monthly => "monthly",
                _ => String.Empty
            };
        }

        public static bool IsIntraday(this Interval interval)
        {
            return IntradayIntervals.Contains(interval);
        }
    }
}