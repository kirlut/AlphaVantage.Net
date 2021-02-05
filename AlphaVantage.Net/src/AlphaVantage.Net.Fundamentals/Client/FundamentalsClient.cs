using AlphaVantage.Net.Common;
using AlphaVantage.Net.Core.Client;
using AlphaVantage.Net.Fundamentals.Data;
using AlphaVantage.Net.Fundamentals.Parsing;
using JetBrains.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlphaVantage.Net.Fundamentals.Client
{
    public sealed class FundamentalsClient : TypedAlphaVantageClient
    {
        internal FundamentalsClient(AlphaVantageClient alphaVantageClient) : base(alphaVantageClient)
        {
        }


       
    }
}
