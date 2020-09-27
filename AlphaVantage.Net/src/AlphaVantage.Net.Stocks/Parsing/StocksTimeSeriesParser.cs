using System;
using System.Collections.Generic;
using AlphaVantage.Net.Common.Intervals;
using AlphaVantage.Net.Common.Parsing;
using AlphaVantage.Net.Common.TimeSeries;
using AlphaVantage.Net.Stocks.TimeSeries;
// ReSharper disable SimplifyConditionalTernaryExpression

namespace AlphaVantage.Net.Stocks.Parsing
{
    internal sealed class StocksTimeSeriesParser : TimeSeriesParserBase<StockTimeSeries, StockDataPoint>
    {
        private readonly bool _isAdjusted;

        public StocksTimeSeriesParser(Interval interval, bool isAdjusted) : base(interval)
        {
            // intraday time series are never adjusted
            _isAdjusted = interval.IsIntraday() ? false : isAdjusted;
        }

        protected override StockTimeSeries CreateTimeSeriesInstance()
        {
            return new StockTimeSeries()
            {
                IsAdjusted = _isAdjusted
            };
        }

        protected override StockDataPoint CreateDataPointInstance()
        {
            return _isAdjusted ? new StockAdjustedDataPoint() : new StockDataPoint();
        }

        protected override Dictionary<string, Action<StockDataPoint, string>> ParsingDelegates => _parsingDelegates;

        // ReSharper disable once InconsistentNaming
        private static readonly Dictionary<string, Action<StockDataPoint, string>> _parsingDelegates =
            new Dictionary<string, Action<StockDataPoint, string>>()
            {
                {"open", (dataPoint, strValue) => { dataPoint.OpeningPrice = strValue.ParseToDecimal(); }},
                {"high", (dataPoint, strValue) => { dataPoint.HighestPrice = strValue.ParseToDecimal(); }},
                {"low", (dataPoint, strValue) => { dataPoint.LowestPrice = strValue.ParseToDecimal(); }},
                {"close", (dataPoint, strValue) => { dataPoint.ClosingPrice = strValue.ParseToDecimal(); }},
                {"volume", (dataPoint, strValue) => { dataPoint.Volume = strValue.ParseToLong(); }},
                {
                    "adjusted close",
                    (dataPoint, strValue) =>
                    {
                        ((StockAdjustedDataPoint) dataPoint).AdjustedClosingPrice = strValue.ParseToDecimal();
                    }
                },
                {
                    "dividend amount",
                    (dataPoint, strValue) =>
                    {
                        ((StockAdjustedDataPoint) dataPoint).DividendAmount = strValue.ParseToDecimal();
                    }
                },
                {
                    "split coefficient",
                    (dataPoint, strValue) =>
                    {
                        ((StockAdjustedDataPoint) dataPoint).SplitCoefficient = strValue.ParseToDecimal();
                    }
                },
            };
    }
}