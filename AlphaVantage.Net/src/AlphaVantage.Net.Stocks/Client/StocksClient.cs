using AlphaVantage.Net.Core.Client;
using JetBrains.Annotations;

namespace AlphaVantage.Net.Stocks.Client
{
    public sealed class StocksClient : TypedAlphaVantageClient
    {
        internal StocksClient([NotNull] AlphaVantageClient alphaVantageClient) : base(alphaVantageClient)
        {
        }
    }
}