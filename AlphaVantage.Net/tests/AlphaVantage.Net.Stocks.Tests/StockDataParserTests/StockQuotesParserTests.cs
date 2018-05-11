using System.IO;
using System.Linq;
using AlphaVantage.Net.Stocks.Parsing;
using AlphaVantage.Net.Stocks.TimeSeries;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;

namespace AlphaVantage.Net.Stocks.Tests.StockDataParserTests
{
    public class StockQuotesParserTests
    {
        [Fact]
        public void StockQuotes_ParsingTest()
        {
            var json = File.ReadAllText("Data/stock-quotes.json");
            var jObject = (JObject) JsonConvert.DeserializeObject(json);

            var parser = new StockDataParser();
            var result = parser.ParseStockQuotes(jObject);
            
            Assert.NotNull(result);
            Assert.Equal(3, result.Count);
            Assert.True(
                result.Any(r => r.Symbol == "MSFT") &&
                result.Any(r => r.Symbol == "FB") && 
                result.Any(r => r.Symbol == "AAPL"));
        }
    }
}