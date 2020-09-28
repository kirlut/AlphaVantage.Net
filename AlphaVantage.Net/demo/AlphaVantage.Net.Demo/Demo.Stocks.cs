using System.Collections.Generic;
using System.Threading.Tasks;
using AlphaVantage.Net.Common.Intervals;
using AlphaVantage.Net.Common.Size;
using AlphaVantage.Net.Core.Client;
using AlphaVantage.Net.Stocks;
using AlphaVantage.Net.Stocks.Client;

namespace AlphaVantage.Net.Demo
{
    public partial class Demo
    {
        public static async Task StocksDemo()
        {
            // use your AlphaVantage API key
            string apiKey = "1";
            // there are 5 more constructors available
            using var client = new AlphaVantageClient(apiKey);
            using var stocksClient = client.Stocks();

            StockTimeSeries stockTs = await stocksClient.GetTimeSeriesAsync("AAPL", Interval.Daily, OutputSize.Compact, isAdjusted: true);

            GlobalQuote globalQuote = await stocksClient.GetGlobalQuoteAsync("AAPL");

            ICollection<SymbolSearchMatch> searchMatches = await stocksClient.SearchSymbolAsync("BA");
        }
    }
}