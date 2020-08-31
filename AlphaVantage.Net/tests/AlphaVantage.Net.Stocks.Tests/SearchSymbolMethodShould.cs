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
                    .And.Match<SymbolSearchMatch>(sm =>
                        string.IsNullOrWhiteSpace(sm.Symbol) == false &&
                        string.IsNullOrWhiteSpace(sm.Name) == false &&
                        string.IsNullOrWhiteSpace(sm.Type) == false &&
                        string.IsNullOrWhiteSpace(sm.Region) == false &&
                        string.IsNullOrWhiteSpace(sm.Timezone) == false &&
                        string.IsNullOrWhiteSpace(sm.Currency) == false &&
                        sm.MarketOpen != default &&
                        sm.MarketClose != default &&
                        sm.MatchScore <= 1 && sm.MatchScore > 0
                    );
            }
        }
        
        [Fact]
        public async Task ReturnEmptyCollection_OnMeaninglessKeyword()
        {
            using var client = new AlphaVantageClient(_apiKey);
            using var stocksClient = client.Stocks();

            var searchMatches = await stocksClient.SearchSymbolAsync("wrong_symbol_name");

            searchMatches.Should().NotBeNull().And.BeEmpty();
        }
    }
}