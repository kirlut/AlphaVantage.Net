using AlphaVantage.Net.Currencies.Common;

namespace AlphaVantage.Net.Forex
{
    public class ForexExchangeRate : ExchangeRateBase
    {
        public PhysicalCurrency FromCurrencyCode { get; set; }
    }
}