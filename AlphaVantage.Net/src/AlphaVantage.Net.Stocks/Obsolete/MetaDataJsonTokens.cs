using System;

// ReSharper disable once CheckNamespace
namespace AlphaVantage.Net.Stocks.Parsing.JsonTokens
{
    [Obsolete("This class is from the old version of AlfaVantage.Net library and will be removed in version 3.1. " +
              "Consider using classes from the latest version: https://github.com/LutsenkoKirill/AlphaVantage.Net")]
    internal static class MetaDataJsonTokens
    {
        public const string MetaDataHeader = "Meta Data";

        public const string InformationToken = "1. Information";
        public const string SymbolToken = "2. Symbol";
        public const string RefreshTimeToken = "3. Last Refreshed";
    }
}