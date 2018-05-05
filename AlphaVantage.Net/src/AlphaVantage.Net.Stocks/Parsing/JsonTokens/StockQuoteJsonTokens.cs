namespace AlphaVantage.Net.Stocks.Parsing.JsonTokens
{
    internal static class StockQuoteJsonTokens
    {
        public const string StockQuotesHeader = "Stock Quotes";

        public const string SymbolToken = "1. symbol";
        public const string PriceToken = "2. price";
        public const string VolumeToken = "3. volume";
        public const string TimestampToken = "4. timestamp";
    }
}