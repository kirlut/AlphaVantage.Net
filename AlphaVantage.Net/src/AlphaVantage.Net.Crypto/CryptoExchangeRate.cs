using System.Text.Json.Serialization;
using AlphaVantage.Net.Common.Currencies;

namespace AlphaVantage.Net.Crypto
{
    public class CryptoExchangeRate : ExchangeRateBase
    {
        [JsonPropertyName("From_Currency Code")]
        public DigitalCurrency FromCurrencyCode { get; set; }
    }
}