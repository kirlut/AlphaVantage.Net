using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaVantage.Net.Common.Earnings
{
    public abstract class EarningsPeriodBase
    {
        public DateTime FiscalDateEnding { get; set; }
        public decimal ReportedEPS { get; set; }
    }
}
