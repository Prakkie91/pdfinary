using System.Collections.Generic;

namespace Pdfinary.Models.ApiModels
{
    public class RenderTemplateRequest
    {
        public string Key { get; set; }
        public int TemplateId { get; set; }

        public Dictionary<string, object> Data { get; set; }
    }
}
