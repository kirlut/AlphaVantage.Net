using System.Text.Json;

namespace AlphaVantage.Net.Core.Parsing
{
    public interface IAlphaVantageJsonDocumentParser<out T>
    {
        T ParseApiResponse(JsonDocument jsonDocument);
    }
}