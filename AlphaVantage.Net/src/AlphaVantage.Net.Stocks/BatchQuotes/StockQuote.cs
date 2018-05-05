using System;

namespace AlphaVantage.Net.Stocks.BatchQuotes
{
    public class StockQuote
    {
        public string Symbol {get; set;}
        
        public DateTime Time {get; set;}
        
        public double Price {get; set;}
        
        public long Volume {get; set;}
    }
}