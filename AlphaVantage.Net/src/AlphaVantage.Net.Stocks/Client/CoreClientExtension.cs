using AlphaVantage.Net.Core.Client;

namespace AlphaVantage.Net.Stocks.Client
{
    public static class CoreClientExtension
    {
        public static StocksClient Stocks(this AlphaVantageClient client)
        {
            return new StocksClient(client);
        }
    }
}