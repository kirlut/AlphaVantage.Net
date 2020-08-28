using System.Text.Json;
using System.Text.Json.Serialization;
using AlphaVantage.Net.Core.Parsing;
using JetBrains.Annotations;

namespace AlphaVantage.Net.Stocks.Parsing
{
    public class GlobalQuoteParser : IAlphaVantageJsonParser<GlobalQuote?>
    {
        public GlobalQuote? ParseApiResponse(string jsonString)
        {
            var quoteWrapper = JsonSerializer.Deserialize<GlobalQuoteWrapper>(jsonString);
            return quoteWrapper.GlobalQuote;
        }

        [UsedImplicitly]
        private class GlobalQuoteWrapper
        {
            [JsonPropertyName("Global Quote")]
            public GlobalQuote? GlobalQuote { get; set; }
        }
    }
}