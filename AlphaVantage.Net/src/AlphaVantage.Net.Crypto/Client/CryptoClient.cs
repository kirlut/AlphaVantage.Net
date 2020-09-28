using AlphaVantage.Net.Core.Client;
using JetBrains.Annotations;

namespace AlphaVantage.Net.Crypto.Client
{
    public class CryptoClient : TypedAlphaVantageClient
    {
        public CryptoClient([NotNull] AlphaVantageClient alphaVantageClient) : base(alphaVantageClient)
        {
        }
    }
}