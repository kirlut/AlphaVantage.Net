using System.Collections.Generic;
using System.Threading.Tasks;
using AlphaVantage.Net.Common;
using AlphaVantage.Net.Common.Exceptions;
using AlphaVantage.Net.Core.Validation;
using AlphaVantage.Net.TestUtils;
using Xunit;

#pragma warning disable 618

namespace AlphaVantage.Net.Core.Tests.Obsolete
{
    public class ObsoleteCoreClientTests
    {
        private readonly string _apiKey = ConfigProvider.Configuration["ApiKey"];

        [Fact(Skip = "Obsolete")]
        public async Task CorrectRequestTest()
        {
            var function = ApiFunction.TIME_SERIES_INTRADAY;
            var symbol = "AAPL";
            var interval = "15min";
            var query = new Dictionary<string, string>()
            {
                {nameof(symbol), symbol},
                {nameof(interval), interval}
            };
            
            var client = new AlphaVantageCoreClient();
            var response = await client.RequestApiAsync(_apiKey, function, query);
            
            Assert.NotNull(response);
            Assert.True(response.ContainsKey("Time Series (15min)"));
        }

        [Fact(Skip = "Obsolete")]
        public async Task BadRequestTest()
        {
            var function = ApiFunction.TIME_SERIES_INTRADAY;
            var symbol = "wrong_symbol"; // Bad request!  No such symbol exist
            var interval = "15min";
            var query = new Dictionary<string, string>()
            {
                {nameof(symbol), symbol},
                {nameof(interval), interval}
            };
            
            var client = new AlphaVantageCoreClient();

            await Assert.ThrowsAsync<AlphaVantageException>(async () =>
            {
                await client.RequestApiAsync(_apiKey, function, query);
            });
        }
        
        [Fact(Skip = "Obsolete")]
        public async Task ValidationErrorTest()
        {
            var function = ApiFunction.TIME_SERIES_INTRADAY;
            var symbol = "wrong_symbol"; // Bad request!  No such symbol exist
            var interval = "15min";
            var query = new Dictionary<string, string>()
            {
                {nameof(symbol), symbol},
                {nameof(interval), interval}
            };
            
            var client = new AlphaVantageCoreClient(new TestValidator());

            var exception = await Assert.ThrowsAsync<AlphaVantageException>(async () =>
            {
                await client.RequestApiAsync(_apiKey, function, query);
            });
            
            Assert.Equal(TestValidator.ErrorMsg, exception.Message);
        }
        
        private class TestValidator : IApiCallValidator
        {
            public static string ErrorMsg = "test";
            
            public ValidationResult Validate(ApiFunction function, IDictionary<string, string>? query)
            {
                return new ValidationResult()
                {
                    IsValid = false,
                    ErrorMsg = ErrorMsg
                };
            }
        }
    }
}