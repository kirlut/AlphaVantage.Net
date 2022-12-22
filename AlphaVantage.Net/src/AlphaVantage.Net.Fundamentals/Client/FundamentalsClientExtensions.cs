using AlphaVantage.Net.Common;
using AlphaVantage.Net.Fundamentals.Data;
using AlphaVantage.Net.Fundamentals.Parsing;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AlphaVantage.Net.Fundamentals.Client
{
    public static class FundamentalsClientExtensions
    {

        public static async Task<Earnings> GetEarningsAsync(this FundamentalsClient fundamentalsClient, string symbol)
        {
            var parser = new EarningsParser();

            var query = new Dictionary<string, string>()
            {
                {ApiQueryConstants.SymbolQueryVar, symbol},
            };


            return await fundamentalsClient.RequestApiAsync(parser, ApiFunction.EARNINGS, query);
        }

        public static async Task<CompanyOverview> GetCompanyOverviewAsync(this FundamentalsClient fundamentalsClient, string symbol)
        {
            var parser = new OverviewParser();

            var query = new Dictionary<string, string>()
            {
                {ApiQueryConstants.SymbolQueryVar, symbol},
            };

            return await fundamentalsClient.RequestApiAsync(parser, ApiFunction.OVERVIEW, query);
        }
    }
}
