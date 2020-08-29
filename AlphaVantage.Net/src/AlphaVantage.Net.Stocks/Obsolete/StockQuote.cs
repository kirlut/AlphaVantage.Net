using System;
// ReSharper disable CheckNamespace

namespace AlphaVantage.Net.Stocks.BatchQuotes
{
    [Obsolete("This class is from the old version of AlfaVantage.Net library and will be removed in version 2.1. " +
              "Consider using classes from the latest version: https://github.com/LutsenkoKirill/AlphaVantage.Net")]
    public class StockQuote
    {
        public string? Symbol {get; set;}
        
        public DateTime Time {get; set;}
        
        public decimal Price {get; set;}
        
        public long? Volume {get; set;}
    }
}