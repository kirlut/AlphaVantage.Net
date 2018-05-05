using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AlphaVantage.Net.Core.Exceptions;
using AlphaVantage.Net.Core.Validation;
using Xunit;

namespace AlphaVantage.Net.Core.Tests
{
    public class CoreClientTests
    {
        private const string ApiKey = "1"; // yep, this api key really exist :)
        
        [Fact]
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
            
            var client = new AlfaVantageCoreClient();
            var response = await client.RequestApiAsync(ApiKey, function, query);
            
            Assert.NotNull(response);
            Assert.True(response.ContainsKey("Time Series (15min)"));
        }

        [Fact]
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
            
            var client = new AlfaVantageCoreClient();

            await Assert.ThrowsAsync<AlfaVantageException>(async () =>
            {
                await client.RequestApiAsync(ApiKey, function, query);
            });
        }
        
        [Fact]
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
            
            var client = new AlfaVantageCoreClient(new TestValidator());

            var exception = await Assert.ThrowsAsync<AlfaVantageException>(async () =>
            {
                await client.RequestApiAsync(ApiKey, function, query);
            });
            
            Assert.Equal(TestValidator.ErrorMsg, exception.Message);
        }
        
        private class TestValidator : IApiCallValidator
        {
            public static string ErrorMsg = "test";
            
            public ValidationResult Validate(string apiKey, ApiFunction function, IDictionary<string, string> query)
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