using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pdfinary.Models
{
    public class ApplicationUser : IdentityUser
    {
        public int? SubscriptionId { get; set; }
        public virtual Subscription Subscription { get; set; }
    }
}
