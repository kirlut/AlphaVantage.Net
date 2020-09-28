// ReSharper disable InconsistentNaming
namespace AlphaVantage.Net.Common
{
    /// <summary>
    /// All possible functions of Alpha Vantage API
    /// </summary>
    /// <remarks>
    /// https://www.alphavantage.co/documentation
    /// </remarks>
    public enum ApiFunction
    {
        // Stock Time Series Data
        TIME_SERIES_INTRADAY,
        TIME_SERIES_INTRADAY_EXTENDED,
        TIME_SERIES_DAILY,
        TIME_SERIES_DAILY_ADJUSTED,
        TIME_SERIES_WEEKLY,
        TIME_SERIES_WEEKLY_ADJUSTED,
        TIME_SERIES_MONTHLY,
        TIME_SERIES_MONTHLY_ADJUSTED,
        GLOBAL_QUOTE,
        SYMBOL_SEARCH,
        
        // Fundamental Data
        OVERVIEW,
        INCOME_STATEMENT,
        BALANCE_SHEET,
        CASH_FLOW,
        LISTING_STATUS,
        
        // Forex (FX)
        CURRENCY_EXCHANGE_RATE,
        FX_INTRADAY,
        FX_DAILY,
        FX_WEEKLY,
        FX_MONTHLY,
        
        // Digital & Crypto Currencies
        CRYPTO_RATING,
        DIGITAL_CURRENCY_DAILY,
        DIGITAL_CURRENCY_WEEKLY,
        DIGITAL_CURRENCY_MONTHLY,
        
        // Stock Technical Indicators
        SMA,
        EMA,
        WMA,
        DEMA,
        TEMA,
        TRIMA,
        KAMA,
        MAMA,
        VWAP,
        T3,
        MACD,
        MACDEXT,
        STOCH,
        STOCHF,
        RSI,
        STOCHRSI,
        WILLR,
        ADX,
        ADXR,
        APO,
        PPO,
        MOM,
        BOP,
        CCI,
        CMO,
        ROC,
        ROCR,
        AROON,
        AROONOSC,
        MFI,
        TRIX,
        ULTOSC,
        DX,
        MINUS_DI,
        PLUS_DI,
        MINUS_DM,
        PLUS_DM,
        BBANDS,
        MIDPOINT,
        MIDPRICE,
        SAR,
        TRANGE,
        ATR,
        NATR,
        AD,
        ADOSC,
        OBV,
        HT_TRENDLINE,
        HT_SINE,
        HT_TRENDMODE,
        HT_DCPERIOD,
        HT_DCPHASE,
        HT_PHASOR,
    }
}