using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.Json;
using AlphaVantage.Net.Common.Intervals;
using AlphaVantage.Net.Common.Parsing;
using AlphaVantage.Net.Core.Exceptions;
using AlphaVantage.Net.Stocks.TimeSeries;

namespace AlphaVantage.Net.Stocks.Parsing
{
    internal class TimeSeriesParser : IAlphaVantageJsonDocumentParser<StockTimeSeries>
    {
        private readonly Interval _interval;
        private readonly bool _isAdjusted;

        [SuppressMessage("ReSharper", "SimplifyConditionalTernaryExpression")]
        public TimeSeriesParser(Interval interval, bool isAdjusted)
        {
            _interval = interval;
            // intraday time series are never adjusted
            _isAdjusted = interval.IsIntraday() ? false : isAdjusted;
        }

        public StockTimeSeries ParseApiResponse(JsonDocument jsonDocument)
        {
            var result = new StockTimeSeries()
            {
                Interval = _interval, 
                IsAdjusted = _isAdjusted
            };

            try
            {
                result.MetaData = jsonDocument.ExtractMetaData();
                result.DataPoints = GetDataPoints(jsonDocument, _isAdjusted);

                return result;
            }
            catch (Exception ex)
            {
                throw new AlphaVantageParsingException(
                    "Error occured while parsing Stocks Time Series response",
                    ex);
            }
        }
        
        private static List<StockDataPoint> GetDataPoints(JsonDocument jsonDocument, bool isAdjusted)
        {
            var result = new List<StockDataPoint>();

            var dataPointsJsonElement = jsonDocument.RootElement.EnumerateObject().Last().Value;

            foreach (var dataPointJson in dataPointsJsonElement.EnumerateObject())
            {
                var dataPoint = isAdjusted ? new StockAdjustedDataPoint() : new StockDataPoint();
                dataPoint.Time = dataPointJson.Name.ParseToDateTime();

                var dataPointFieldsJson = dataPointJson.Value;
                EnrichDataPointFields(dataPoint, dataPointFieldsJson);

                result.Add(dataPoint);
            }

            return result;
        }

        private static void EnrichDataPointFields(StockDataPoint dataPoint, JsonElement dataPointFieldsJson)
        {
            foreach (var fieldJson in dataPointFieldsJson.EnumerateObject())
            {
                if (ParsingDelegates.ContainsKey(fieldJson.Name) == false) continue;

                ParsingDelegates[fieldJson.Name].Invoke(dataPoint, fieldJson.Value.GetString());
            }
        }

        private static readonly Dictionary<string, Action<StockDataPoint, string>> ParsingDelegates =
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