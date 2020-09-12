using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using AlphaVantage.Net.Core.Client;
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
        public async Task ReturnJsonDocument_OnCorrectRequest_WithoutClean()
        {
            const ApiFunction function = ApiFunction.TIME_SERIES_INTRADAY;
            var query = GetQuery("AAPL", "15min");

            using var client = new AlphaVantageClient(_apiKey);
            var response = await client.RequestParsedJsonAsync(function, query);

            response
                .Should().NotBeNull()
                .And
                .Match<JsonDocument>(
                    resp => resp.RootElement.EnumerateObject()
                        .LastOrDefault().NameEquals("Time Series (15min)")
                    );
        }
        [Fact]
        public async Task ReturnJsonDocument_OnCorrectRequest_WithClean()
        {
            const ApiFunction function = ApiFunction.TIME_SERIES_INTRADAY;
            var query = GetQuery("AAPL", "15min");

            using var client = new Client.AlphaVantageClient(_apiKey);
            var response = await client.RequestParsedJsonAsync(function, query, true);

            response
                .Should().NotBeNull()
                .And
                .Match<JsonDocument>(
                    resp => resp.RootElement.EnumerateObject()
                        .LastOrDefault().NameEquals("Time Series (15min)")
                    )
                .And
                .Match<JsonDocument>(
                    resp => resp
                        .RootElement.EnumerateObject().LastOrDefault()
                        .Value.EnumerateObject().FirstOrDefault()
                        .Value.EnumerateObject().FirstOrDefault()
                        .NameEquals("open")
                    );
        }
        
        [Fact]
        public async Task ReturnJsonString_OnCorrectRequest_WithoutClean()
        {
            const ApiFunction function = ApiFunction.TIME_SERIES_INTRADAY;
            var query = GetQuery("AAPL", "15min");

            using var client = new AlphaVantageClient(_apiKey);
            var response = await client.RequestPureJsonAsync(function, query);

            response
                .Should().NotBeNull()
                .And
                .Contain("Time Series (15min)");
        }
        
        [Fact]
        public async Task ReturnJsonString_OnCorrectRequest_WithClean()
        {
            const ApiFunction function = ApiFunction.TIME_SERIES_INTRADAY;
            var query = GetQuery("AAPL", "15min");

            using var client = new AlphaVantageClient(_apiKey);
            var response = await client.RequestPureJsonAsync(function, query, true);

            response
                .Should().NotBeNull()
                .And
                .Contain("Time Series (15min)")
                .And
                .NotContain("1. ");
        }
        
        [Fact]
        public async Task ThrowExceptionOnIncorrectRequest()
        {
            const ApiFunction function = ApiFunction.TIME_SERIES_INTRADAY;
            var query = GetQuery("wrong_symbol", "15min");

            using var client = new AlphaVantageClient(_apiKey);

            await Assert.ThrowsAsync<AlphaVantageException>(async () =>
            {
                await client.RequestParsedJsonAsync(function, query);
            });
        }

        private Dictionary<string, string> GetQuery(string symbolValue, string intervalValue)
        {
            return new Dictionary<string, string>()
            {
                {"symbol", symbolValue},
                {"interval", intervalValue}
            };
        }
    }
}