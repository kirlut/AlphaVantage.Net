using System;
using System.Text.Json;
using AlphaVantage.Net.Core.Exceptions;
using AlphaVantage.Net.Core.Parsing;
using AlphaVantage.Net.Stocks.TimeSeries;

namespace AlphaVantage.Net.Stocks.Parsing
{
    internal class IntradayTimeSeriesParser : TimeSeriesParserBase, IAlphaVantageJsonDocumentParser<IntradayTimeSeries>
    {
        private readonly IntradayInterval _interval;

        public IntradayTimeSeriesParser(IntradayInterval interval)
        {
            _interval = interval;
        }

        public IntradayTimeSeries ParseApiResponse(JsonDocument jsonDocument)
        {
            var result = new IntradayTimeSeries()
            {
                Interval = _interval
            };

            try
            {
                result.MetaData = jsonDocument.ExtractMetaData();
                result.DataPoints = GetDataPoints(jsonDocument, false);

                return result;
            }
            catch (Exception ex)
            {
                throw new AlphaVantageParsingException(
                    "Error occured while parsing Stocks Time Series response",
                    ex);
            }
        }
    }
}