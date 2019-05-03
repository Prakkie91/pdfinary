using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pdfinary.Models
{
    public class Render
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public string BlobUrl { get; set; }
        public string Data { get; set; }
        public RenderType RenderType { get; set; }

        public int SubscriptionId { get; set; }
        public virtual Subscription Subscription { get; set; }

        public int? TemplateId { get; set; }
        public virtual Template Template { get; set; }
    }

    public enum RenderType
    {
        Template = 1,
        Url = 2
    }

}
