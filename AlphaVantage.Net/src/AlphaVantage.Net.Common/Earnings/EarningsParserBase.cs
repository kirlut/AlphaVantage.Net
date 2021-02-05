using AlphaVantage.Net.Common.Parsing;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace AlphaVantage.Net.Common.Earnings
{
    public abstract class EarningsParserBase<TEarnings, TQuarterlyEarnings, TAnnualEarnings> : IAlphaVantageJsonDocumentParser<TEarnings>
        where TQuarterlyEarnings : EarningsPeriodBase
        where TAnnualEarnings : EarningsPeriodBase
        where TEarnings : EarningsBase<TQuarterlyEarnings, TAnnualEarnings>

    {

        public abstract TEarnings CreateEarningsInstance();

        public abstract TQuarterlyEarnings CreateQuarterlyEarningsInstance();

        public abstract TAnnualEarnings CreateAnnualEarningsInstance();

        protected abstract Action<TAnnualEarnings, string>? GetAnnualParsingDelegate(string fieldName);
        protected abstract Action<TQuarterlyEarnings, string>? GetQuarterlyParsingDelegate(string fieldName);
        public TEarnings ParseApiResponse(JsonDocument jsonDocument)
        {
            var result = CreateEarningsInstance();

            try
            {
                result.Symbol = jsonDocument.RootElement.GetProperty("symbol").GetString();
                result.AnnualEarnings = GetAnnualEarningsData(jsonDocument);
                result.QuarterlyEarnings = GetQuarterlyEarningsData(jsonDocument);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return result;
        }

        private List<TAnnualEarnings> GetAnnualEarningsData(JsonDocument jsonDocument)
        {
            var result = new List<TAnnualEarnings>();
            var annualEarningsJson = jsonDocument.RootElement.GetProperty("annualEarnings");

            foreach (var annualEarningsDataPoint in annualEarningsJson.EnumerateArray())
            {
                var annualDataPoint = CreateAnnualEarningsInstance();
                EnrichAnnualEarningsDataPoint(annualDataPoint, annualEarningsDataPoint);
                result.Add(annualDataPoint);
            }

            return result;
        }
        private void EnrichAnnualEarningsDataPoint(TAnnualEarnings annualEarningDataPoint, JsonElement annualEarningDataPointFields)
        {
            foreach (var fieldJson in annualEarningDataPointFields.EnumerateObject())
            {
                GetAnnualParsingDelegate(fieldJson.Name)?.Invoke(annualEarningDataPoint, fieldJson.Value.GetString());
            }
        }

        private List<TQuarterlyEarnings> GetQuarterlyEarningsData(JsonDocument jsonDocument)
        {
            var result = new List<TQuarterlyEarnings>();
            var quarterlyEarningsJson = jsonDocument.RootElement.GetProperty("quarterlyEarnings");

            foreach (var quarterlyEarningsDataPoint in quarterlyEarningsJson.EnumerateArray())
            {

                System.Diagnostics.Debug.WriteLine(quarterlyEarningsDataPoint.ToString());
                var quarterlyDataPoint = CreateQuarterlyEarningsInstance();
                EnrichQuarterlyEarningsDataPoint(quarterlyDataPoint, quarterlyEarningsDataPoint);
                result.Add(quarterlyDataPoint);
            }

            return result;
        }
        private void EnrichQuarterlyEarningsDataPoint(TQuarterlyEarnings quarterlyEarningDataPoint, JsonElement quarterlyEarningDataPointFields)
        {
            foreach (var fieldJson in quarterlyEarningDataPointFields.EnumerateObject())
            {
                GetQuarterlyParsingDelegate(fieldJson.Name)?.Invoke(quarterlyEarningDataPoint, fieldJson.Value.GetString());
            }
        }
    }
}
