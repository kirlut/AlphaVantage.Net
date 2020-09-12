# AlphaVantage.Net version 2.0
.Net client library for [**Alpha Vantage API**](https://www.alphavantage.co/).  

# Release notes:
- Most of the library classes were rewritten from scratch, keeping in mind all issues that were opened for the previous version. 
- New client works with `System.Text.Json` under the hood which is faster than classic `Newtonsoft Json` 
- Now you can create client's instances with 6 different constructors. It  gives you access to underlying `HttpClient` + allow you to create wrappers around it if needed.  

Solution consist of following packages: 
- [AlphaVantage.Net.Core](AlphaVantage.Net/src/AlphaVantage.Net.Core) - low-level client for Alpha Vantage API based on `HttpClient` and `System.Text.Json`
- [AlphaVantage.Net.Stocks](AlphaVantage.Net/src/AlphaVantage.Net.Stocks) - high-level POCO classes and extensions for client from `AlphaVantage.Net.Core` that simplify retrieval of [stock time series data](https://www.alphavantage.co/documentation/#time-series-data) from Alpha Vantage API and perform parsing for you

## AlphaVantage.Net.Core
This package allow you to request any available data from API, but you have to manually set all query parameters and retrieve information you need from the result

### Installation: 
- Package Manager:  
`Install-Package AlphaVantage.Net.Core -Version 2.0.0-preview-1`  
- .NET CLI:  
`dotnet add package AlphaVantage.Net.Core --version 2.0.0-preview-1`  

### Usage: 
```csharp
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
This package provide additional abstraction layer under `AlphaVantage.Net.Core` and allow you to retrieve time series data without dealing with parsing. 

### Installation: 
- Package Manager:  
`Install-Package AlphaVantage.Net.Stocks -Version 2.0.0-preview-1`  
- .NET CLI:  
`dotnet add package AlphaVantage.Net.Stocks --version 2.0.0-preview-1`  

### Usage: 
```csharp
public async Task AlphaVantageStocksDemo()
{
    // use your AlphaVantage API key
    string apiKey = "1";
    // there's 5 more constructors allowed
    using var client = new AlphaVantageClient(apiKey);
    using var stocksClient = client.Stocks();

    StockTimeSeries stockTs = await stocksClient.GetTimeSeriesAsync("AAPL", TimeSeriesType.Monthly, TimeSeriesSize.Full, isAdjusted: true);

    IntradayTimeSeries intradayTs = await stocksClient.GetIntradayTimeSeriesAsync("AAPL", IntradayInterval.FifteenMin, TimeSeriesSize.Full);

    GlobalQuote globalQuote = await stocksClient.GetGlobalQuoteAsync("AAPL");

    ICollection<SymbolSearchMatch> searchMatches = await stocksClient.SearchSymbolAsync("BA");
}
```
