using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pdfinary.Data;
using Pdfinary.Models;
using Pdfinary.Models.ApiModels;

namespace Pdfinary.Controllers
{
    [Route("api/Render")]
    [ApiController]
    public class RenderApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RenderApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/RenderApi
        [HttpGet]
        public IEnumerable<Render> GetRenders()
        {
            Random rnd = new Random();
            int days = rnd.Next(1, 30);

            _context.Add(new Render() { CreateDate = DateTime.UtcNow.AddDays(-days), BlobUrl = "https://", Data = "", RenderType = RenderType.Url, SubscriptionId = 1 });
            _context.SaveChanges();
            return _context.Renders;
        }


        // GET: api/RenderApi/5
        [HttpGet("RenderUrl")]
        public async Task<IActionResult> RenderUrl(string url, string key)
        {
         

            return Ok();
        }

        // PUT: api/RenderApi/5
        [HttpPost("RenderTemplate")]
        public async Task<IActionResult> RenderTemplate([FromRoute] int id, [FromBody] RenderTemplateRequest render)
        {
         

            return NoContent();
        }

    }
}