![GitHub](https://img.shields.io/github/license/LutsenkoKirill/AlphaVantage.Net)

# AlphaVantage.Net
The most popular .Net client library for [**Alpha Vantage API**](https://www.alphavantage.co/).  

# Release notes for version 2:
- Most of the library classes were rewritten from scratch, keeping in mind all issues that were opened for the previous release. 
- New client works with `System.Text.Json` under the hood which is faster than classic `Newtonsoft Json` 
- Now you can create client's instances with 6 different constructors. It  gives you access to underlying `HttpClient` + allow you to create wrappers around it if needed.  
- All packages were written using newest C# `Nullable reference types` feature, to reduce possible bugs

# Packages: 
- [**AlphaVantage.Net.Core**](#alphavantagenetcore) - low-level client for Alpha Vantage API based on `HttpClient` and `System.Text.Json`
### Fully typed clients:
- [**AlphaVantage.Net.Stocks**](#alphavantagenetstocks) - [stock time series](https://www.alphavantage.co/documentation/#time-series-data).
- [**AlphaVantage.Net.Forex**](#alphavantagenetforex) - [Forex data](https://www.alphavantage.co/documentation/#fx)
- [**AlphaVantage.Net.Crypto**](#alphavantagenetcrypto) - [cryptocurrencies data](https://www.alphavantage.co/documentation/#digital-currency)
- [**AlphaVantage.Net.TechnicalIndicators**](#alphavantagenettechnicalindicators) - [technical indicators time series](https://www.alphavantage.co/documentation/#technical-indicators).

# Documentation

## AlphaVantage.Net.Core
![Nuget (with prereleases)](https://img.shields.io/nuget/vpre/AlphaVantage.Net.Core)
![Nuget](https://img.shields.io/nuget/dt/AlphaVantage.Net.Core)  
This package allow you to request any available data from API, but you have to manually set all query parameters and retrieve information you need from the result

### Installation: 
- Package Manager:  
`Install-Package AlphaVantage.Net.Core -Version 2.0.1`  
- .NET CLI:  
`dotnet add package AlphaVantage.Net.Core --version 2.0.1`  

### Usage: 
```csharp
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using AlphaVantage.Net.Common;
using AlphaVantage.Net.Core.Client;

.....

public static async Task CoreDemo()
{
    // use your AlphaVantage API key
    string apiKey = "1";
    // there's 5 more constructors available
    using var client = new AlphaVantageClient(apiKey);

    // query for intraday time series for Apple Inc:
    var query = new Dictionary<string, string>()
    {
        {"symbol", "AAPL"},
        {"interval", "15min"}
    };
    
    // retrieve response as pure JSON string
    string stringResult = await client.RequestPureJsonAsync(ApiFunction.TIME_SERIES_INTRADAY, query);

    // retrieve response as parsed JsonDocument from System.Text.Json
    JsonDocument parsedResult = await client.RequestParsedJsonAsync(ApiFunction.TIME_SERIES_INTRADAY, query);
}
```

## AlphaVantage.Net.Stocks
![Nuget (with prereleases)](https://img.shields.io/nuget/vpre/AlphaVantage.Net.Stocks)
![Nuget](https://img.shields.io/nuget/dt/AlphaVantage.Net.Stocks)   

### Installation: 
- Package Manager:  
`Install-Package AlphaVantage.Net.Stocks -Version 2.0.1`  
- .NET CLI:  
`dotnet add package AlphaVantage.Net.Stocks --version 2.0.1`  

### Usage: 
```csharp
using System.Collections.Generic;
using System.Threading.Tasks;
using AlphaVantage.Net.Common.Intervals;
using AlphaVantage.Net.Common.Size;
using AlphaVantage.Net.Core.Client;
using AlphaVantage.Net.Stocks;
using AlphaVantage.Net.Stocks.Client;

.....

public static async Task StocksDemo()
{
    // use your AlphaVantage API key
    string apiKey = "1";
    // there are 5 more constructors available
    using var client = new AlphaVantageClient(apiKey);
    using var stocksClient = client.Stocks();

    StockTimeSeries stockTs = await stocksClient.GetTimeSeriesAsync("AAPL", Interval.Daily, OutputSize.Compact, isAdjusted: true);

    GlobalQuote globalQuote = await stocksClient.GetGlobalQuoteAsync("AAPL");

    ICollection<SymbolSearchMatch> searchMatches = await stocksClient.SearchSymbolAsync("BA");
}
```

## AlphaVantage.Net.Forex
![Nuget (with prereleases)](https://img.shields.io/nuget/vpre/AlphaVantage.Net.Forex)
![Nuget](https://img.shields.io/nuget/dt/AlphaVantage.Net.Forex)  

### Installation: 
- Package Manager:  
`Install-Package AlphaVantage.Net.Forex -Version 2.0.1`  
- .NET CLI:  
`dotnet add package AlphaVantage.Net.Forex --version 2.0.1`  

### Usage: 
```csharp
using System.Threading.Tasks;
using AlphaVantage.Net.Common.Currencies;
using AlphaVantage.Net.Common.Intervals;
using AlphaVantage.Net.Common.Size;
using AlphaVantage.Net.Core.Client;
using AlphaVantage.Net.Forex;
using AlphaVantage.Net.Forex.Client;

.....

public static async Task ForexDemo()
{
    // use your AlphaVantage API key
    string apiKey = "1";
    // there are 5 more constructors available
    using var client = new AlphaVantageClient(apiKey);
    using var forexClient = client.Forex();

    ForexTimeSeries forexTimeSeries = await forexClient.GetTimeSeriesAsync(
        PhysicalCurrency.USD, 
        PhysicalCurrency.ILS,
        Interval.Daily, 
        OutputSize.Compact);
            
    ForexExchangeRate forexExchangeRate = await forexClient.GetExchangeRateAsync(PhysicalCurrency.USD, PhysicalCurrency.ILS);
}
```

## AlphaVantage.Net.Crypto
![Nuget (with prereleases)](https://img.shields.io/nuget/vpre/AlphaVantage.Net.Crypto)
![Nuget](https://img.shields.io/nuget/dt/AlphaVantage.Net.Crypto)  

### Installation: 
- Package Manager:  
`Install-Package AlphaVantage.Net.Crypto -Version 2.0.1`  
- .NET CLI:  
`dotnet add package AlphaVantage.Net.Crypto --version 2.0.1`  

### Usage: 
```csharp
using System.Threading.Tasks;
using AlphaVantage.Net.Common.Currencies;
using AlphaVantage.Net.Common.Intervals;
using AlphaVantage.Net.Core.Client;
using AlphaVantage.Net.Crypto;
using AlphaVantage.Net.Crypto.Client;

.....

public static async Task CryptoDemo()
{
    // use your AlphaVantage API key
    string apiKey = "1";
    // there are 5 more constructors available
    using var client = new AlphaVantageClient(apiKey);
    using var cryptoClient = client.Crypto();

    CryptoTimeSeries cryptoTimeSeries =
        await cryptoClient.GetTimeSeriesAsync(DigitalCurrency.BTC, PhysicalCurrency.ILS, Interval.Weekly);

    CryptoRating cryptoRating = await cryptoClient.GetCryptoRatingAsync(DigitalCurrency.BTC);

    CryptoExchangeRate exchangeRate =
        await cryptoClient.GetExchangeRateAsync(DigitalCurrency.BTC, PhysicalCurrency.ILS);
}
```

## AlphaVantage.Net.TechnicalIndicators
![Nuget (with prereleases)](https://img.shields.io/nuget/vpre/AlphaVantage.Net.TechnicalIndicators)
![Nuget](https://img.shields.io/nuget/dt/AlphaVantage.Net.TechnicalIndicators)  
Since API endpoints from this section have many different additional parameters, you still need to check Alpha Vantage documentation in order to use it. 

### Installation: 
- Package Manager:  
`Install-Package AlphaVantage.Net.TechnicalIndicators -Version 2.0.1`  
- .NET CLI:  
`dotnet add package AlphaVantage.Net.TechnicalIndicators --version 2.0.1`  

### Usage: 
```csharp
using System.Collections.Generic;
using System.Threading.Tasks;
using AlphaVantage.Net.Common.Intervals;
using AlphaVantage.Net.Core.Client;
using AlphaVantage.Net.TechnicalIndicators;
using AlphaVantage.Net.TechnicalIndicators.Client;

.....

public static async Task TechIndicatorsDemo()
{
    // use your AlphaVantage API key
    string apiKey = "1";
    // there are 5 more constructors available
    using var client = new AlphaVantageClient(apiKey);

    var symbol = "IBM";
    var indicatorType = TechIndicatorType.SMA;
    var query = new Dictionary<string, string>()
    {
        {"time_period", "20"},
        {"series_type", "close"}
    };

    TechIndicatorTimeSeries result = await client.GetTechIndicatorTimeSeriesAsync(symbol, indicatorType, Interval.Min15, query);
}
```
