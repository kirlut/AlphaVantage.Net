using AlphaVantage.Net.Currencies.Common;

namespace AlphaVantage.Net.Cryptocurrencies
{
    public class CryptoExchangeRate : ExchangeRateBase
    {
        public DigitalCurrency FromCurrencyCode { get; set; }
    }
}