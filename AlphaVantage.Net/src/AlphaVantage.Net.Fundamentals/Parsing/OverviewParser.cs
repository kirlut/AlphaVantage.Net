using AlphaVantage.Net.Common.Currencies;
using AlphaVantage.Net.Common.Overview;
using AlphaVantage.Net.Common.Parsing;
using AlphaVantage.Net.Fundamentals.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaVantage.Net.Fundamentals.Parsing
{
    internal sealed class OverviewParser : OverviewParserBase<CompanyOverview>
    {
        public override CompanyOverview CreateOverviewInstance()
        {
            return new CompanyOverview();
        }

        public override Action<CompanyOverview, string>? GetOverviewParsingDelegate(string fieldName)
        {
            return OverviewParsingDelegates.ContainsKey(fieldName) ? OverviewParsingDelegates[fieldName] : null;
        }


        public static readonly Dictionary<string, Action<CompanyOverview, string>> OverviewParsingDelegates =
            new Dictionary<string, Action<CompanyOverview, string>>()
            {
                {"Symbol", (dataPoint, strValue) => { dataPoint.Symbol = strValue; }},
                {"AssetType", (dataPoint, strValue) => { dataPoint.AssetType = strValue; }},
                {"Name", (dataPoint, strValue) => { dataPoint.Name = strValue; }},
                {"Description", (dataPoint, strValue) => { dataPoint.Description = strValue; }},
                {"Exchange", (dataPoint, strValue) => { dataPoint.Exchange = strValue; }},
                {"Currency", (dataPoint, strValue) => { dataPoint.PhysicalCurrency = strValue.ParseToCurrency(); }},
                {"Country", (dataPoint, strValue) => { dataPoint.Country = strValue; }},
                {"Sector", (dataPoint, strValue) => { dataPoint.Sector = strValue; }},
                {"Industry", (dataPoint, strValue) => { dataPoint.Industry = strValue; }},
                {"Address", (dataPoint, strValue) => { dataPoint.Address = strValue; }},
                {"FullTimeEmployees", (dataPoint, strValue) => { dataPoint.FullTimeEmployees = strValue.ParseToInt(); }},
                {"FiscalYearEnd", (dataPoint, strValue) => { dataPoint.FiscalYearEnd = strValue; }},
                {"LatestQuarter", (dataPoint, strValue) => { dataPoint.LatestQuarter = strValue.ParseToDateTime(); }},
                {"MarketCapitalization", (dataPoint, strValue) => { dataPoint.MarketCapitalization = strValue.ParseToLong(); }},
                {"EBITDA", (dataPoint, strValue) => { dataPoint.EBITDA = strValue.ParseToLong(); }},
                {"PERatio", (dataPoint, strValue) => { dataPoint.PERatio = strValue.ParseToDecimal(); }},
                {"PEGRatio", (dataPoint, strValue) => { dataPoint.PEGRatio = strValue.ParseToDecimal(); }},
                {"BookValue", (dataPoint, strValue) => { dataPoint.BookValue = strValue.ParseToDecimal(); }},
                {"DividendPerShare", (dataPoint, strValue) => { dataPoint.DividendPerShare = strValue.ParseToDecimal(); }},
                {"DividendYield", (dataPoint, strValue) => { dataPoint.DividendYield = strValue.ParseToDecimal(); }},
                {"EPS", (dataPoint, strValue) => { dataPoint.EPS = strValue.ParseToDecimal(); }},
                {"RevenuePerShareTTM", (dataPoint, strValue) => { dataPoint.RevenuePerShareTTM = strValue.ParseToDecimal(); }},
                {"ProfitMargin", (dataPoint, strValue) => { dataPoint.ProfitMargin = strValue.ParseToDecimal(); }},
                {"OperatingMarginTTM", (dataPoint, strValue) => { dataPoint.OperatingMarginTTM = strValue.ParseToDecimal(); }},
                {"ReturnOnAssetsTTM", (dataPoint, strValue) => { dataPoint.ReturnOnAssetsTTM = strValue.ParseToDecimal(); }},
                {"ReturnOnEquityTTM", (dataPoint, strValue) => { dataPoint.ReturnOnEquityTTM = strValue.ParseToDecimal(); }},
                {"RevenueTTM", (dataPoint, strValue) => { dataPoint.RevenueTTM = strValue.ParseToLong(); }},
                {"GrossProfitTTM", (dataPoint, strValue) => { dataPoint.GrossProfitTTM = strValue.ParseToLong(); }},
                {"DilutedEPSTTM", (dataPoint, strValue) => { dataPoint.DilutedEPSTTM = strValue.ParseToDecimal(); }},
                {"QuarterlyEarningsGrowthYOY", (dataPoint, strValue) => { dataPoint.QuarterlyEarningsGrowthYOY = strValue.ParseToDecimal(); }},
                {"QuarterlyRevenueGrowthYOY", (dataPoint, strValue) => { dataPoint.QuarterlyRevenueGrowthYOY = strValue.ParseToDecimal(); }},
                {"AnalystTargetPrice", (dataPoint, strValue) => { dataPoint.AnalystTargetPrice = strValue.ParseToDecimal(); }},
                {"TrailingPE", (dataPoint, strValue) => { dataPoint.TrailingPE = strValue.ParseToDecimal(); }},
                {"ForwardPE", (dataPoint, strValue) => { dataPoint.ForwardPE = strValue.ParseToDecimal(); }},
                {"PriceToSalesRatioTTM", (dataPoint, strValue) => { dataPoint.PriceToSalesRatioTTM = strValue.ParseToDecimal(); }},
                {"PriceToBookRatio", (dataPoint, strValue) => { dataPoint.PriceToBookRatio = strValue.ParseToDecimal(); }},
                {"EVToRevenue", (dataPoint, strValue) => { dataPoint.EVToRevenue = strValue.ParseToDecimal(); }},
                {"EVToEBITDA", (dataPoint, strValue) => { dataPoint.EVToEBITDA = strValue.ParseToDecimal(); }},
                {"Beta", (dataPoint, strValue) => { dataPoint.Beta = strValue.ParseToDecimal(); }},
                {"52WeekHigh", (dataPoint, strValue) => { dataPoint.FiftyTwoWeekHigh = strValue.ParseToDecimal(); }},
                {"52WeekLow", (dataPoint, strValue) => { dataPoint.FiftyTwoWeekLow = strValue.ParseToDecimal(); }},
                {"50DayMovingAverage", (dataPoint, strValue) => { dataPoint.FiftyDayMovingAverage = strValue.ParseToDecimal(); }},
                {"200DayMovingAverage", (dataPoint, strValue) => { dataPoint.TwoHundredDayMovingAverage = strValue.ParseToDecimal(); }},
                {"SharesOutstanding", (dataPoint, strValue) => { dataPoint.SharesOutstanding = strValue.ParseToLong(); }},
                {"SharesFloat", (dataPoint, strValue) => { dataPoint.SharesFloat = strValue.ParseToLong(); }},
                {"SharesShort", (dataPoint, strValue) => { dataPoint.SharesShort = strValue.ParseToLong(); }},
                {"SharesShortPriorMonth", (dataPoint, strValue) => { dataPoint.SharesShortPriorMonth = strValue.ParseToLong(); }},
                {"ShortRatio", (dataPoint, strValue) => { dataPoint.ShortRatio = strValue.ParseToDecimal(); }},
                {"ShortPercentOutstanding", (dataPoint, strValue) => { dataPoint.ShortPercentOutstanding = strValue.ParseToDecimal(); }},
                {"ShortPercentFloat", (dataPoint, strValue) => { dataPoint.ShortPercentFloat = strValue.ParseToDecimal(); }},
                {"PercentInsiders", (dataPoint, strValue) => { dataPoint.PercentInsiders = strValue.ParseToDecimal(); }},
                {"PercentInstitutions", (dataPoint, strValue) => { dataPoint.PercentInstitutions = strValue.ParseToDecimal(); }},
                {"ForwardAnnualDividendRate", (dataPoint, strValue) => { dataPoint.ForwardAnnualDividendRate = strValue.ParseToDecimal(); }},
                {"ForwardAnnualDividendYield", (dataPoint, strValue) => { dataPoint.ForwardAnnualDividendYield = strValue.ParseToDecimal(); }},
                {"PayoutRatio", (dataPoint, strValue) => { dataPoint.PayoutRatio = strValue.ParseToDecimal(); }},
                {"DividendDate", (dataPoint, strValue) => { dataPoint.DividendDate = strValue.ParseToDateTime(); }},
                {"ExDividendDate", (dataPoint, strValue) => { dataPoint.ExDividendDate = strValue.ParseToDateTime(); }},
                {"LastSplitFactor", (dataPoint, strValue) => { dataPoint.LastSplitFactor = strValue; }},
                {"LastSplitDate", (dataPoint, strValue) => { dataPoint.LastSplitDate = strValue.ParseToDateTime(); }}


            };
    }
}
