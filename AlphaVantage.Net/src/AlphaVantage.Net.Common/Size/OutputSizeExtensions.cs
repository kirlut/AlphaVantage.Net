namespace AlphaVantage.Net.Common.Size
{
    public static class OutputSizeExtensions
    {
        public static string ConvertToQueryString(this OutputSize sizeEnum)
        {
            return sizeEnum == OutputSize.Compact ? "compact" : "full";
        }
    }
}