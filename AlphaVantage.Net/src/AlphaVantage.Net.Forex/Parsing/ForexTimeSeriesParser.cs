using System;
using System.Collections.Generic;
using AlphaVantage.Net.Common.Currencies;
using AlphaVantage.Net.Common.Intervals;
using AlphaVantage.Net.Common.Parsing;
using AlphaVantage.Net.Common.TimeSeries;

namespace AlphaVantage.Net.Forex.Parsing
{
    internal sealed class ForexTimeSeriesParser : TimeSeriesParserBase<ForexTimeSeries, ForexDataPoint>
    {
        private readonly PhysicalCurrency _fromSymbol;
        private readonly PhysicalCurrency _toSymbol;
        
        public ForexTimeSeriesParser(
            Interval timeSeriesInterval, 
            PhysicalCurrency fromSymbol, 
            PhysicalCurrency toSymbol) : base(timeSeriesInterval)
        {
            _fromSymbol = fromSymbol;
            _toSymbol = toSymbol;
        }

        protected override ForexTimeSeries CreateTimeSeriesInstance()
        {
            return new ForexTimeSeries()
            {
                FromSymbol = _fromSymbol,
                ToSymbol = _toSymbol
            };
        }

        protected override ForexDataPoint CreateDataPointInstance()
        {
            return new ForexDataPoint();
        }

        protected override Action<ForexDataPoint, string>? GetParsingDelegate(string fieldName)
        {
            return ParsingDelegates.ContainsKey(fieldName) ? ParsingDelegates[fieldName] : null;
        }
        
        // ReSharper disable once InconsistentNaming
        private static readonly Dictionary<string, Action<ForexDataPoint, string>> ParsingDelegates =
            new Dictionary<string, Action<ForexDataPoint, string>>()
            {
                {"open", (dataPoint, strValue) => { dataPoint.OpeningPrice = strValue.ParseToDecimal(); }},
                {"high", (dataPoint, strValue) => { dataPoint.HighestPrice = strValue.ParseToDecimal(); }},
                {"low", (dataPoint, strValue) => { dataPoint.LowestPrice = strValue.ParseToDecimal(); }},
                {"close", (dataPoint, strValue) => { dataPoint.ClosingPrice = strValue.ParseToDecimal(); }}
            };
    }
}