using System.Collections.Generic;

namespace Pdfinary.Models
{
    public class Subscription
    {
        public int Id { get; set; }
        public string Company { get; set; }

        public string ApiKey { get; set; }
        public ICollection<Render> Renders { get; set; }
    }
}
