using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HtmlToPdf.Models
{
    public class Payment
    {
        public int Id { get; set; }

        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        public ApplicationUser ApplicationUser { get; set; }

        public DateTime CreateDate { get; set; }

        public decimal Amount { get; set; }

        public DateTime PaidDate { get; set; }

        [StringLength(50)]
        public string Status { get; set; }

    }
}
