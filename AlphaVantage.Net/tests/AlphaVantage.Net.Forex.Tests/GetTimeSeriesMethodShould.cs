using System.Threading.Tasks;
using AlphaVantage.Net.Common.Currencies;
using AlphaVantage.Net.Common.Intervals;
using AlphaVantage.Net.Common.Size;
using AlphaVantage.Net.Core.Client;
using AlphaVantage.Net.Forex.Client;
using AlphaVantage.Net.TestUtils;
using FluentAssertions;
using Xunit;

namespace AlphaVantage.Net.Forex.Tests
{
    public class GetTimeSeriesMethodShould
    {
        private readonly string _apiKey = ConfigProvider.Configuration["ApiKey"];

        [Theory]
        [InlineData(Interval.Min1, OutputSize.Compact)]
        [InlineData(Interval.Min5, OutputSize.Compact)]
        [InlineData(Interval.Min15, OutputSize.Compact)]
        [InlineData(Interval.Min30, OutputSize.Compact)]
        [InlineData(Interval.Min60, OutputSize.Compact)]
        [InlineData(Interval.Daily, OutputSize.Compact)]
        [InlineData(Interval.Weekly, OutputSize.Compact)]
        [InlineData(Interval.Monthly, OutputSize.Compact)]
        [InlineData(Interval.Min1, OutputSize.Full)]
        [InlineData(Interval.Min5, OutputSize.Full)]
        [InlineData(Interval.Min15, OutputSize.Full)]
        [InlineData(Interval.Min30, OutputSize.Full)]
        [InlineData(Interval.Min60, OutputSize.Full)]
        [InlineData(Interval.Daily, OutputSize.Full)]
        [InlineData(Interval.Weekly, OutputSize.Full)]
        [InlineData(Interval.Monthly, OutputSize.Full)]
        public async Task ReturnValidTimeSeries(Interval interval, OutputSize outputSize)
        {
            
            using var client = new AlphaVantageClient(_apiKey);
            using var forexClient = client.Forex();
            
            var fromCurrency = PhysicalCurrency.USD;
            var toCurrency = PhysicalCurrency.ILS;

            var timeSeries = await forexClient.GetTimeSeriesAsync(fromCurrency, toCurrency, interval, outputSize);
            
            timeSeries.Should().NotBeNull()
                .And.Match<ForexTimeSeries>(ts =>
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
        
        private static bool IsDataPointValid(ForexDataPoint dp)
        {
            return dp.Time != default &&
                   dp.OpeningPrice > 0 &&
                   dp.ClosingPrice > 0 &&
                   dp.HighestPrice > 0 &&
                   dp.LowestPrice > 0;
        }
    }
}