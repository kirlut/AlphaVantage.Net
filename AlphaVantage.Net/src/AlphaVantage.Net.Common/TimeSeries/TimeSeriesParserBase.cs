using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using AlphaVantage.Net.Common.Exceptions;
using AlphaVantage.Net.Common.Intervals;
using AlphaVantage.Net.Common.Parsing;

namespace AlphaVantage.Net.Common.TimeSeries
{
    public abstract class TimeSeriesParserBase<TTimeSeries, TDataPoint> : IAlphaVantageJsonDocumentParser<TTimeSeries>
        where TDataPoint : DataPointBase
        where TTimeSeries : TimeSeriesBase<TDataPoint>
    {
        protected abstract TTimeSeries CreateTimeSeriesInstance();

        protected abstract TDataPoint CreateDataPointInstance();

        protected abstract Dictionary<string, Action<TDataPoint, string>> ParsingDelegates { get; }

        protected readonly Interval TimeSeriesInterval;

        protected TimeSeriesParserBase(Interval timeSeriesInterval)
        {
            TimeSeriesInterval = timeSeriesInterval;
        }

        public TTimeSeries ParseApiResponse(JsonDocument jsonDocument)
        {
            var result = CreateTimeSeriesInstance();
            result.Interval = TimeSeriesInterval;

            try
            {
                result.MetaData = jsonDocument.ExtractMetaData();
                result.DataPoints = GetDataPoints(jsonDocument);

                return result;
            }
            catch (Exception ex)
            {
                throw new AlphaVantageParsingException(
                    "Error occured while parsing Time Series response",
                    ex);
            }
        }
        
        private List<TDataPoint> GetDataPoints(JsonDocument jsonDocument)
        {
            var result = new List<TDataPoint>();

            var dataPointsJsonElement = jsonDocument.RootElement.EnumerateObject().Last().Value;

            foreach (var dataPointJson in dataPointsJsonElement.EnumerateObject())
            {
                var dataPoint = CreateDataPointInstance();
                dataPoint.Time = dataPointJson.Name.ParseToDateTime();

                var dataPointFieldsJson = dataPointJson.Value;
                EnrichDataPointFields(dataPoint, dataPointFieldsJson);

                result.Add(dataPoint);
            }

            return result;
        }

        private void EnrichDataPointFields(TDataPoint dataPoint, JsonElement dataPointFieldsJson)
        {
            foreach (var fieldJson in dataPointFieldsJson.EnumerateObject())
            {
                if (ParsingDelegates.ContainsKey(fieldJson.Name) == false) continue;

                ParsingDelegates[fieldJson.Name].Invoke(dataPoint, fieldJson.Value.GetString());
            }
        }
    }
}