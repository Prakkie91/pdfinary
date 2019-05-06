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

        public double Scale { get; set; }
        public bool ScrollPage { get; set; }
        public bool EmulateScreenMedia { get; set; }
        public string PageFormat { get; set; }
        public bool IsLandscape { get; set; }

        public string ProductionHtml { get; set; }
        public string DraftHtml { get; set; }

        public int MarginTop { get; set; }
        public int MarginBottom { get; set; }
        public int MarginLeft { get; set; }
        public int MarginRight { get; set; }

        public virtual ICollection<Render> Renders { get; set; }
    }
}
