using System.Text.Json;
using AlphaVantage.Net.Common.Parsing;

namespace AlphaVantage.Net.Forex.Parsing
{
    internal class TimeSeriesParser : IAlphaVantageJsonDocumentParser<ForexTimeSeries>
    {
        public ForexTimeSeries ParseApiResponse(JsonDocument jsonDocument)
        {
            throw new System.NotImplementedException();
        }
    }
}