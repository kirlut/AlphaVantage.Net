using System.Threading.Tasks;
using AlphaVantage.Net.Core.Client;
using AlphaVantage.Net.Core.Exceptions;
using AlphaVantage.Net.Stocks.Client;
using AlphaVantage.Net.Stocks.TimeSeries;
using AlphaVantage.Net.TestUtils;
using FluentAssertions;
using Xunit;

namespace AlphaVantage.Net.Stocks.Tests
{
    public class GetIntradayTimeSeriesMethodShould
    {
        private readonly string _apiKey = ConfigProvider.Configuration["ApiKey"];

        [Theory]
        [InlineData(IntradayInterval.OneMin, TimeSeriesSize.Compact)]
        [InlineData(IntradayInterval.FiveMin, TimeSeriesSize.Compact)]
        [InlineData(IntradayInterval.FifteenMin, TimeSeriesSize.Compact)]
        [InlineData(IntradayInterval.ThirtyMin, TimeSeriesSize.Compact)]
        [InlineData(IntradayInterval.SixtyMin, TimeSeriesSize.Compact)]
        [InlineData(IntradayInterval.OneMin, TimeSeriesSize.Full)]
        [InlineData(IntradayInterval.FiveMin, TimeSeriesSize.Full)]
        [InlineData(IntradayInterval.FifteenMin, TimeSeriesSize.Full)]
        [InlineData(IntradayInterval.ThirtyMin, TimeSeriesSize.Full)]
        [InlineData(IntradayInterval.SixtyMin, TimeSeriesSize.Full)]
        public async Task ReturnValidTimeSeries(IntradayInterval interval, TimeSeriesSize size)
        {
            using var client = new AlphaVantageClient(_apiKey);
            using var stocksClient = client.Stocks();

            var timeSeries = await stocksClient.GetIntradayTimeSeriesAsync("AAPL", interval, size);

            timeSeries.Should().NotBeNull()
                .And.Match<IntradayTimeSeries>(ts =>
                    ts.Interval == interval);

            timeSeries.MetaData.Should().NotBeNull()
                .And.HaveCountGreaterThan(1);

            timeSeries.DataPoints.Should().NotBeNull()
                .And.HaveCountGreaterThan(1)
                .And.NotContainNulls()
                .And.OnlyContain(dp => IsDataPointValid(dp));
        }
        
        private static bool IsDataPointValid(StockDataPoint dp)
        {
            return dp.Time != default &&
                   dp.OpeningPrice > 0 &&
                   dp.ClosingPrice > 0 &&
                   dp.HighestPrice > 0 &&
                   dp.LowestPrice > 0 &&
                   dp.Volume > 0;
        }
        
        [Theory]
        [InlineData(IntradayInterval.OneMin, TimeSeriesSize.Compact)]
        [InlineData(IntradayInterval.FiveMin, TimeSeriesSize.Compact)]
        [InlineData(IntradayInterval.FifteenMin, TimeSeriesSize.Compact)]
        [InlineData(IntradayInterval.ThirtyMin, TimeSeriesSize.Compact)]
        [InlineData(IntradayInterval.SixtyMin, TimeSeriesSize.Compact)]
        [InlineData(IntradayInterval.OneMin, TimeSeriesSize.Full)]
        [InlineData(IntradayInterval.FiveMin, TimeSeriesSize.Full)]
        [InlineData(IntradayInterval.FifteenMin, TimeSeriesSize.Full)]
        [InlineData(IntradayInterval.ThirtyMin, TimeSeriesSize.Full)]
        [InlineData(IntradayInterval.SixtyMin, TimeSeriesSize.Full)]
        public async Task ThrowException_ForInvalidSymbol(IntradayInterval interval, TimeSeriesSize size)
        {
            using var client = new AlphaVantageClient(_apiKey);
            using var stocksClient = client.Stocks();

            await Assert.ThrowsAsync<AlphaVantageException>(async () =>
            {
                await stocksClient.GetIntradayTimeSeriesAsync("wrong_symbol", interval, size);
            });
        }
    }
}