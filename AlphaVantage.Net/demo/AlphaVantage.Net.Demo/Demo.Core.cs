using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using AlphaVantage.Net.Common;
using AlphaVantage.Net.Core.Client;

namespace AlphaVantage.Net.Demo
{
    public partial class Demo
    {
        public static async Task CoreDemo()
        {
            // use your AlphaVantage API key
            string apiKey = "1";
            // there are 5 more constructors available
            using var client = new AlphaVantageClient(apiKey);

            // query for intraday time series for Apple Inc:
            var query = new Dictionary<string, string>()
            {
                {"symbol", "AAPL"},
                {"interval", "15min"}
            };
    
            // retrieve response as pure JSON string
            string stringResult = await client.RequestPureJsonAsync(ApiFunction.TIME_SERIES_INTRADAY, query);

            // retrieve response as parsed JsonDocument from System.Text.Json
            JsonDocument parsedResult = await client.RequestParsedJsonAsync(ApiFunction.TIME_SERIES_INTRADAY, query);
        }
    }
}