using System;
using System.Collections.Generic;
using AlphaVantage.Net.Core;
using AlphaVantage.Net.Stocks.BatchQuotes;
using AlphaVantage.Net.Stocks.TimeSeries;
using AlphaVantage.Net.Stocks.Validation;

namespace AlphaVantage.Net.Stocks
{
    /// <summary>
    /// Client for Alpha Vantage API (stock data only)
    /// </summary>
    public class AlphaVantageStocksClient
    {
        private readonly string _apiKey;
        private readonly AlphaVantageCoreClient _coreClient;
        
        public AlphaVantageStocksClient(string apiKey)
        {
            if(string.IsNullOrWhiteSpace(apiKey)) throw new ArgumentNullException(nameof(apiKey));
            
            _apiKey = apiKey;
            _coreClient = new AlphaVantageCoreClient(new StocksApiCallValidator());
        }

        public StockTimeSeries RequestIntradayTimeSeries(
            string symbol, 
            IntradayInterval interval = IntradayInterval.SixtyMin, 
            TimeSeriesSize size = TimeSeriesSize.Compact)
        {
            //todo:
            return null;
        }

        public StockTimeSeries RequestDailyTimeSeries(
            string symbol, 
            TimeSeriesSize size = TimeSeriesSize.Compact, 
            bool adjusted = false)
        {
            //todo:
            return null;
        }
        
        public StockTimeSeries RequestWeeklyTimeSeries(string symbol, bool adjusted = false)
        {
            //todo:
            return null;
        }
        
        public StockTimeSeries RequestMonthlyTimeSeries(string symbol, bool adjusted = false)
        {
            //todo:
            return null;
        }

        public ICollection<StockQuote> RequestBatchQuotes(string[] symbols)
        {
            //todo
            return null;
        }
    }
}