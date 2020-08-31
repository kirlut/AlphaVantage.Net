using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text.Json;
using AlphaVantage.Net.Core.Exceptions;
using AlphaVantage.Net.Core.Parsing;
using AlphaVantage.Net.Stocks.TimeSeries;

namespace AlphaVantage.Net.Stocks.Parsing
{
    public class StockTimeSeriesParser : IAlphaVantageJsonDocumentParser<StockTimeSeries>
    {
        private readonly TimeSeriesType _type;
        private readonly bool _isAdjusted;

        public StockTimeSeriesParser(TimeSeriesType type, bool isAdjusted)
        {
            _isAdjusted = isAdjusted;
            _type = type;
        }

        public StockTimeSeries ParseApiResponse(JsonDocument jsonDocument)
        {
            var result = new StockTimeSeries()
            {
                Type = _type,
                IsAdjusted = _isAdjusted,
            };

            try
            {
                result.MetaData = jsonDocument.ExtractMetaData();

                var dataPointsJsonElement = jsonDocument.RootElement.EnumerateObject().Last().Value;
                result.DataPoints = GetDataPoints(dataPointsJsonElement);

                return result;
            }
            catch (Exception ex)
            {
                throw new AlphaVantageParsingException(
                    "Error occured while parsing Stocks Time Series response",
                    ex);
            }
        }

        private List<StockDataPoint> GetDataPoints(JsonElement jsonElement)
        {
            var result = new List<StockDataPoint>();

            foreach (var dataPointJson in jsonElement.EnumerateObject())
            {
                var dataPoint = _isAdjusted ? new StockAdjustedDataPoint() : new StockDataPoint();
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