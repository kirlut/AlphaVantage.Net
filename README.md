# AlphaVantage.Net
.Net client library for [**Alpha Vantage API**](https://www.alphavantage.co/).  
Solution consist of two packages: 
- [AlphaVantage.Net.Core](AlphaVantage.Net/src/AlphaVantage.Net.Core) - low-level wrapper for Alpha Vantage API based on `HttpClient` and `Newtonsoft Json.Net`
- [AlphaVantage.Net.Stocks](AlphaVantage.Net/src/AlphaVantage.Net.Stocks) - hight-level client for retrieving [stock time series data](https://www.alphavantage.co/documentation/#time-series-data) from Alpha Vantage API based on `AlphaVantage.Net.Core`

## AlphaVantage.Net.Stocks
### Installation: 
- Package Manager:  
`Install-Package AlphaVantage.Net.Stocks -Version 1.0.1`  
- .NET CLI:  
`dotnet add package AlphaVantage.Net.Stocks --version 1.0.1`  
### Usage example: 
```csharp
public async Task AlphaVantageStocksDemo()
{
    string apiKey = "1"; // enter your API key here

    var client = new AlphaVantageStocksClient(apiKey);

    // retrieve daily time series for stocks of Apple Inc.:
    StockTimeSeries timeSeries = await client.RequestDailyTimeSeriesAsync("AAPL", TimeSeriesSize.Compact, adjusted: false);
    foreach (var dataPoint in timeSeries.DataPoints)
    {
        Console.WriteLine($"{dataPoint.Time}: {dataPoint.ClosingPrice}");
    }

    // retrieve stocks batch quotes for Apple Inc. and Facebook Inc.:
    ICollection<StockQuote> batchQuotes = await client.RequestBatchQuotesAsync(new[] {"AAPL", "FB"});
    foreach (var stockQuote in batchQuotes)
    {
        Console.WriteLine($"{stockQuote.Symbol}: {stockQuote.Price}");
    }
}
```

## AlphaVantage.Net.Core
### Installation: 
- Package Manager:  
`Install-Package AlphaVantage.Net.Core -Version 1.0.0`  
- .NET CLI:  
`dotnet add package AlphaVantage.Net.Core --version 1.0.0`  
### Usage example: 
```csharp
public static async Task AlphaVantageCoreDemo()
{
    var coreClient = new AlphaVantageCoreClient();

    string apiKey = "1"; // enter your API key here

    // retrieve stocks batch quoutes of Apple Inc. and Facebook Inc.:
    var query = new Dictionary<string, string>()
    {
         {"symbols", "FB,AAPL"}
    };
    JObject deserialisedResponse = await coreClient.RequestApiAsync(apiKey, ApiFunction.BATCH_STOCK_QUOTES, query);           
    // parse JObject here
}
```
