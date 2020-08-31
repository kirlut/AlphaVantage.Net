using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using AlphaVantage.Net.Core.Exceptions;
using AlphaVantage.Net.Core.Parsing;
using JetBrains.Annotations;
using NodaTime;
using NodaTime.Serialization.SystemTextJson;
using NodaTime.Text;

namespace AlphaVantage.Net.Stocks.Parsing
{
    public class SearchResultParser : IAlphaVantageJsonParser<ICollection<SymbolSearchMatch>>
    {
        private static readonly JsonSerializerOptions SerializerOptions =
            SerializerOptionsFactory.GetSerializerOptions();

        public ICollection<SymbolSearchMatch> ParseApiResponse(string jsonString)
        {
            try
            {
                var searchResult = JsonSerializer.Deserialize<SymbolSearchResult>(jsonString, SerializerOptions);
                return searchResult.BestMatches;
            }
            catch (Exception ex)
            {
                throw new AlphaVantageParsingException(
                    "Error occured while parsing Symbol Search response",
                    ex);
            }
        }

        [UsedImplicitly]
        private class SymbolSearchResult
        {
            [JsonPropertyName("bestMatches")]
            public ICollection<SymbolSearchMatch> BestMatches { get; set; } = new List<SymbolSearchMatch>();
        }
    }
}