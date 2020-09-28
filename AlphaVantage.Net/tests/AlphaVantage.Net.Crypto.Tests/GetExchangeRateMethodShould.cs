using System.Threading.Tasks;
using AlphaVantage.Net.Common.Currencies;
using AlphaVantage.Net.Core.Client;
using AlphaVantage.Net.Crypto.Client;
using AlphaVantage.Net.TestUtils;
using FluentAssertions;
using Xunit;

namespace AlphaVantage.Net.Crypto.Tests
{
    public class GetExchangeRateMethodShould
    {
        private readonly string _apiKey = ConfigProvider.Configuration["ApiKey"];
        
        [Fact]
        public async Task ReturnExchangeRate()
        {
            using var client = new AlphaVantageClient(_apiKey);
            using var forexClient = client.Crypto();

            var fromCurrency = DigitalCurrency.BTC;
            var toCurrency = PhysicalCurrency.ILS;
            
            var exchangeRate = await forexClient.GetExchangeRateAsync(fromCurrency, toCurrency);

            exchangeRate.Should().NotBeNull()
                .And.Match<CryptoExchangeRate>(er =>
                    er.FromCurrencyCode == fromCurrency &&
                    string.IsNullOrEmpty(er.FromCurrencyName) == false &&
                    er.ToCurrencyCode == toCurrency &&
                    string.IsNullOrEmpty(er.ToCurrencyName) == false &&
                    er.ExchangeRate != default &&
                    er.LastRefreshed != default &&
                    string.IsNullOrEmpty(er.TimeZone) == false &&
                    er.BidPrice != default &&
                    er.AskPrice != default
                );
        }
    }
}