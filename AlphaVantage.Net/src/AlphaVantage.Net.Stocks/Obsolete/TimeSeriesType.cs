using System;
// ReSharper disable CheckNamespace

namespace AlphaVantage.Net.Stocks.TimeSeries
{
    [Obsolete("This class is from the old version of AlfaVantage.Net library and will be removed in version 2.1. " +
              "Consider using classes from the latest version: https://github.com/LutsenkoKirill/AlphaVantage.Net")]

    public enum TimeSeriesType
    {
        Daily,
        Weekly,
        Monthly,
        Intraday
    }
}