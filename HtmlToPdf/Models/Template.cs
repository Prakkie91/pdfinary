using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HtmlToPdf.Models
{
    public class Template
    {
        public int Id { get; set; }

        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        public ApplicationUser ApplicationUser { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime LastModifiedDate { get; set; }

        public string Body { get; set; }
    }
}
