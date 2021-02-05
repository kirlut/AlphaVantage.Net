using AlphaVantage.Net.Common.Earnings;
using AlphaVantage.Net.Common.Parsing;
using AlphaVantage.Net.Fundamentals.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaVantage.Net.Fundamentals.Parsing
{
    internal sealed class EarningsParser : EarningsParserBase<Earnings, QuarterlyEarnings, AnnualEarnings>
    {
        public const string EMPTY_VALUE = "None";
        public override AnnualEarnings CreateAnnualEarningsInstance()
        {
            return new AnnualEarnings();
        }

        public override Earnings CreateEarningsInstance()
        {
            return new Earnings();
        }

        public override QuarterlyEarnings CreateQuarterlyEarningsInstance()
        {
            return new QuarterlyEarnings();
        }

        protected override Action<AnnualEarnings, string>? GetAnnualParsingDelegate(string fieldName)
        {
            return AnnualParsingDelegates.ContainsKey(fieldName) ? AnnualParsingDelegates[fieldName] : null;
        }



        protected override Action<QuarterlyEarnings, string>? GetQuarterlyParsingDelegate(string fieldName)
        {
            return QuarterlyParsingDelegates.ContainsKey(fieldName) ? QuarterlyParsingDelegates[fieldName] : null;
        }


        private static readonly Dictionary<string, Action<QuarterlyEarnings, string>> QuarterlyParsingDelegates =
            new Dictionary<string, Action<QuarterlyEarnings, string>>()
            {
                {"fiscalDateEnding", (dataPoint, strValue) => { dataPoint.FiscalDateEnding = strValue.ParseToDateTime(); }},
                {"reportedDate", (dataPoint, strValue) => { dataPoint.ReportedDate = strValue.ParseToDateTime(); }},
                {"reportedEPS", (dataPoint, strValue) => { dataPoint.ReportedEPS = strValue.ParseToDecimal(); }},
                {"estimatedEPS", (dataPoint, strValue) => {
                    if(strValue == EMPTY_VALUE )
                        dataPoint.EstimatedEPS = 0;
                    else
                    dataPoint.EstimatedEPS = strValue.ParseToDecimal();
                }},
                {"surprise", (dataPoint, strValue) => {
                    if(strValue == EMPTY_VALUE )
                        dataPoint.Surprise = 0;
                    else
                    dataPoint.Surprise = strValue.ParseToDecimal();
                }},
                {"surprisePercentage", (dataPoint, strValue) => {
                    if(strValue == EMPTY_VALUE )
                        dataPoint.SurprisePercentage = 0;
                    else
                    dataPoint.SurprisePercentage = strValue.ParseToDecimal(); 
                }},

            };

        private static readonly Dictionary<string, Action<AnnualEarnings, string>> AnnualParsingDelegates =
           new Dictionary<string, Action<AnnualEarnings, string>>()
           {
                {"fiscalDateEnding", (dataPoint, strValue) => { dataPoint.FiscalDateEnding = strValue.ParseToDateTime(); }},
                {"reportedEPS", (dataPoint, strValue) => { dataPoint.ReportedEPS = strValue.ParseToDecimal(); }}
           };
    }
}
