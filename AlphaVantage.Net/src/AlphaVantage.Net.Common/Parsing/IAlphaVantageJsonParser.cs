namespace AlphaVantage.Net.Common.Parsing
{
    public interface IAlphaVantageJsonParser<out T>
    {
        T ParseApiResponse(string jsonString);
    }
}