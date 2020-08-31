using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using AlphaVantage.Net.Core.Exceptions;
using AlphaVantage.Net.Core.Parsing;
using JetBrains.Annotations;

namespace AlphaVantage.Net.Stocks.Parsing
{
    public class GlobalQuoteParser : IAlphaVantageJsonParser<GlobalQuote?>
    {
        private static readonly JsonSerializerOptions SerializerOptions =
            SerializerOptionsFactory.GetSerializerOptions();
        
        public GlobalQuote? ParseApiResponse(string jsonString)
        {
            if (jsonString.Contains("{}")) throw new AlphaVantageParsingException(jsonString);
                
            try
            {
                var quoteWrapper = JsonSerializer.Deserialize<GlobalQuoteWrapper>(jsonString, SerializerOptions);
                return quoteWrapper.GlobalQuote;
            }
            catch (Exception ex)
            {
                throw new AlphaVantageParsingException(
                    "Error occured while parsing Global Quote response", 
                    ex);
            }
        }

        [UsedImplicitly]
        private class GlobalQuoteWrapper
        {
            [JsonPropertyName("Global Quote")]
            public GlobalQuote? GlobalQuote { get; set; }
        }
    }
}