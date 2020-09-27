using AlphaVantage.Net.Common.Currencies;

namespace AlphaVantage.Net.Forex
{
    public class ForexExchangeRate : ExchangeRateBase
    {
        public PhysicalCurrency FromCurrencyCode { get; set; }
    }
}