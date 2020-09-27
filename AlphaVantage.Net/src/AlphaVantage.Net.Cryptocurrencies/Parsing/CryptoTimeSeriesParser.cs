using System;
using System.Collections.Generic;
using AlphaVantage.Net.Common.Parsing;
using AlphaVantage.Net.Common.Currencies;
using AlphaVantage.Net.Common.Intervals;
using AlphaVantage.Net.Common.TimeSeries;

namespace AlphaVantage.Net.Cryptocurrencies.Parsing
{
    internal sealed class CryptoTimeSeriesParser : TimeSeriesParserBase<CryptoTimeSeries, CryptoDataPoint>
    {
        private readonly DigitalCurrency _fromSymbol;
        private readonly PhysicalCurrency _toSymbol;
        private readonly Dictionary<string, Action<CryptoDataPoint, string>> _parsingDelegates;
        
        public CryptoTimeSeriesParser(
            Interval timeSeriesInterval, 
            DigitalCurrency fromSymbol, 
            PhysicalCurrency toSymbol) : base(timeSeriesInterval)
        {
            _fromSymbol = fromSymbol;
            _toSymbol = toSymbol;

            _parsingDelegates = CreateParsingDelegates(toSymbol);
        }

        protected override CryptoTimeSeries CreateTimeSeriesInstance()
        {
            return new CryptoTimeSeries()
            {
                FromSymbol = _fromSymbol,
                ToSymbol = _toSymbol
            };
        }

        protected override CryptoDataPoint CreateDataPointInstance()
        {
            return new CryptoDataPoint();
        }

        protected override Action<CryptoDataPoint, string>? GetParsingDelegate(string fieldName)
        {
            return _parsingDelegates.ContainsKey(fieldName) ? _parsingDelegates[fieldName] : null;
        }

        private static Dictionary<string, Action<CryptoDataPoint, string>> CreateParsingDelegates(PhysicalCurrency currency)
        {
            var result = new Dictionary<string, Action<CryptoDataPoint, string>>(ConstantParsingDelegates)
            {
                {
                    $"open ({currency.ToString()})",
                    (dataPoint, strValue) => { dataPoint.OpeningPrice = strValue.ParseToDecimal(); }
                },
                {
                    $"high ({currency.ToString()})",
                    (dataPoint, strValue) => { dataPoint.HighestPrice = strValue.ParseToDecimal(); }
                },
                {
                    $"low ({currency.ToString()})",
                    (dataPoint, strValue) => { dataPoint.LowestPrice = strValue.ParseToDecimal(); }
                },
                {
                    $"close ({currency.ToString()})",
                    (dataPoint, strValue) => { dataPoint.ClosingPrice = strValue.ParseToDecimal(); }
                }
            };


            return result;
        }
        
        private static readonly Dictionary<string, Action<CryptoDataPoint, string>> ConstantParsingDelegates =
            new Dictionary<string, Action<CryptoDataPoint, string>>()
            {
                {"open (USD)", (dataPoint, strValue) => { dataPoint.OpeningPriceUSD = strValue.ParseToDecimal(); }},
                {"high (USD)", (dataPoint, strValue) => { dataPoint.HighestPriceUSD = strValue.ParseToDecimal(); }},
                {"low (USD)", (dataPoint, strValue) => { dataPoint.LowestPriceUSD = strValue.ParseToDecimal(); }},
                {"close (USD)", (dataPoint, strValue) => { dataPoint.ClosingPriceUSD = strValue.ParseToDecimal(); }},
                {"volume", (dataPoint, strValue) => { dataPoint.Volume = strValue.ParseToLong(); }},
                {"market cap (USD)", (dataPoint, strValue) => { dataPoint.MarketCapitalization = strValue.ParseToLong(); }}
            };
    }
}