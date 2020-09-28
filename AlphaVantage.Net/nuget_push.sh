#!/bin/bash

PACKAGES=( 
	AlphaVantage.Net.Common 
	AlphaVantage.Net.Core 
	AlphaVantage.Net.Crypto
	AlphaVantage.Net.Forex
	AlphaVantage.Net.Stocks 
	AlphaVantage.Net.TechnicalIndicators 
)
API_KEY=apikey
VERSION=2.0.1

for packageName in "${PACKAGES[@]}"
do
	dotnet nuget push ./src/${packageName}/bin/Release/${packageName}.${VERSION}.nupkg -k ${API_KEY} -s https://api.nuget.org/v3/index.json
done
