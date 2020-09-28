using AlphaVantage.Net.Core.Client;

namespace AlphaVantage.Net.Stocks.Client
{
    public static class CoreClientExtension
    {
        /// <summary>
        /// Return <see cref="StocksClient"/> that simplify work with Stock APIs
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public static StocksClient Stocks(this AlphaVantageClient client)
        {
            return new StocksClient(client);
        }
    }
}