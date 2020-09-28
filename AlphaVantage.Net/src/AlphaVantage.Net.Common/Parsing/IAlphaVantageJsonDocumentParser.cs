using System.Text.Json;

namespace AlphaVantage.Net.Common.Parsing
{
    public interface IAlphaVantageJsonDocumentParser<out T>
    {
        T ParseApiResponse(JsonDocument jsonDocument);
    }
}