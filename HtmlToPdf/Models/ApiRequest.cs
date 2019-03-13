using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HtmlToPdf.Models
{
    public class ApiRequest
    {
        public int Id { get; set; }

        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        public ApplicationUser ApplicationUser { get; set; }

        [StringLength(50)]
        public string Url { get; set; }

        [StringLength(50)]
        public string Status { get; set; }

        public DateTime CreateDate { get; set; }

    }
}
