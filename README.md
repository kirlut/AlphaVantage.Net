![GitHub](https://img.shields.io/github/license/LutsenkoKirill/AlphaVantage.Net)

# AlphaVantage.Net
The most popular .Net client library for [**Alpha Vantage API**](https://www.alphavantage.co/).  

# Release notes for version 2:
- Most of the library classes were rewritten from scratch, keeping in mind all issues that were opened for the previous release. 
- New client works with `System.Text.Json` under the hood which is faster than classic `Newtonsoft Json` 
- Now you can create client's instances with 6 different constructors. It  gives you access to underlying `HttpClient` + allow you to create wrappers around it if needed.  
- All packages were written using newest C# `Nullable reference types` feature, to reduce possible bugs

# Packages: 
- [AlphaVantage.Net.Core](AlphaVantage.Net/src/AlphaVantage.Net.Core) - low-level client for Alpha Vantage API based on `HttpClient` and `System.Text.Json`
### Extensions for the core client
This packages contain high-level extensions and POCO classes that simplify work with corresponding API parts and do all parsing for you:
- [AlphaVantage.Net.Stocks](AlphaVantage.Net/src/AlphaVantage.Net.Stocks) - for [stock time series data](https://www.alphavantage.co/documentation/#time-series-data)
- [AlphaVantage.Net.TechnicalIndicators](AlphaVantage.Net/src/AlphaVantage.Net.TechnicalIndicators) - for [technical indicators data](https://www.alphavantage.co/documentation/#technical-indicators).

# Documentation

## AlphaVantage.Net.Core
![Nuget (with prereleases)](https://img.shields.io/nuget/vpre/AlphaVantage.Net.Core)
![Nuget](https://img.shields.io/nuget/dt/AlphaVantage.Net.Core)  
This package allow you to request any available data from API, but you have to manually set all query parameters and retrieve information you need from the result

### Installation: 
- Package Manager:  
`Install-Package AlphaVantage.Net.Core -Version 2.0.0-preview-2`  
- .NET CLI:  
`dotnet add package AlphaVantage.Net.Core --version 2.0.0-preview-2`  

### Usage: 
```csharp
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using AlphaVantage.Net.Core;
using AlphaVantage.Net.Core.Client;

...

public static async Task AlphaVantageCoreDemo()
{
    // use your AlphaVantage API key
    string apiKey = "1";
    // there's 5 more constructors allowed
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
This package provide additional abstraction layer under `AlphaVantage.Net.Core` and allow you to retrieve time series data without dealing with parsing. 

### Installation: 
- Package Manager:  
`Install-Package AlphaVantage.Net.Stocks -Version 2.0.0-preview-2`  
- .NET CLI:  
`dotnet add package AlphaVantage.Net.Stocks --version 2.0.0-preview-2`  

### Usage: 
```csharp
using System.Collections.Generic;
using System.Threading.Tasks;
using AlphaVantage.Net.Core.Client;
using AlphaVantage.Net.Core.Intervals;
using AlphaVantage.Net.Stocks;
using AlphaVantage.Net.Stocks.Client;
using AlphaVantage.Net.Stocks.TimeSeries;

...

public async Task AlphaVantageStocksDemo()
{
    // use your AlphaVantage API key
    string apiKey = "1";
    // there's 5 more constructors allowed
    using var client = new AlphaVantageClient(apiKey);
    using var stocksClient = client.Stocks();

    StockTimeSeries stockTs = await stocksClient.GetTimeSeriesAsync("AAPL", Interval.Daily, TimeSeriesSize.Full, isAdjusted: true);

    GlobalQuote globalQuote = await stocksClient.GetGlobalQuoteAsync("AAPL");

    ICollection<SymbolSearchMatch> searchMatches = await stocksClient.SearchSymbolAsync("BA");
}
```

## AlphaVantage.Net.TechnicalIndicators
![Nuget (with prereleases)](https://img.shields.io/nuget/vpre/AlphaVantage.Net.TechnicalIndicators)
![Nuget](https://img.shields.io/nuget/dt/AlphaVantage.Net.TechnicalIndicators)  
This package extension method for the core client and allow you to retrieve technical indicators data without dealing with parsing. Since API functions from this section have many different additional parameters, you still need to check Alpha Vantage documentation in order to use it. 

### Installation: 
- Package Manager:  
`Install-Package AlphaVantage.Net.TechnicalIndicators -Version 2.0.0-preview-2`  
- .NET CLI:  
`dotnet add package AlphaVantage.Net.TechnicalIndicators --version 2.0.0-preview-2`  

### Usage: 
```csharp
using System.Collections.Generic;
using System.Threading.Tasks;
using AlphaVantage.Net.Core.Client;
using AlphaVantage.Net.Core.Intervals;
using AlphaVantage.Net.TechnicalIndicators;
using AlphaVantage.Net.TechnicalIndicators.Client;

...

public async Task AlphaVantageTechnicalIndicatorsDemo()
{
    // use your AlphaVantage API key
    string apiKey = "1";
    // there's 5 more constructors allowed
    using var client = new AlphaVantageClient(apiKey);

    var symbol = "IBM";
    var indicatorType = TechIndicatorType.SMA;
    var query = new Dictionary<string, string>()
    {
        {"time_period", "20"},
        {"series_type", "close"}
    };

    TechIndicatorResult result = await client.GetTechnicalIndicatorAsync(symbol, indicatorType, Interval.Min15, query);
}
```