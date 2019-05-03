using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pdfinary.Models
{
    public class Subscription
    {
        public int Id { get; set; }
        public string Company { get; set; }

        public string ApiKey { get; set; }
    }
}
