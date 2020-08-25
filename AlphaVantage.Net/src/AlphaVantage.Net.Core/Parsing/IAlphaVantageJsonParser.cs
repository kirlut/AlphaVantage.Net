namespace AlphaVantage.Net.Core.Parsing
{
    public interface IAlphaVantageJsonParser<out T>
    {
        T ParseApiResponse(string jsonString);
    }
}