// ReSharper disable InconsistentNaming

using System;

// ReSharper disable once CheckNamespace
namespace AlphaVantage.Net.Stocks.Parsing.JsonTokens
{
    [Obsolete("This class is from the old version of AlfaVantage.Net library and will be removed in version 2.1. " +
              "Consider using classes from the latest version: https://github.com/LutsenkoKirill/AlphaVantage.Net")]
    internal static class TimeSeriesJsonTokens
    {
        public const string TimeSeriesHeader = "Time Series";

        public const string AdjustedToken_1 = "Daily Time Series with Splits and Dividend Events";
        public const string AdjustedToken_2 = "Adjusted";
        
        public const string IntradayTimeSeriesTypeToken = "Intraday";
        public const string DailyTimeSeriesTypeToken = "Daily";
        public const string WeeklyTimeSeriesTypeToken = "Weekly";
        public const string MonthlyTimeSeriesTypeToken = "Monthly";

        public const string OpeningPriceToken = "1. open";
        public const string HighestPriceToken = "2. high";
        public const string LowestPriceToken = "3. low";
        public const string ClosingPriceToken = "4. close";
        
        public const string VolumeNonAdjustedToken = "5. volume";
        
        public const string VolumeAdjustedToken = "6. volume";
        public const string AdjustedClosingPriceToken = "5. adjusted close";
        public const string DividendAmountToken = "7. dividend amount";
        public const string SplitCoefficientToken = "8. split coefficient";

    }
}