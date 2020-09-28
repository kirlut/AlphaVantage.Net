using System.Threading.Tasks;
using AlphaVantage.Net.Common.Currencies;
using AlphaVantage.Net.Core.Client;
using AlphaVantage.Net.Forex.Client;
using AlphaVantage.Net.TestUtils;
using FluentAssertions;
using Xunit;

namespace AlphaVantage.Net.Forex.Tests
{
    public class GetExchangeRateMethodShould
    {
        private readonly string _apiKey = ConfigProvider.Configuration["ApiKey"];
        
        [Fact]
        public async Task ReturnValidExchangeRate()
        {
            using var client = new AlphaVantageClient(_apiKey);
            using var forexClient = client.Forex();

            var fromCurrency = PhysicalCurrency.USD;
            var toCurrency = PhysicalCurrency.ILS;
            
            var exchangeRate = await forexClient.GetExchangeRateAsync(fromCurrency, toCurrency);

            exchangeRate.Should().NotBeNull()
                .And.Match<ForexExchangeRate>(er =>
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