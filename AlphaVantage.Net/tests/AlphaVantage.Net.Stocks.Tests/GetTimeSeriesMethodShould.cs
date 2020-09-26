using System.Threading.Tasks;
using AlphaVantage.Net.Core.Client;
using AlphaVantage.Net.Core.Exceptions;
using AlphaVantage.Net.Core.Intervals;
using AlphaVantage.Net.Core.Size;
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
        [InlineData(Interval.Daily, OutputSize.Compact, false)]
        [InlineData(Interval.Weekly, OutputSize.Compact, false)]
        [InlineData(Interval.Monthly, OutputSize.Compact, false)]
        [InlineData(Interval.Daily, OutputSize.Full, false)]
        [InlineData(Interval.Weekly, OutputSize.Full, false)]
        [InlineData(Interval.Monthly, OutputSize.Full, false)]
        [InlineData(Interval.Daily, OutputSize.Compact, true)]
        [InlineData(Interval.Weekly, OutputSize.Compact, true)]
        [InlineData(Interval.Monthly, OutputSize.Compact, true)]
        [InlineData(Interval.Daily, OutputSize.Full, true)]
        [InlineData(Interval.Weekly, OutputSize.Full, true)]
        [InlineData(Interval.Monthly, OutputSize.Full, true)]
        public async Task ReturnValidTimeSeries(Interval interval, OutputSize size, bool isAdjusted)
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
        [InlineData(Interval.Min1, OutputSize.Full, false)]
        [InlineData(Interval.Min5, OutputSize.Full, false)]
        [InlineData(Interval.Min15, OutputSize.Full, false)]
        [InlineData(Interval.Min30, OutputSize.Full, false)]
        [InlineData(Interval.Min60, OutputSize.Full, false)]
        [InlineData(Interval.Min1, OutputSize.Compact, false)]
        [InlineData(Interval.Min5, OutputSize.Compact, false)]
        [InlineData(Interval.Min15, OutputSize.Compact, false)]
        [InlineData(Interval.Min30, OutputSize.Compact, false)]
        [InlineData(Interval.Min60, OutputSize.Compact, false)]
        [InlineData(Interval.Min1, OutputSize.Full, true)]
        [InlineData(Interval.Min5, OutputSize.Full, true)]
        [InlineData(Interval.Min15, OutputSize.Full, true)]
        [InlineData(Interval.Min30, OutputSize.Full, true)]
        [InlineData(Interval.Min60, OutputSize.Full, true)]
        [InlineData(Interval.Min1, OutputSize.Compact, true)]
        [InlineData(Interval.Min5, OutputSize.Compact, true)]
        [InlineData(Interval.Min15, OutputSize.Compact, true)]
        [InlineData(Interval.Min30, OutputSize.Compact, true)]
        [InlineData(Interval.Min60, OutputSize.Compact, true)]
        public async Task ReturnValidIntradayTimeSeries(Interval interval, OutputSize size, bool isAdjusted)
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
        [InlineData(Interval.Daily, OutputSize.Compact, false)]
        [InlineData(Interval.Weekly, OutputSize.Compact, false)]
        [InlineData(Interval.Monthly, OutputSize.Compact, false)]
        [InlineData(Interval.Daily, OutputSize.Full, false)]
        [InlineData(Interval.Weekly, OutputSize.Full, false)]
        [InlineData(Interval.Monthly, OutputSize.Full, false)]
        [InlineData(Interval.Daily, OutputSize.Compact, true)]
        [InlineData(Interval.Weekly, OutputSize.Compact, true)]
        [InlineData(Interval.Monthly, OutputSize.Compact, true)]
        [InlineData(Interval.Daily, OutputSize.Full, true)]
        [InlineData(Interval.Weekly, OutputSize.Full, true)]
        [InlineData(Interval.Monthly, OutputSize.Full, true)]
        [InlineData(Interval.Min1, OutputSize.Full, false)]
        [InlineData(Interval.Min5, OutputSize.Full, false)]
        [InlineData(Interval.Min15, OutputSize.Full, false)]
        [InlineData(Interval.Min30, OutputSize.Full, false)]
        [InlineData(Interval.Min60, OutputSize.Full, false)]
        [InlineData(Interval.Min1, OutputSize.Compact, false)]
        [InlineData(Interval.Min5, OutputSize.Compact, false)]
        [InlineData(Interval.Min15, OutputSize.Compact, false)]
        [InlineData(Interval.Min30, OutputSize.Compact, false)]
        [InlineData(Interval.Min60, OutputSize.Compact, false)]
        public async Task ThrowException_ForInvalidSymbol(Interval interval, OutputSize size, bool isAdjusted)
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