using System.Threading.Tasks;
using AlphaVantage.Net.Common.Currencies;
using AlphaVantage.Net.Core.Client;
using AlphaVantage.Net.Crypto.Client;
using AlphaVantage.Net.TestUtils;
using FluentAssertions;
using Xunit;

namespace AlphaVantage.Net.Crypto.Tests
{
    public class GetCryptoRatingMethodShould
    {
        private readonly string _apiKey = ConfigProvider.Configuration["ApiKey"];
        
        [Fact]
        public async Task ReturnValidCryptoRating()
        {
            using var client = new AlphaVantageClient(_apiKey);
            using var forexClient = client.Crypto();

            var currency = DigitalCurrency.BTC;
            
            var exchangeRate = await forexClient.GetCryptoRatingAsync(currency);

            exchangeRate.Should().NotBeNull()
                .And.Match<CryptoRating>(er =>
                    er.Symbol == currency &&
                    string.IsNullOrEmpty(er.Name) == false &&
                    er.Rating != default &&
                    er.FcasScore > 0 &&
                    er.DeveloperScore > 0 &&
                    er.MarketMaturityScore > 0 &&
                    er.UtilityScore > 0 &&
                    er.LastRefreshed != default &&
                    string.IsNullOrEmpty(er.TimeZone) == false
                );
        }
    }
}