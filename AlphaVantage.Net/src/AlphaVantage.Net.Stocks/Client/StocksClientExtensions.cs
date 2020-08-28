using System.Collections.Generic;
using System.Threading.Tasks;
using AlphaVantage.Net.Core;
using AlphaVantage.Net.Stocks.Parsing;
using AlphaVantage.Net.Stocks.TimeSeries;

namespace AlphaVantage.Net.Stocks.Client
{
    public static class StocksClientExtensions
    {
        private static readonly GlobalQuoteParser GlobalQuoteParser = new GlobalQuoteParser();
        private static readonly SearchResultParser SearchResultParser = new SearchResultParser();
        
        public static Task<StockTimeSeries> GetTimeSeriesAsync(this StocksClient stocksClient, 
            string symbol, 
            IntradayInterval interval,
            TimeSeriesSize size,
            bool isAdjusted = false)
        {
            return Task.FromResult(new StockTimeSeries());
        }
        
        public static Task<StockTimeSeries> GetIntradayTimeSeriesAsync(this StocksClient stocksClient, 
            string symbol, 
            IntradayInterval interval,
            TimeSeriesSize size)
        {
            return Task.FromResult(new StockTimeSeries());
        }
        
        /// <summary>
        /// Returns the price and volume information for a symbol
        /// </summary>
        /// <param name="stocksClient"></param>
        /// <param name="symbol"></param>
        /// <remarks>
        /// function=GLOBAL_QUOTE
        /// </remarks>
        public static async Task<GlobalQuote?> GetGlobalQuoteAsync(this StocksClient stocksClient, string symbol)
        {
            var query = new Dictionary<string, string>(){{"symbol", symbol}};
            return await stocksClient.RequestApiAsync(GlobalQuoteParser, ApiFunction.GLOBAL_QUOTE, query);
        }
        
        /// <summary>
        /// Returns the best-matching symbols and market information based on keywords
        /// </summary>
        /// <param name="stocksClient"></param>
        /// <param name="keyWords"></param>
        /// <remarks>
        /// function=SYMBOL_SEARCH
        /// </remarks>
        public static async Task<ICollection<SymbolSearchMatch>> SearchSymbolAsync(this StocksClient stocksClient, string keyWords)
        {
            var query = new Dictionary<string, string>(){{"keywords", keyWords}};
            return await stocksClient.RequestApiAsync(SearchResultParser, ApiFunction.SYMBOL_SEARCH, query);
        }
    }
}