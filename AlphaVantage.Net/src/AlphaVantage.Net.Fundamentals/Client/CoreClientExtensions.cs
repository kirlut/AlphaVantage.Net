using AlphaVantage.Net.Core.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaVantage.Net.Fundamentals.Client
{
    public static class CoreClientExtension
    {
        /// <summary>
        /// Return <see cref="FundamentalsClient"/> that simplify work with Fundamentals APIs
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public static FundamentalsClient Fundamentals(this AlphaVantageClient client)
        {
            return new FundamentalsClient(client);
        }
    }
}
