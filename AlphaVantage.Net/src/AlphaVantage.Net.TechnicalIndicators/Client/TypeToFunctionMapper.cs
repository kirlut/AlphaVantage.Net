using System;
using AlphaVantage.Net.Common;

namespace AlphaVantage.Net.TechnicalIndicators.Client
{
    internal static class TypeToFunctionMapper
    {
        public static ApiFunction ToApiFunction(this TechIndicatorType techIndicatorType)
        {
            return (techIndicatorType) switch
            {
                TechIndicatorType.SMA => ApiFunction.SMA,
                TechIndicatorType.EMA => ApiFunction.EMA,
                TechIndicatorType.WMA => ApiFunction.WMA,
                TechIndicatorType.DEMA => ApiFunction.DEMA,
                TechIndicatorType.TEMA => ApiFunction.TEMA,
                TechIndicatorType.TRIMA => ApiFunction.TRIMA,
                TechIndicatorType.KAMA => ApiFunction.KAMA,
                TechIndicatorType.MAMA => ApiFunction.MAMA,
                TechIndicatorType.VWAP => ApiFunction.VWAP,
                TechIndicatorType.T3 => ApiFunction.T3,
                TechIndicatorType.MACD => ApiFunction.MACD,
                TechIndicatorType.MACDEXT => ApiFunction.MACDEXT,
                TechIndicatorType.STOCH => ApiFunction.STOCH,
                TechIndicatorType.STOCHF => ApiFunction.STOCHF,
                TechIndicatorType.RSI => ApiFunction.RSI,
                TechIndicatorType.STOCHRSI => ApiFunction.STOCHRSI,
                TechIndicatorType.WILLR => ApiFunction.WILLR,
                TechIndicatorType.ADX => ApiFunction.ADX,
                TechIndicatorType.ADXR => ApiFunction.ADXR,
                TechIndicatorType.APO => ApiFunction.APO,
                TechIndicatorType.PPO => ApiFunction.PPO,
                TechIndicatorType.MOM => ApiFunction.MOM,
                TechIndicatorType.BOP => ApiFunction.BOP,
                TechIndicatorType.CCI => ApiFunction.CCI,
                TechIndicatorType.CMO => ApiFunction.CMO,
                TechIndicatorType.ROC => ApiFunction.ROC,
                TechIndicatorType.ROCR => ApiFunction.ROCR,
                TechIndicatorType.AROON => ApiFunction.AROON,
                TechIndicatorType.AROONOSC => ApiFunction.AROONOSC,
                TechIndicatorType.MFI => ApiFunction.MFI,
                TechIndicatorType.TRIX => ApiFunction.TRIX,
                TechIndicatorType.ULTOSC => ApiFunction.ULTOSC,
                TechIndicatorType.DX => ApiFunction.DX,
                TechIndicatorType.MINUS_DI => ApiFunction.MINUS_DI,
                TechIndicatorType.PLUS_DI => ApiFunction.PLUS_DI,
                TechIndicatorType.MINUS_DM => ApiFunction.MINUS_DM,
                TechIndicatorType.PLUS_DM => ApiFunction.PLUS_DM,
                TechIndicatorType.BBANDS => ApiFunction.BBANDS,
                TechIndicatorType.MIDPOINT => ApiFunction.MIDPOINT,
                TechIndicatorType.MIDPRICE => ApiFunction.MIDPRICE,
                TechIndicatorType.SAR => ApiFunction.SAR,
                TechIndicatorType.TRANGE => ApiFunction.TRANGE,
                TechIndicatorType.ATR => ApiFunction.ATR,
                TechIndicatorType.NATR => ApiFunction.NATR,
                TechIndicatorType.AD => ApiFunction.AD,
                TechIndicatorType.ADOSC => ApiFunction.ADOSC,
                TechIndicatorType.OBV => ApiFunction.OBV,
                TechIndicatorType.HT_TRENDLINE => ApiFunction.HT_TRENDLINE,
                TechIndicatorType.HT_SINE => ApiFunction.HT_SINE,
                TechIndicatorType.HT_TRENDMODE => ApiFunction.HT_TRENDMODE,
                TechIndicatorType.HT_DCPERIOD => ApiFunction.HT_DCPERIOD,
                TechIndicatorType.HT_DCPHASE => ApiFunction.HT_DCPHASE,
                TechIndicatorType.HT_PHASOR => ApiFunction.HT_PHASOR,

                _ => throw new ArgumentOutOfRangeException(nameof(techIndicatorType))
            };
        }
    }
}