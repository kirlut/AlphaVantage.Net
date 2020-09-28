using AlphaVantage.Net.Common.TimeSeries;

// ReSharper disable InconsistentNaming

namespace AlphaVantage.Net.Crypto
{
    public class CryptoDataPoint : DataPointBase
    {
        public decimal OpeningPrice {get; set;}
        
        public decimal OpeningPriceUSD {get; set;}
        
        public decimal ClosingPrice {get; set;}
        
        public decimal ClosingPriceUSD {get; set;}
        
        public decimal HighestPrice {get; set;}
        
        public decimal HighestPriceUSD {get; set;}
        
        public decimal LowestPrice {get; set;}
        
        public decimal LowestPriceUSD {get; set;}
        
        public decimal Volume {get; set;}
        
        public decimal MarketCapitalization {get; set;}
    }
}