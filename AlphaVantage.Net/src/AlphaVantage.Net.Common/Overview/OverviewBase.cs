using AlphaVantage.Net.Common.Currencies;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaVantage.Net.Common.Overview
{
    public abstract class OverviewBase
    {
        public string Symbol { get; set; } = String.Empty;
        public string AssetType { get; set; } = String.Empty;//probably could become an enum
        public string Name { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
        public string Exchange { get; set; } = String.Empty; //probably could become an enum
        public PhysicalCurrency PhysicalCurrency { get; set; }
        public string Country { get; set; } = String.Empty;//probably could become an enum

        public string Sector { get; set; } = String.Empty;
        public string Industry { get; set; } = String.Empty;
        public string Address { get; set; } = String.Empty;
        public int FullTimeEmployees { get; set; }
        public string FiscalYearEnd { get; set; } = String.Empty;
        public DateTime LatestQuarter { get; set; }
        public long MarketCapitalization { get; set; }
        public long EBITDA { get; set; }
        public decimal PERatio { get; set; }
        public decimal PEGRatio { get; set; }
        public decimal BookValue { get; set; }
        public decimal DividendPerShare { get; set; }
        public decimal DividendYield { get; set; }
        public decimal EPS { get; set; }
        public decimal RevenuePerShareTTM { get; set; }
        public decimal ProfitMargin { get; set; }
        public decimal OperatingMarginTTM { get; set; }
        public decimal ReturnOnAssetsTTM { get; set; }
        public decimal ReturnOnEquityTTM { get; set; }
        public long RevenueTTM { get; set; }
        public long GrossProfitTTM { get; set; }
        public decimal DilutedEPSTTM { get; set; }
        public decimal QuarterlyEarningsGrowthYOY { get; set; }
        public decimal QuarterlyRevenueGrowthYOY { get; set; }
        public decimal AnalystTargetPrice { get; set; }
        public decimal TrailingPE { get; set; }
        public decimal ForwardPE { get; set; }
        public decimal PriceToSalesRatioTTM { get; set; }
        public decimal PriceToBookRatio { get; set; }
        public decimal EVToRevenue { get; set; }
        public decimal EVToEBITDA { get; set; }
        public decimal Beta { get; set; }
        public decimal FiftyTwoWeekHigh { get; set; }
        public decimal FiftyTwoWeekLow { get; set; }
        public decimal FiftyDayMovingAverage { get; set; }
        public decimal TwoHundredDayMovingAverage { get; set; }
        public long SharesOutstanding { get; set; }
        public long SharesFloat { get; set; }
        public long SharesShort { get; set; }
        public long SharesShortPriorMonth { get; set; }
        public decimal ShortRatio { get; set; }
        public decimal ShortPercentOutstanding { get; set; }
        public decimal ShortPercentFloat { get; set; }
        public decimal PercentInsiders { get; set; }
        public decimal PercentInstitutions { get; set; }
        public decimal ForwardAnnualDividendRate { get; set; }
        public decimal ForwardAnnualDividendYield { get; set; }
        public decimal PayoutRatio { get; set; }
        public DateTime DividendDate { get; set; }
        public DateTime ExDividendDate { get; set; }
        public string LastSplitFactor { get; set; } = String.Empty;
        public DateTime LastSplitDate { get; set; }
    }
}
