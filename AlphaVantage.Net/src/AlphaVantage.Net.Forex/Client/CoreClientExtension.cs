using AlphaVantage.Net.Core.Client;

namespace AlphaVantage.Net.Forex.Client
{
    public static class CoreClientExtension
    {
        /// <summary>
        /// Return <see cref="ForexClient"/> that simplify work with Forex APIs
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public static ForexClient Forex(this AlphaVantageClient client)
        {
            return new ForexClient(client);
        }
    }
}