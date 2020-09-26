namespace AlphaVantage.Net.Core.Size
{
    public static class OutputSizeExtensions
    {
        public static string ConvertToString(this OutputSize sizeEnum)
        {
            return sizeEnum == OutputSize.Compact ? "compact" : "full";
        }
    }
}