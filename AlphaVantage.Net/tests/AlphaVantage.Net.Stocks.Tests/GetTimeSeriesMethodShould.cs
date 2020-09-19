using System.Threading.Tasks;
using AlphaVantage.Net.Core.Client;
using AlphaVantage.Net.Core.Exceptions;
using AlphaVantage.Net.Core.Intervals;
using AlphaVantage.Net.Stocks.Client;
using AlphaVantage.Net.Stocks.TimeSeries;
using AlphaVantage.Net.TestUtils;
using FluentAssertions;
using Xunit;

#pragma warning disable 618

namespace AlphaVantage.Net.Stocks.Tests
{
    public class GetTimeSeriesMethodShould
    {
        private readonly string _apiKey = ConfigProvider.Configuration["ApiKey"];

        [Theory]
        [InlineData(Interval.Daily, TimeSeriesSize.Compact, false)]
        [InlineData(Interval.Weekly, TimeSeriesSize.Compact, false)]
        [InlineData(Interval.Monthly, TimeSeriesSize.Compact, false)]
        [InlineData(Interval.Daily, TimeSeriesSize.Full, false)]
        [InlineData(Interval.Weekly, TimeSeriesSize.Full, false)]
        [InlineData(Interval.Monthly, TimeSeriesSize.Full, false)]
        [InlineData(Interval.Daily, TimeSeriesSize.Compact, true)]
        [InlineData(Interval.Weekly, TimeSeriesSize.Compact, true)]
        [InlineData(Interval.Monthly, TimeSeriesSize.Compact, true)]
        [InlineData(Interval.Daily, TimeSeriesSize.Full, true)]
        [InlineData(Interval.Weekly, TimeSeriesSize.Full, true)]
        [InlineData(Interval.Monthly, TimeSeriesSize.Full, true)]
        public async Task ReturnValidTimeSeries(Interval interval, TimeSeriesSize size, bool isAdjusted)
        {
            using var client = new AlphaVantageClient(_apiKey);
            using var stocksClient = client.Stocks();

            var timeSeries = await stocksClient.GetTimeSeriesAsync("AAPL", interval, size, isAdjusted);

            timeSeries.Should().NotBeNull()
                .And.Match<StockTimeSeries>(ts =>
                    ts.IsAdjusted == isAdjusted &&
                    ts.Interval == interval);

            timeSeries.MetaData.Should().NotBeNull()
                .And.HaveCountGreaterThan(1);

            timeSeries.DataPoints.Should().NotBeNull()
                .And.HaveCountGreaterThan(1)
                .And.NotContainNulls()
                .And.OnlyContain(dp => IsDataPointValid(dp));
            
            if(isAdjusted == false) return;
            
            timeSeries.DataPoints.Should()
                .OnlyContain(dp => 
                    IsAdjustedDataPointValid(dp, interval == Interval.Daily && isAdjusted));
        }

