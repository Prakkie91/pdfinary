using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HtmlToPdf.Data;
using HtmlToPdf.Models;

namespace HtmlToPdf.Areas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemplatesApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TemplatesApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/TemplatesApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Template>>> GetTemplates()
        {
            return await _context.Templates.ToListAsync();
        }

        // GET: api/TemplatesApi/5
        [HttpPost("{id}")]
        public async Task<ActionResult<Template>> RenderTemplate(int id)
        {
            var template = await _context.Templates.FindAsync(id);

            if (template == null)
            {
                return NotFound();
            }

            return template;
        }
    }
}
