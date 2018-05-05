namespace AlphaVantage.Net.Stocks.Parsing.JsonTokens
{
    internal static class TimeSeriesJsonTokens
    {
        public const string TimeSeries1MinHeader = "Time Series (1min)";
        public const string TimeSeries5MinHeader = "Time Series (5min)";
        public const string TimeSeries15MinHeader = "Time Series (15min)";
        public const string TimeSeries30MinHeader = "Time Series (30min)";
        public const string TimeSeries60MinHeader = "Time Series (60min)";
        public const string TimeSeriesDailyHeader = "Time Series (Daily)";
        public const string TimeSeriesWeeklyHeader = "Weekly Time Series";
        public const string TimeSeriesWeeklyAdjustedHeader = "Weekly Adjusted Time Series";
        public const string TimeSeriesMonthlyHeader = "Monthly Time Series";
        public const string TimeSeriesAdjustedHeader = "Monthly Adjusted Time Series";

        public const string OpeningPriceToken = "1. open";
        public const string HighestPriceToken = "2. high";
        public const string LowestPriceToken = "3. low";
        public const string ClosingPriceToken = "4. close";
        public const string VolumeNonAdjustedToken = "5. volume";
        
        public const string VolumeAdjustedToken = "6. volume";
        public const string AdjustedClosingPriceToken = "5. adjusted close";
        public const string DividendAmountToken = "7. dividend amount";
        public const string DailySplitCoefficientToken = "8. split coefficient";
    }
}