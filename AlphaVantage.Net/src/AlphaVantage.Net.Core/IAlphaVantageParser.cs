using System.Text.Json;

namespace AlphaVantage.Net.Core
{
    public interface IAlphaVantageParser<out T>
    {
        T ParseApiResponse(JsonDocument jsonDocument);
    }
}