using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using NodaTime;
using NodaTime.Serialization.SystemTextJson;
using NodaTime.Text;

namespace AlphaVantage.Net.Core.Parsing
{
    public static class SerializerOptionsFactory
    {
        public static JsonSerializerOptions GetSerializerOptions()
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var localTimeConverter = new NodaPatternConverter<LocalTime>(
                LocalTimePattern.CreateWithInvariantCulture("HH:mm"));
            var localDateConverter = new NodaPatternConverter<LocalDate>(
                LocalDatePattern.CreateWithInvariantCulture("yyyy-MM-dd"));

            options.Converters.Add(localTimeConverter);
            options.Converters.Add(localDateConverter);
            options.Converters.Add(new DecimalConverter());
            options.Converters.Add(new LongConverter());

            return options;
        }

        private class DecimalConverter : JsonConverter<decimal>
        {
            public override decimal Read(
                ref Utf8JsonReader reader,
                Type typeToConvert,
                JsonSerializerOptions options) =>
                reader.GetString().Replace("%", "").ParseToDecimal();

            public override void Write(
                Utf8JsonWriter writer,
                decimal dateTimeValue,
                JsonSerializerOptions options) =>
                throw new NotImplementedException();
        }

        private class LongConverter : JsonConverter<long>
        {
            public override long Read(
                ref Utf8JsonReader reader,
                Type typeToConvert,
                JsonSerializerOptions options) =>
                reader.GetString().ParseToLong();

            public override void Write(
                Utf8JsonWriter writer,
                long dateTimeValue,
                JsonSerializerOptions options) =>
                throw new NotImplementedException();
        }
    }
}