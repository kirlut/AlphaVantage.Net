using AlphaVantage.Net.Common.TimeSeries;

namespace AlphaVantage.Net.TechnicalIndicators
{
    public class TechIndicatorTimeSeries : TimeSeriesBase<TechIndicatorDataPoint>
    {
        public TechIndicatorType IndicatorType { get; set; }
    }
}