using System.Threading.Tasks;
using AlphaVantage.Net.Common.Currencies;
using AlphaVantage.Net.Common.Exceptions;
using AlphaVantage.Net.Common.Intervals;
using AlphaVantage.Net.Core.Client;
using AlphaVantage.Net.Crypto.Client;
using AlphaVantage.Net.TestUtils;
using FluentAssertions;
using Xunit;

namespace AlphaVantage.Net.Crypto.Tests
{
    public class GetTimeSeriesMethodShould
    {
        private readonly string _apiKey = ConfigProvider.Configuration["ApiKey"];
        
        [Theory]
        [InlineData(Interval.Daily)]
        [InlineData(Interval.Weekly)]
        [InlineData(Interval.Monthly)]
        public async Task ReturnValidNotIntradayTimeSeries(Interval interval)
        {
            using var client = new AlphaVantageClient(_apiKey);
            using var cryptoClient = client.Crypto();
            
            var fromCurrency = DigitalCurrency.BTC;
            var toCurrency = PhysicalCurrency.ILS;

            var timeSeries = await cryptoClient.GetTimeSeriesAsync(fromCurrency, toCurrency, interval);

            timeSeries.Should().NotBeNull()
                .And.Match<CryptoTimeSeries>(ts =>
                    ts.Interval == interval &&
                    ts.FromCurrency == fromCurrency && 
                    ts.ToCurrency == toCurrency);

            timeSeries.MetaData.Should().NotBeNull()
                .And.HaveCountGreaterThan(1);

            timeSeries.DataPoints.Should().NotBeNull()
                .And.HaveCountGreaterThan(1)
                .And.NotContainNulls()
                .And.OnlyContain(dp => IsDataPointValid(dp));
        }

        [Theory]
        [InlineData(Interval.Min1)]
        [InlineData(Interval.Min5)]
        [InlineData(Interval.Min15)]
        [InlineData(Interval.Min30)]
        [InlineData(Interval.Min60)]
        public async Task ThrowException_ForIntradayIntervals(Interval interval)
        {
            using var client = new AlphaVantageClient(_apiKey);
            using var cryptoClient = client.Crypto();
            
            var fromCurrency = DigitalCurrency.BTC;
            var toCurrency = PhysicalCurrency.ILS;

            await Assert.ThrowsAsync<AlphaVantageException>(async () =>
            {
                await cryptoClient.GetTimeSeriesAsync(fromCurrency, toCurrency, interval);
            });
        }
        
        private static bool IsDataPointValid(CryptoDataPoint dp)
        {
            return dp.Time != default &&
                   dp.OpeningPrice > 0 &&
                   dp.OpeningPriceUSD > 0 &&
                   dp.ClosingPrice > 0 &&
                   dp.ClosingPriceUSD > 0 &&
                   dp.HighestPrice > 0 &&
                   dp.HighestPriceUSD > 0 &&
                   dp.LowestPrice > 0 &&
                   dp.LowestPriceUSD > 0 &&
                   dp.Volume > 0 &&
                   dp.MarketCapitalization > 0;
        }
    }
}