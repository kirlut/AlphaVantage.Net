using AlphaVantage.Net.Common.Currencies;
using System;
using System.Globalization;

namespace AlphaVantage.Net.Common.Parsing
{
    public static class ParsingExtensions
    {
        static readonly string EMPTY_VALUE = "None";
        public static decimal ParseToDecimal(this string stringToParse)
        {
            if (stringToParse == EMPTY_VALUE)
                return 0;
            return decimal.Parse(stringToParse, CultureInfo.InvariantCulture);
        }
        
        public static DateTime ParseToDateTime(this string stringToParse)
        {
            if (stringToParse == EMPTY_VALUE)
                return DateTime.MinValue;
            return DateTime.Parse(stringToParse, CultureInfo.InvariantCulture);
        }
        
        public static long ParseToLong(this string stringToParse)
        {
            if (stringToParse == EMPTY_VALUE)
                return 0;
            return long.Parse(stringToParse, CultureInfo.InvariantCulture);
        }
        
        public static int ParseToInt(this string stringToParse)
        {
            if (stringToParse == EMPTY_VALUE)
                return 0;
            return int.Parse(stringToParse, CultureInfo.InvariantCulture);
        }

        public static PhysicalCurrency ParseToCurrency(this string stringToParse)
        {
            if (stringToParse == EMPTY_VALUE)
                return 0;
            PhysicalCurrency result = PhysicalCurrency.AED;

            if (!Enum.TryParse(stringToParse, out result))
                throw new FormatException("The currency code did not match a known currency code");

            return result;
        }
    }
}