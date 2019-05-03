using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pdfinary.Models
{
    public class Template
    {
        public int Id { get; set; }
        public int SubscriptionId { get; set; }
        public virtual Subscription Subscription { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public string ProductionHtml { get; set; }
        public string DraftHtml { get; set; }

        public virtual ICollection<Render> Renders { get; set; }
    }
}
