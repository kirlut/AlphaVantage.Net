using System;
// ReSharper disable CheckNamespace

namespace AlphaVantage.Net.Stocks.Utils
{
    [Obsolete("This class is from the old version of AlfaVantage.Net library and will be removed in version 2.1. " +
              "Consider using classes from the latest version: https://github.com/LutsenkoKirill/AlphaVantage.Net")]
    internal static class StockApiQueryVars
    {
        public const string Symbol = "symbol";
        
        public const string IntradayInterval = "interval";

        public const string OutputSize = "outputsize";
    }
}