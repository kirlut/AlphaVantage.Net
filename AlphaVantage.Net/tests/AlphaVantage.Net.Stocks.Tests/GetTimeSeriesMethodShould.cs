using System.Threading.Tasks;
using AlphaVantage.Net.Core.Client;
using AlphaVantage.Net.Core.Exceptions;
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
        [InlineData(TimeSeriesType.Daily, TimeSeriesSize.Compact, false)]
        [InlineData(TimeSeriesType.Weekly, TimeSeriesSize.Compact, false)]
        [InlineData(TimeSeriesType.Monthly, TimeSeriesSize.Compact, false)]
        [InlineData(TimeSeriesType.Daily, TimeSeriesSize.Full, false)]
        [InlineData(TimeSeriesType.Weekly, TimeSeriesSize.Full, false)]
        [InlineData(TimeSeriesType.Monthly, TimeSeriesSize.Full, false)]
        [InlineData(TimeSeriesType.Daily, TimeSeriesSize.Compact, true)]
        [InlineData(TimeSeriesType.Weekly, TimeSeriesSize.Compact, true)]
        [InlineData(TimeSeriesType.Monthly, TimeSeriesSize.Compact, true)]
        [InlineData(TimeSeriesType.Daily, TimeSeriesSize.Full, true)]
        [InlineData(TimeSeriesType.Weekly, TimeSeriesSize.Full, true)]
        [InlineData(TimeSeriesType.Monthly, TimeSeriesSize.Full, true)]
        public async Task ReturnValidTimeSeries(TimeSeriesType seriesType, TimeSeriesSize size, bool isAdjusted)
        {
            using var client = new AlphaVantageClient(_apiKey);
            using var stocksClient = client.Stocks();

            var timeSeries = await stocksClient.GetTimeSeriesAsync("AAPL", seriesType, size, isAdjusted);

            timeSeries.Should().NotBeNull()
                .And.Match<StockTimeSeries>(ts =>
                    ts.IsAdjusted == isAdjusted &&
                    ts.Type == seriesType);

            timeSeries.MetaData.Should().NotBeNull()
                .And.HaveCountGreaterThan(1);

            timeSeries.DataPoints.Should().NotBeNull()
                .And.HaveCountGreaterThan(1)
                .And.NotContainNulls()
                .And.OnlyContain(dp => IsDataPointValid(dp));
            
            if(isAdjusted == false) return;
            
            timeSeries.DataPoints.Should()
                .OnlyContain(dp => 
                    IsAdjustedDataPointValid(dp, seriesType == TimeSeriesType.Daily && isAdjusted));
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

        [Theory]
        [InlineData(TimeSeriesType.Daily, TimeSeriesSize.Compact, false)]
        [InlineData(TimeSeriesType.Weekly, TimeSeriesSize.Compact, false)]
        [InlineData(TimeSeriesType.Monthly, TimeSeriesSize.Compact, false)]
        [InlineData(TimeSeriesType.Daily, TimeSeriesSize.Full, false)]
        [InlineData(TimeSeriesType.Weekly, TimeSeriesSize.Full, false)]
        [InlineData(TimeSeriesType.Monthly, TimeSeriesSize.Full, false)]
        [InlineData(TimeSeriesType.Daily, TimeSeriesSize.Compact, true)]
        [InlineData(TimeSeriesType.Weekly, TimeSeriesSize.Compact, true)]
        [InlineData(TimeSeriesType.Monthly, TimeSeriesSize.Compact, true)]
        [InlineData(TimeSeriesType.Daily, TimeSeriesSize.Full, true)]
        [InlineData(TimeSeriesType.Weekly, TimeSeriesSize.Full, true)]
        [InlineData(TimeSeriesType.Monthly, TimeSeriesSize.Full, true)]
        public async Task ThrowException_ForInvalidSymbol(TimeSeriesType seriesType, TimeSeriesSize size,
            bool isAdjusted)
        {
            using var client = new AlphaVantageClient(_apiKey);
            using var stocksClient = client.Stocks();


            await Assert.ThrowsAsync<AlphaVantageException>(async () =>
            {
                await stocksClient.GetTimeSeriesAsync("wrong_symbol", seriesType, size, isAdjusted);
            });
        }

        [Theory]
        [InlineData(TimeSeriesSize.Compact, false)]
        [InlineData(TimeSeriesSize.Compact, true)]
        [InlineData(TimeSeriesSize.Full, false)]
        [InlineData(TimeSeriesSize.Full, true)]
        public async Task ThrowException_ForIntradayTimeSeries(TimeSeriesSize size, bool isAdjusted)
        {
            using var client = new AlphaVantageClient(_apiKey);
            using var stocksClient = client.Stocks();

            await Assert.ThrowsAsync<AlphaVantageException>(async () =>
            {
                await stocksClient.GetTimeSeriesAsync("AAPL", TimeSeriesType.Intraday, size, isAdjusted);
            });
        }
    }
}