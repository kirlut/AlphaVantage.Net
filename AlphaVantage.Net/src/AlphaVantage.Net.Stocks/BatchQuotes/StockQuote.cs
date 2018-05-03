using System;

namespace AlphaVantage.Net.Stocks.BatchQuotes
{
    public class StockQuote
    {
        internal StockQuote(string symbol, DateTime time, double price, long volume)
        {
            Symbol = symbol;
            Time = time;
            Price = price;
            Volume = volume;
        }

        public string Symbol { get; }
        
        public DateTime Time { get; }
        
        public double Price { get; }
        
        public long Volume { get; }
    }
}