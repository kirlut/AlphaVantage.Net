using System.Text.Json.Serialization;
using AlphaVantage.Net.Common.Currencies;

namespace AlphaVantage.Net.Forex
{
    public class ForexExchangeRate : ExchangeRateBase
    {
        [JsonPropertyName("From_Currency Code")]
        public PhysicalCurrency FromCurrencyCode { get; set; }
    }
}