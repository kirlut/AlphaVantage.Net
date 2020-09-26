using AlphaVantage.Net.Core.Client;

namespace AlphaVantage.Net.Cryptocurrencies.Crypto
{
    public static class CoreClientExtension
    {
        /// <summary>
        /// Return <see cref="CryptoClient"/> that simplify work with Cryptocurrencies APIs
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public static CryptoClient Forex(this AlphaVantageClient client)
        {
            return new CryptoClient(client);
        }
    }
}