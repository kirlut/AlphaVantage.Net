using AlphaVantage.Net.Core.Client;
using JetBrains.Annotations;

namespace AlphaVantage.Net.TechnicalIndicators.Client
{
    internal class TechnicalIndicatorsClient : TypedAlphaVantageClient
    {
        public TechnicalIndicatorsClient([NotNull] AlphaVantageClient alphaVantageClient) : base(alphaVantageClient)
        {
        }
    }
}