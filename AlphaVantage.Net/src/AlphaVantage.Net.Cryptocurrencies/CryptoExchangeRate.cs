using AlphaVantage.Net.Common.Currencies;

namespace AlphaVantage.Net.Cryptocurrencies
{
    public class CryptoExchangeRate : ExchangeRateBase
    {
        public DigitalCurrency FromCurrencyCode { get; set; }
    }
}