using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using AlphaVantage.Net.Core.Exceptions;
using FluentAssertions;
using AlphaVantage.Net.TestUtils;
using Xunit;

namespace AlphaVantage.Net.Core.Tests
{
    public class AlphaVantageClientShould
    {
        private readonly string _apiKey = ConfigProvider.Configuration["ApiKey"];
        
        [Fact]
        public async Task ReturnJsonDocumentOnCorrectRequest()
        {
            var function = ApiFunction.TIME_SERIES_INTRADAY;
            var symbol = "AAPL";
            var interval = "15min";
            var query = new Dictionary<string, string>()
            {
                {nameof(symbol), symbol},
                {nameof(interval), interval}
            };

            using var client = new Client.AlphaVantageClient(_apiKey);
            var response = await client.RequestApiAsync(function, query);

            response
                .Should().NotBeNull()
                .And
                .Match<JsonDocument>(
                    resp => resp.RootElement.EnumerateObject()
                        .LastOrDefault().NameEquals("Time Series (15min)")
                    );
        }
        
        [Fact]
        public async Task ThrowExceptionOnIncorrectRequest()
        {
            var function = ApiFunction.TIME_SERIES_INTRADAY;
            var symbol = "wrong_symbol"; // Bad request!  No such symbol exist
            var interval = "15min";
            var query = new Dictionary<string, string>()
            {
                {nameof(symbol), symbol},
                {nameof(interval), interval}
            };

            using var client = new Client.AlphaVantageClient(_apiKey);

            await Assert.ThrowsAsync<AlphaVantageException>(async () =>
            {
                await client.RequestApiAsync(function, query);
            });
        }
    }
}