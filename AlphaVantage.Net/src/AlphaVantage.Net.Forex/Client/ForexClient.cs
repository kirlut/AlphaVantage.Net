using AlphaVantage.Net.Core.Client;
using JetBrains.Annotations;

namespace AlphaVantage.Net.Forex.Client
{
    public class ForexClient : TypedAlphaVantageClient
    {
        internal ForexClient([NotNull] AlphaVantageClient alphaVantageClient) : base(alphaVantageClient)
        {
        }
    }
}