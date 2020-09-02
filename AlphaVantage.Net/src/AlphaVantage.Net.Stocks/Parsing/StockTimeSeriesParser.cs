using System;
using System.Text.Json;
using AlphaVantage.Net.Core.Exceptions;
using AlphaVantage.Net.Core.Parsing;
using AlphaVantage.Net.Stocks.TimeSeries;

namespace AlphaVantage.Net.Stocks.Parsing
{
    internal class StockTimeSeriesParser : TimeSeriesParserBase, IAlphaVantageJsonDocumentParser<StockTimeSeries>
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
    }
}