        [Theory]
        [InlineData(Interval.Min1, TimeSeriesSize.Full, false)]
        [InlineData(Interval.Min5, TimeSeriesSize.Full, false)]
        [InlineData(Interval.Min15, TimeSeriesSize.Full, false)]
        [InlineData(Interval.Min30, TimeSeriesSize.Full, false)]
        [InlineData(Interval.Min60, TimeSeriesSize.Full, false)]
        [InlineData(Interval.Min1, TimeSeriesSize.Compact, false)]
        [InlineData(Interval.Min5, TimeSeriesSize.Compact, false)]
        [InlineData(Interval.Min15, TimeSeriesSize.Compact, false)]
        [InlineData(Interval.Min30, TimeSeriesSize.Compact, false)]
        [InlineData(Interval.Min60, TimeSeriesSize.Compact, false)]
        [InlineData(Interval.Min1, TimeSeriesSize.Full, true)]
        [InlineData(Interval.Min5, TimeSeriesSize.Full, true)]
        [InlineData(Interval.Min15, TimeSeriesSize.Full, true)]
        [InlineData(Interval.Min30, TimeSeriesSize.Full, true)]
        [InlineData(Interval.Min60, TimeSeriesSize.Full, true)]
        [InlineData(Interval.Min1, TimeSeriesSize.Compact, true)]
        [InlineData(Interval.Min5, TimeSeriesSize.Compact, true)]
        [InlineData(Interval.Min15, TimeSeriesSize.Compact, true)]
        [InlineData(Interval.Min30, TimeSeriesSize.Compact, true)]
        [InlineData(Interval.Min60, TimeSeriesSize.Compact, true)]
        public async Task ReturnValidIntradayTimeSeries(Interval interval, TimeSeriesSize size, bool isAdjusted)
        {
            using var client = new AlphaVantageClient(_apiKey);
            using var stocksClient = client.Stocks();

            var timeSeries = await stocksClient.GetTimeSeriesAsync("AAPL", interval, size, isAdjusted);

            timeSeries.Should().NotBeNull()
                .And.Match<StockTimeSeries>(ts =>
                    ts.IsAdjusted == false && // intraday ts are always not adjusted
                    ts.Interval == interval);

            timeSeries.MetaData.Should().NotBeNull()
                .And.HaveCountGreaterThan(1);

            timeSeries.DataPoints.Should().NotBeNull()
                .And.HaveCountGreaterThan(1)
                .And.NotContainNulls()
                .And.OnlyContain(dp => 
                    IsDataPointValid(dp) && dp.GetType() != typeof(StockAdjustedDataPoint));
        }

        [Theory]
        [InlineData(Interval.Daily, TimeSeriesSize.Compact, false)]
        [InlineData(Interval.Weekly, TimeSeriesSize.Compact, false)]
        [InlineData(Interval.Monthly, TimeSeriesSize.Compact, false)]
        [InlineData(Interval.Daily, TimeSeriesSize.Full, false)]
        [InlineData(Interval.Weekly, TimeSeriesSize.Full, false)]
        [InlineData(Interval.Monthly, TimeSeriesSize.Full, false)]
        [InlineData(Interval.Daily, TimeSeriesSize.Compact, true)]
        [InlineData(Interval.Weekly, TimeSeriesSize.Compact, true)]
        [InlineData(Interval.Monthly, TimeSeriesSize.Compact, true)]
        [InlineData(Interval.Daily, TimeSeriesSize.Full, true)]
        [InlineData(Interval.Weekly, TimeSeriesSize.Full, true)]
        [InlineData(Interval.Monthly, TimeSeriesSize.Full, true)]
        [InlineData(Interval.Min1, TimeSeriesSize.Full, false)]
        [InlineData(Interval.Min5, TimeSeriesSize.Full, false)]
        [InlineData(Interval.Min15, TimeSeriesSize.Full, false)]
        [InlineData(Interval.Min30, TimeSeriesSize.Full, false)]
        [InlineData(Interval.Min60, TimeSeriesSize.Full, false)]
        [InlineData(Interval.Min1, TimeSeriesSize.Compact, false)]
        [InlineData(Interval.Min5, TimeSeriesSize.Compact, false)]
        [InlineData(Interval.Min15, TimeSeriesSize.Compact, false)]
        [InlineData(Interval.Min30, TimeSeriesSize.Compact, false)]
        [InlineData(Interval.Min60, TimeSeriesSize.Compact, false)]
        public async Task ThrowException_ForInvalidSymbol(Interval interval, TimeSeriesSize size, bool isAdjusted)
        {
            using var client = new AlphaVantageClient(_apiKey);
            using var stocksClient = client.Stocks();
            
            await Assert.ThrowsAsync<AlphaVantageException>(async () =>
            {
                await stocksClient.GetTimeSeriesAsync("wrong_symbol", interval, size, isAdjusted);
            });
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
        
        private static bool IsAdjustedDataPointValid(StockDataPoint dp, bool hasSplitCoefficient)
        {
            
            return dp is StockAdjustedDataPoint adp &&
                   adp.AdjustedClosingPrice > 0 &&
                   // adp.DividendAmount != default &&
                   (hasSplitCoefficient == false || adp.SplitCoefficient != null);
        }
    }
}