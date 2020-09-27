using System;
using System.IO.Compression;
using System.Text.Json;
using System.Text.Json.Serialization;
using AlphaVantage.Net.Common.Exceptions;
using JetBrains.Annotations;

namespace AlphaVantage.Net.Common.Parsing
{
    public class WrappedValueParser<T> : IAlphaVantageJsonParser<T>
    where T: class
    {
        private readonly string _wrappedValuePropertyName;

        public WrappedValueParser(string wrappedValuePropertyName)
        {
            _wrappedValuePropertyName = wrappedValuePropertyName;
        }

        public T ParseApiResponse(string jsonString)
        {
            if (jsonString.Contains("{}")) throw new AlphaVantageParsingException("response is empty: " + jsonString);

            Wrapper quoteWrapper;
            try
            {
                var serializerOptions = SerializerOptionsFactory.GetSerializerOptions();
                serializerOptions.PropertyNamingPolicy = new WrappedValueNamingPolicy(_wrappedValuePropertyName);
                
                quoteWrapper = JsonSerializer.Deserialize<Wrapper>(jsonString, serializerOptions);
            }
            catch (Exception ex)
            {
                throw new AlphaVantageParsingException(
                    "Error occured while parsing response", 
                    ex);
            }
            
            return quoteWrapper?.WrappedValue ?? throw new AlphaVantageParsingException("Can't parse json: " + jsonString);
        }
        
        public class WrappedValueNamingPolicy : JsonNamingPolicy
        {
            private readonly string _wrappedValuePropertyName;

            public WrappedValueNamingPolicy(string wrappedValuePropertyName)
            {
                _wrappedValuePropertyName = wrappedValuePropertyName;
            }

            public override string ConvertName(string name)
            {
                if (name != "WrappedValue") return name;
                
                return _wrappedValuePropertyName;
            }
        }
        
        [UsedImplicitly]
        private class Wrapper
        {
            public T? WrappedValue { get; set; }
        }
    }
}