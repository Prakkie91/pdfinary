using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HtmlToPdf.Models
{
    public class ApplicationUser: IdentityUser
    {
        [StringLength(50)]
        public string ApiKey { get; set; }

        public int SubscriptionTypeId { get; set; }
        public SubscriptionType SubscriptionType { get; set; }

        public ICollection<Template> Templates { get; set; }
    }
}
