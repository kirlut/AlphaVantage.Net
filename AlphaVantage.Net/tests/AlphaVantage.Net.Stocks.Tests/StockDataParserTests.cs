using System;
using System.IO;
using AlphaVantage.Net.Stocks.Parsing;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;

namespace AlphaVantage.Net.Stocks.Tests
{
    public class StockDataParserTests
    {
        [Fact]
        public void Test1()
        {
            var json = File.ReadAllText("time-series-not-adjusted.json");
            var jObject = (JObject) JsonConvert.DeserializeObject(json);

            var parser = new StockDataParser();
            var result = parser.ParseTimeSeries(jObject);
            
            Assert.NotNull(result);
        }
    }
}