using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pdfinary.Data;
using Pdfinary.Models;

namespace Pdfinary.Controllers
{
    [Route("api/[controller]")]
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
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRender([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var render = await _context.Renders.FindAsync(id);

            if (render == null)
            {
                return NotFound();
            }

            return Ok(render);
        }

        // PUT: api/RenderApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRender([FromRoute] int id, [FromBody] Render render)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != render.Id)
            {
                return BadRequest();
            }

            _context.Entry(render).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RenderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/RenderApi
        [HttpPost]
        public async Task<IActionResult> PostRender([FromBody] Render render)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Renders.Add(render);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRender", new { id = render.Id }, render);
        }

        // DELETE: api/RenderApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRender([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var render = await _context.Renders.FindAsync(id);
            if (render == null)
            {
                return NotFound();
            }

            _context.Renders.Remove(render);
            await _context.SaveChangesAsync();

            return Ok(render);
        }

        private bool RenderExists(int id)
        {
            return _context.Renders.Any(e => e.Id == id);
        }
    }
}