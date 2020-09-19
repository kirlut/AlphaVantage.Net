using System.Text.Json;
using AlphaVantage.Net.Core.Parsing;

namespace AlphaVantage.Net.TechnicalIndicators.Parsing
{
    public class TechIndicatorsResultParser : IAlphaVantageJsonDocumentParser<TechIndicatorResult>
    {
        public TechIndicatorResult ParseApiResponse(JsonDocument jsonDocument)
        {
            throw new System.NotImplementedException();
        }
    }
}