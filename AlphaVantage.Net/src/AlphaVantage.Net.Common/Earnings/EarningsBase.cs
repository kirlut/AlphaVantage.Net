using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaVantage.Net.Common.Earnings
{
    public abstract class EarningsBase<TQuartleryEarnings, TAnnualEarnings>
        where TQuartleryEarnings : EarningsPeriodBase
        where TAnnualEarnings : EarningsPeriodBase
    {
        public string Symbol { get; set; } = String.Empty;

        public ICollection<TAnnualEarnings> AnnualEarnings { get; set; } = new List<TAnnualEarnings>();
        public ICollection<TQuartleryEarnings> QuarterlyEarnings { get; set; } = new List<TQuartleryEarnings>();
    }
}
