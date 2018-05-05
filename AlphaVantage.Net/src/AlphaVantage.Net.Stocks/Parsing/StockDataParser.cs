using System.Collections.Generic;
using AlphaVantage.Net.Stocks.BatchQuotes;
using AlphaVantage.Net.Stocks.TimeSeries;
using Newtonsoft.Json.Linq;

namespace AlphaVantage.Net.Stocks.Parsing
{
    internal class StockDataParser
    {
        public StockTimeSeries ParseTimeSeries(JObject jObject, bool isAdjusted)
        {
            //todo:
            return null;
        }

        public ICollection<StockQuote> ParseStockQuotes(JObject jObject)
        {
            //todo:
            return null;
        }
    }
}