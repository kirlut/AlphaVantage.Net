using AlphaVantage.Net.Common.Earnings;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaVantage.Net.Fundamentals.Data
{
    public class QuarterlyEarnings : EarningsPeriodBase
    {
        public DateTime ReportedDate { get; set; }
        public decimal EstimatedEPS { get; set; }
        public decimal Surprise { get; set; }
        public decimal SurprisePercentage { get; set; }


        public bool DidMeetExpectedEPS()
        {
            return ReportedEPS >= EstimatedEPS;
        }
    }
}
