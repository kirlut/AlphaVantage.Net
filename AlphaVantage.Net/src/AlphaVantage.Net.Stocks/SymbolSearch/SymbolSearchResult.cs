using System.Collections.Generic;

namespace AlphaVantage.Net.Stocks.SymbolSearch
{
    public class SymbolSearchResult
    {
        public ICollection<SymbolSearchMatch> BestMatches { get; } = new List<SymbolSearchMatch>();
    }
}