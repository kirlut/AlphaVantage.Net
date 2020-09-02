using System;

namespace AlphaVantage.Net.Stocks.TimeSeries
{
    public class StockTimeSeries : TimeSeriesBase
    {
    public TimeSeriesType Type { get; set; }

    public bool IsAdjusted { get; set; }


    #region Obsolete

    [Obsolete("This field is no longer in use in 2.0 and will be removed in 2.1. " +
              "Consider to get metadata from the 'Metadata' field")]
    public DateTime LastRefreshed { get; set; }

    [Obsolete("This field is no longer in use in 2.0 and will be removed in 2.1. " +
              "Consider to get metadata from the 'Metadata' field")]
    public string Symbol { get; set; } = String.Empty;

    #endregion

    }
}