using System.Threading.Tasks;
using AlphaVantage.Net.Core.Client;
using AlphaVantage.Net.Core.Exceptions;
using AlphaVantage.Net.Stocks.Client;
using AlphaVantage.Net.TestUtils;
using FluentAssertions;
using Xunit;

namespace AlphaVantage.Net.Stocks.Tests
{
    public class GetGlobalQuoteMethodShould
    {
        private readonly string _apiKey = ConfigProvider.Configuration["ApiKey"];
        
        [Fact]
        public async Task ReturnGlobalQuote()
        {
            using var client = new AlphaVantageClient(_apiKey);
            using var stocksClient = client.Stocks();
            
            var globalQuote = await stocksClient.GetGlobalQuoteAsync("AAPL");

            globalQuote.Should().NotBeNull()
                .And
                .Match<GlobalQuote>(sm => 
                    string.IsNullOrWhiteSpace(sm.Symbol) == false &&
                    sm.OpeningPrice != default &&
                    sm.PreviousClosingPrice != default &&
                    sm.HighestPrice != default &&
                    sm.LowestPrice != default &&
                    sm.Price != default &&
                    sm.Volume != default &&
                    sm.Change != default &&
                    sm.ChangePercent != default &&
                    sm.LatestTradingDay != default
                );
        }
        
        [Fact]
        public async Task ReturnNull_OnNonExistingSymbol()
        {
            using var client = new AlphaVantageClient(_apiKey);
            using var stocksClient = client.Stocks();

            await Assert.ThrowsAsync<AlphaVantageParsingException>(async () =>
            {
                await stocksClient.GetGlobalQuoteAsync("wrong_symbol_name");
            });
        }
    }
}