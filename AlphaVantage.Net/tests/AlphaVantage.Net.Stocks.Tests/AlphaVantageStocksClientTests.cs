using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AlphaVantage.Net.Stocks.TimeSeries;
using Xunit;

namespace AlphaVantage.Net.Stocks.Tests
{
    public class AlphaVantageStocksClientTests
    {
        private const string Symbol = "AAPL";
        private const string ApiKey = "1";

        public AlphaVantageStocksClientTests()
        {
            Thread.Sleep(TimeSpan.FromSeconds(10)); // to avoid api rejection
        }
        
        [Fact]
        public async Task RequestIntradayTimeSeriesAsync_Test()
        {
            var client = new AlphaVantageStocksClient(ApiKey);

            var result =
                await client.RequestIntradayTimeSeriesAsync(Symbol, IntradayInterval.OneMin, TimeSeriesSize.Compact);
            
            Assert.NotNull(result);
            Assert.Equal(TimeSeriesType.Intraday, result.Type);
            Assert.Equal(Symbol, result.Symbol);
            Assert.NotNull(result.DataPoints);
            Assert.True(result.DataPoints.All(p => 
                p.GetType() == typeof(StockDataPoint) &&
                p.GetType() != typeof(StockAdjustedDataPoint)));
        }
        
        [Fact]
        public async Task RequestDailyTimeSeriesAsync_NotAdjusted_Test()
        {
            var client = new AlphaVantageStocksClient(ApiKey);

            var result =
                await client.RequestDailyTimeSeriesAsync(Symbol, TimeSeriesSize.Compact, adjusted: false);
            
            Assert.NotNull(result);
            Assert.Equal(TimeSeriesType.Daily, result.Type);
            Assert.Equal(Symbol, result.Symbol);
            Assert.False(result.IsAdjusted);
            Assert.NotNull(result.DataPoints);
            Assert.True(result.DataPoints.All(p => 
                p.GetType() == typeof(StockDataPoint) &&
                p.GetType() != typeof(StockAdjustedDataPoint)));
        }
        
        [Fact]
        public async Task RequestDailyTimeSeriesAsync_Adjusted_Test()
        {
            var client = new AlphaVantageStocksClient(ApiKey);

            var result =
                await client.RequestDailyTimeSeriesAsync(Symbol, TimeSeriesSize.Compact, adjusted: true);
            
            Assert.NotNull(result);
            Assert.Equal(TimeSeriesType.Daily, result.Type);
            Assert.Equal(Symbol, result.Symbol);
            Assert.True(result.IsAdjusted);
            Assert.NotNull(result.DataPoints);
            Assert.True(result.DataPoints.All(p => p is StockAdjustedDataPoint));
        }
        
        [Fact]
        public async Task RequestWeeklyTimeSeriesAsync_NotAdjusted_Test()
        {
            var client = new AlphaVantageStocksClient(ApiKey);

            var result =
                await client.RequestWeeklyTimeSeriesAsync(Symbol, adjusted: false);
            
            Assert.NotNull(result);
            Assert.Equal(TimeSeriesType.Weekly, result.Type);
            Assert.Equal(Symbol, result.Symbol);
            Assert.False(result.IsAdjusted);
            Assert.NotNull(result.DataPoints);
            Assert.True(result.DataPoints.All(p => 
                p.GetType() == typeof(StockDataPoint) &&
                p.GetType() != typeof(StockAdjustedDataPoint)));
        }
        
        [Fact]
        public async Task RequestWeeklyTimeSeriesAsync_Adjusted_Test()
        {
            var client = new AlphaVantageStocksClient(ApiKey);

            var result =
                await client.RequestWeeklyTimeSeriesAsync(Symbol, adjusted: true);
            
            Assert.NotNull(result);
            Assert.Equal(TimeSeriesType.Weekly, result.Type);
            Assert.Equal(Symbol, result.Symbol);
            Assert.True(result.IsAdjusted);
            Assert.NotNull(result.DataPoints);
            Assert.True(result.DataPoints.All(p => p is StockAdjustedDataPoint));
        }
        
        [Fact]
        public async Task RequestMonthlyTimeSeriesAsync_NotAdjusted_Test()
        {
            var client = new AlphaVantageStocksClient(ApiKey);

            var result =
                await client.RequestMonthlyTimeSeriesAsync(Symbol, adjusted: false);
            
            Assert.NotNull(result);
            Assert.Equal(TimeSeriesType.Monthly, result.Type);
            Assert.Equal(Symbol, result.Symbol);
            Assert.False(result.IsAdjusted);
            Assert.NotNull(result.DataPoints);
            Assert.True(result.DataPoints.All(p => 
                p.GetType() == typeof(StockDataPoint) &&
                p.GetType() != typeof(StockAdjustedDataPoint)));
        }
        
        [Fact]
        public async Task RequestMonthlyTimeSeriesAsync_Adjusted_Test()
        {
            var client = new AlphaVantageStocksClient(ApiKey);

            var result =
                await client.RequestMonthlyTimeSeriesAsync(Symbol, adjusted: true);
            
            Assert.NotNull(result);
            Assert.Equal(TimeSeriesType.Monthly, result.Type);
            Assert.Equal(Symbol, result.Symbol);
            Assert.True(result.IsAdjusted);
            Assert.NotNull(result.DataPoints);
            Assert.True(result.DataPoints.All(p => p is StockAdjustedDataPoint));
        }
        
        [Fact]
        public async Task RequestBatchQuotesAsync_Test()
        {
            var client = new AlphaVantageStocksClient(ApiKey);

            var result =
                await client.RequestBatchQuotesAsync(new []{"AAPL", "FB", "MSFT"});
            
            Assert.NotNull(result);
            Assert.Equal(3, result.Count);
            Assert.True(
                result.Any(r => r.Symbol == "MSFT") &&
                result.Any(r => r.Symbol == "FB") && 
                result.Any(r => r.Symbol == "AAPL"));
        }
    }
}