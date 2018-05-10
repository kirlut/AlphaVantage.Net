using System;
using System.Collections.Generic;
using System.Linq;
using AlphaVantage.Net.Stocks.BatchQuotes;
using AlphaVantage.Net.Stocks.Parsing.Exceptions;
using AlphaVantage.Net.Stocks.Parsing.JsonTokens;
using Newtonsoft.Json.Linq;

namespace AlphaVantage.Net.Stocks.Parsing
{
    internal partial class StockDataParser
    {
        public ICollection<StockQuote> ParseStockQuotes(JObject jObject)
        {
            if(jObject == null) throw new ArgumentNullException(nameof(jObject));

            try
            {
                var properties = jObject.Children().Select(ch => (JProperty) ch).ToArray();
                var stockQuotesJson = properties.FirstOrDefault(p => p.Name == StockQuoteJsonTokens.StockQuotesHeader);
                if (stockQuotesJson == null)
                    throw new StocksParsingException("Unable to parse stock quotes");

                var result = new List<StockQuote>();
                var contentDict = new Dictionary<string, string>();
                foreach (var quoteJson in stockQuotesJson.Value)
                {
                    var quoteProperties = quoteJson.Children().Select(q => (JProperty) q).ToArray();
                    foreach (var quoteProperty in quoteProperties)
                    {
                        contentDict.Add(quoteProperty.Name, quoteProperty.Value.ToString());
                    }

                    var quote = ComposeStockQuote(contentDict);
                    contentDict.Clear();

                    result.Add(quote);
                }

                return result;
            }
            catch (StocksParsingException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new StocksParsingException("Unable to parse data. See the inner exception for details", ex);
            }
        }

        private StockQuote ComposeStockQuote(IDictionary<string, string> stockQuoteContent)
        {
            return new StockQuote()
            {
                Symbol = stockQuoteContent[StockQuoteJsonTokens.SymbolToken],
                Time = DateTime.Parse(stockQuoteContent[StockQuoteJsonTokens.TimestampToken]),
                Price = Decimal.Parse(stockQuoteContent[StockQuoteJsonTokens.PriceToken]),
                Volume = Int64.Parse(stockQuoteContent[StockQuoteJsonTokens.VolumeToken])
            };
        }
    }
}