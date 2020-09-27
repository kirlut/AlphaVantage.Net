using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using AlphaVantage.Net.Common.Parsing;
using AlphaVantage.Net.Core.Exceptions;

namespace AlphaVantage.Net.TechnicalIndicators.Parsing
{
    internal class TechIndicatorsResultParser : IAlphaVantageJsonDocumentParser<TechIndicatorResult>
    {
        public TechIndicatorResult ParseApiResponse(JsonDocument jsonDocument)
        {
            var result = new TechIndicatorResult();
            
            try
            {
                result.MetaData = jsonDocument.ExtractMetaData();
                result.DataPoints = GetDataPoints(jsonDocument);
                
                return result;
            }
            catch (Exception ex)
            {
                throw new AlphaVantageParsingException(
                    "Error occured while parsing Technical Indicators response",
                    ex);
            }
        }

        private static List<TechIndicatorDataPoint> GetDataPoints(JsonDocument jsonDocument)
        {
            var result = new List<TechIndicatorDataPoint>();

            var dataPointsJsonElement = jsonDocument.RootElement.EnumerateObject().Last().Value;

            foreach (var dataPointJson in dataPointsJsonElement.EnumerateObject())
            {
                var dataPointFieldsJson = dataPointJson.Value;

                var dataPoint = new TechIndicatorDataPoint
                {
                    Time = dataPointJson.Name.ParseToDateTime(),
                    Parameters = GetParameters(dataPointFieldsJson)
                };
                
                result.Add(dataPoint);
            }

            return result;
        }

        private static List<TechIndicatorParameter> GetParameters(JsonElement dataPointFieldsJson)
        {
            var result = new List<TechIndicatorParameter>();
            
            foreach (var fieldJson in dataPointFieldsJson.EnumerateObject())
            {
                var techIndicatorParameter = new TechIndicatorParameter()
                {
                    ParameterName = fieldJson.Name,
                    ParameterValue = fieldJson.Value.ToString().ParseToDecimal()
                };
                
                result.Add(techIndicatorParameter);
            }

            return result;
        }
    }
}