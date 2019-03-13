using System;
using System.Collections.Generic;

namespace HtmlToPdf.Areas.Client.Models
{
    public class DashboardViewModel
    {
        public string ApiKey { get; set; }
        public string SubscriptionName { get; set; }

        public int SubscriptionRequests { get; set; }
        public int TotalRequests { get; set; }
        public int FailedRequests { get; set; }

        public Dictionary<DateTime, int> DailyRequests { get; set; }
    }
}
