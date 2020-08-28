using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using AlphaVantage.Net.Core.Parsing;
using JetBrains.Annotations;
using NodaTime;
using NodaTime.Serialization.SystemTextJson;
using NodaTime.Text;

namespace AlphaVantage.Net.Stocks.Parsing
{
    public class SearchResultParser : IAlphaVantageJsonParser<ICollection<SymbolSearchMatch>>
    {
        public ICollection<SymbolSearchMatch> ParseApiResponse(string jsonString)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            var localTimeConverter = new NodaPatternConverter<LocalTime>(
                LocalTimePattern.CreateWithInvariantCulture("HH:mm"));   
            options.Converters.Add(localTimeConverter);
            options.Converters.Add(new DecimalConverter());
            
            var searchResult = JsonSerializer.Deserialize<SymbolSearchResult>(jsonString, options);
            return searchResult.BestMatches;
        }

        [UsedImplicitly]
        private class SymbolSearchResult
        {
            [JsonPropertyName("bestMatches")]
            public ICollection<SymbolSearchMatch> BestMatches { get; set; } = new List<SymbolSearchMatch>();
        }
        
        private class DecimalConverter : JsonConverter<decimal>
        {
            public override decimal Read(
                ref Utf8JsonReader reader,
                Type typeToConvert,
                JsonSerializerOptions options) =>
                decimal.Parse(reader.GetString());

            public override void Write(
                Utf8JsonWriter writer,
                decimal dateTimeValue,
                JsonSerializerOptions options) =>
                throw new NotImplementedException();
        }
    }
}