using System.Threading.Tasks;
using AlphaVantage.Net.Stocks.SymbolSearch;
using AlphaVantage.Net.Stocks.TimeSeries;

namespace AlphaVantage.Net.Stocks.Client
{
    public static class StocksClientExtensions
    {
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
        
        public static Task<GlobalQuote> GetGlobalQuoteAsync(this StocksClient stocksClient, string symbol)
        {
            return Task.FromResult(new GlobalQuote());
        }
        
        public static Task<SymbolSearchResult> SearchSymbolAsync(this StocksClient stocksClient, string keyWords)
        {
            return Task.FromResult(new SymbolSearchResult());
        }
    }
}