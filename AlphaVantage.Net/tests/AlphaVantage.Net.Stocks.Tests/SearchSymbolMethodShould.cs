using System.Linq;
using System.Threading.Tasks;
using AlphaVantage.Net.Core.Client;
using AlphaVantage.Net.Stocks.Client;
using AlphaVantage.Net.TestUtils;
using FluentAssertions;
using Xunit;

namespace AlphaVantage.Net.Stocks.Tests
{
    public class SearchSymbolMethodShould
    {
        private readonly string _apiKey = ConfigProvider.Configuration["ApiKey"];
        
        [Fact]
        public async Task ReturnSearchResults()
        {
            using var client = new AlphaVantageClient(_apiKey);
            using var stocksClient = client.Stocks();
            
            var searchMatches = await stocksClient.SearchSymbolAsync("BA");

            searchMatches.Should().NotBeNull()
                .And.HaveCountGreaterThan(1);

            foreach (var searchMatch in searchMatches)
            {
                searchMatch.Should().NotBeNull()
                    .And.Match<SymbolSearchMatch>(searchMatch =>
                        string.IsNullOrWhiteSpace(searchMatch.Symbol) == false &&
                        string.IsNullOrWhiteSpace(searchMatch.Name) == false &&
                        string.IsNullOrWhiteSpace(searchMatch.Type) == false &&
                        string.IsNullOrWhiteSpace(searchMatch.Region) == false &&
                        string.IsNullOrWhiteSpace(searchMatch.Timezone) == false &&
                        string.IsNullOrWhiteSpace(searchMatch.Currency) == false &&
                        searchMatch.MarketOpen != default &&
                        searchMatch.MarketClose != default &&
                        searchMatch.MatchScore <= 1 && searchMatch.MatchScore > 0
                    );
            }
        }
        
        [Fact]
        public async Task ReturnEmptyCollectionOnMeaninglessKeyword()
        {
            using var client = new AlphaVantageClient(_apiKey);
            using var stocksClient = client.Stocks();

            var searchMatches = await stocksClient.SearchSymbolAsync("wrong_symbol_name");

            searchMatches.Should().NotBeNull().And.BeEmpty();
        }
    }
}