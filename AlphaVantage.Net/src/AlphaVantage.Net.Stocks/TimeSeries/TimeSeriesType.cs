using System;

namespace AlphaVantage.Net.Stocks.TimeSeries
{
    public enum TimeSeriesType
    {
        Daily,
        Weekly,
        Monthly,
        
        [Obsolete("This enum value is no longer in use in 2.0 and will be removed in 2.1")]
        Intraday
    }
}