using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlphaVantage.Net.Common.Exceptions;
using AlphaVantage.Net.Common.Intervals;
using AlphaVantage.Net.Core.Client;
using AlphaVantage.Net.TechnicalIndicators.Client;
using AlphaVantage.Net.TestUtils;
using FluentAssertions;
using Xunit;

namespace AlphaVantage.Net.TechnicalIndicators.Tests
{
    public class GetTechnicalIndicatorMethodShould
    {
        private readonly string _apiKey = ConfigProvider.Configuration["ApiKey"];
        
        [Theory]
        [InlineData(Interval.Min1)]
        [InlineData(Interval.Min5)]
        [InlineData(Interval.Min15)]
        [InlineData(Interval.Min30)]
        [InlineData(Interval.Min60)]
        [InlineData(Interval.Daily)]
        [InlineData(Interval.Weekly)]
        [InlineData(Interval.Monthly)]
        public async Task ReturnValidResult_WithOneParameter(Interval interval)
        {
            using var client = new AlphaVantageClient(_apiKey);

            var symbol = "IBM";
            var indicatorType = TechIndicatorType.SMA;
            var query = new Dictionary<string, string>()
            {
                {"time_period", "20"},
                {"series_type", "close"}
            };

            var result = await client.GetTechIndicatorTimeSeriesAsync(symbol, indicatorType, interval, query);

            AssertTechIndicatorResultValid(result, interval, indicatorType, 1);
        }
        
        [Theory]
        [InlineData(Interval.Min1)]
        [InlineData(Interval.Min5)]
        [InlineData(Interval.Min15)]
        [InlineData(Interval.Min30)]
        [InlineData(Interval.Min60)]
        [InlineData(Interval.Daily)]
        [InlineData(Interval.Weekly)]
        [InlineData(Interval.Monthly)]
        public async Task ReturnValidResult_WithMultipleParameters(Interval interval)
        {
            using var client = new AlphaVantageClient(_apiKey);

            var symbol = "IBM";
            var indicatorType = TechIndicatorType.BBANDS;
            var query = new Dictionary<string, string>()
            {
                {"time_period", "20"},
                {"series_type", "close"}
            };

            var result = await client.GetTechIndicatorTimeSeriesAsync(symbol, indicatorType, interval, query);

            AssertTechIndicatorResultValid(result, interval, indicatorType, 3);
        }

        [Theory]
        [InlineData(Interval.Daily)]
        [InlineData(Interval.Weekly)]
        [InlineData(Interval.Monthly)]
        public async Task ThrowException_IfRequestVwapNotForIntradayInterval(Interval interval)
        {
            using var client = new AlphaVantageClient(_apiKey);

            var symbol = "IBM";
            var indicatorType = TechIndicatorType.VWAP;
            
            await Assert.ThrowsAsync<AlphaVantageException>(async () =>
            {
                await client.GetTechIndicatorTimeSeriesAsync(symbol, indicatorType, interval);
            });
        }

        [Theory]
        [InlineData(Interval.Min1)]
        [InlineData(Interval.Min5)]
        [InlineData(Interval.Min15)]
        [InlineData(Interval.Min30)]
        [InlineData(Interval.Min60)]
        public async Task ReturnValidResult_IfRequestVwapForIntradayInterval(Interval interval)
        {
            using var client = new AlphaVantageClient(_apiKey);

            var symbol = "IBM";
            var indicatorType = TechIndicatorType.VWAP;
            
            var result = await client.GetTechIndicatorTimeSeriesAsync(symbol, indicatorType, interval);
            AssertTechIndicatorResultValid(result, interval, indicatorType, 1);
        }

        private static void AssertTechIndicatorResultValid(
            TechIndicatorTimeSeries timeSeries, 
            Interval interval,  
            TechIndicatorType indicatorType,
            int expectedParamsCount)
        {
            timeSeries.Should().NotBeNull()
                .And.Match<TechIndicatorTimeSeries>(ti =>
                    ti.Interval == interval &&
                    ti.IndicatorType == indicatorType);

            timeSeries.MetaData.Should().NotBeNull()
                .And.HaveCountGreaterThan(1);

            timeSeries.DataPoints.Should().NotBeNull()
                .And.HaveCountGreaterThan(1)
                .And.NotContainNulls()
                .And.OnlyContain(dp => IsDataPointValid(dp, expectedParamsCount));
        }
        
        private static bool IsDataPointValid(TechIndicatorDataPoint dp, int expectedParamsCount)
        {
            return dp.Time != default &&
                   dp.Parameters.Count == expectedParamsCount &&
                   dp.Parameters.All(IsParameterValid);
        }
        
        private static bool IsParameterValid(TechIndicatorParameter parameter)
        {
            return string.IsNullOrEmpty(parameter.ParameterName) == false &&
                   parameter.ParameterValue != default;
        }
    }
}