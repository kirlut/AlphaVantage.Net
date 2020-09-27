using System;

namespace AlphaVantage.Net.Common.Currencies
{
    public abstract class ExchangeRateBase
    {
        public string FromCurrencyName { get; set; } = String.Empty;

        public PhysicalCurrency ToCurrencyCode { get; set; }
        
        public string ToCurrencyName { get; set; } = String.Empty;
        
        public decimal ExchangeRate { get; set; }
        
        public DateTime LastRefreshed { get; set; }
        
        public string TimeZone { get; set; } = String.Empty;
        
        public decimal BidPrice { get; set; }
        
        public decimal AskPrice { get; set; }
    }
}