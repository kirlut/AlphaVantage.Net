using System.Collections.Generic;
using System.Threading.Tasks;
using AlphaVantage.Net.Common;
using AlphaVantage.Net.Common.Parsing;
using AlphaVantage.Net.TestUtils;
using FluentAssertions;
using Xunit;

namespace AlphaVantage.Net.Core.Tests
{
    public class MetaDataExtractorShould
    {
        private readonly string _apiKey = ConfigProvider.Configuration["ApiKey"];
        
        [Fact]
        public async Task ExtractMetaDataFromApiResponse()
        {
            var symbol = "AAPL";
            var interval = "15min";
            
            using var client = new Client.AlphaVantageClient(_apiKey);
            var response = await client.RequestParsedJsonAsync(
                ApiFunction.TIME_SERIES_INTRADAY, 
                new Dictionary<string, string>()
                {
                    {"symbol", symbol},
                    {"interval", interval}
                }, 
                true);

            var metadata = response.ExtractMetaData();

            metadata.Should().NotBeNull()
                .And.HaveCount(6)
                .And.ContainKeys(new[] {"Information", "Symbol", "Last Refreshed", "Interval", "Output Size", "Time Zone"})
                .And.ContainValues(symbol, interval, "Compact");
        }
        
        [Fact]
        public async Task ReturnEmptyDictionaryIfNoMetadata()
        {
            using var client = new Client.AlphaVantageClient(_apiKey);
            var response = await client.RequestParsedJsonAsync(
                ApiFunction.SYMBOL_SEARCH, 
                new Dictionary<string, string>()
                {
                    {"keywords", "BA"}
                }, 
                true);

            var metadata = response.ExtractMetaData();

            metadata.Should().NotBeNull().And.BeEmpty();
        }
    }
}