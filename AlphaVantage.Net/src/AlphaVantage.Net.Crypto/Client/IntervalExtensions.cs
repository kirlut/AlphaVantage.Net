using AlphaVantage.Net.Common;
using AlphaVantage.Net.Common.Exceptions;
using AlphaVantage.Net.Common.Intervals;

namespace AlphaVantage.Net.Crypto.Client
{
    internal static class IntervalExtensions
    {
        public static ApiFunction ConvertToApiFunction(this Interval interval)
        {
            switch (interval)
            {
                case Interval.Daily:
                    return ApiFunction.DIGITAL_CURRENCY_DAILY;
                case Interval.Weekly:
                    return ApiFunction.DIGITAL_CURRENCY_WEEKLY;
                case Interval.Monthly:
                    return ApiFunction.DIGITAL_CURRENCY_MONTHLY;
                default:
                    throw new AlphaVantageException(
                        "Cryptocurrencies time series support only non-intraday intervals: daily, weekly, monthly");
            }
        }
    }
}