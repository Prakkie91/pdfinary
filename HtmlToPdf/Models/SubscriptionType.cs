using System.ComponentModel.DataAnnotations;

namespace HtmlToPdf.Models
{
    public class SubscriptionType
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }
        public int NumberOfRequests { get; set; }
        public double MonthlyCost { get; set; }
    }
}
