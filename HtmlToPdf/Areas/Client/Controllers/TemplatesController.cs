using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HtmlToPdf.Data;
using HtmlToPdf.Models;
using Microsoft.AspNetCore.Authorization;

namespace HtmlToPdf.Areas.Client.Controllers
{
    [Area("Client")]
    [Authorize]
    public class TemplatesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TemplatesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Client/Templates
        public async Task<IActionResult> Index()
        {
            var user = HttpContext.User;
            
            var applicationDbContext = _context.Templates.Include(a=>a.ApplicationUser).Where(a=>a.ApplicationUser.NormalizedUserName== user.Identity.Name);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Client/Templates/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var template = await _context.Templates
                .Include(t => t.ApplicationUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (template == null)
            {
                return NotFound();
            }

            return View(template);
        }

        // GET: Client/Templates/Create
        public IActionResult Create()
        {
            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id");
            return View();
        }

        // POST: Client/Templates/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Template template)
        {
            if (ModelState.IsValid)
            {
                _context.Add(template);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", template.ApplicationUserId);
            return View(template);
        }

        // GET: Client/Templates/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var template = await _context.Templates.FindAsync(id);
            if (template == null)
            {
                return NotFound();
            }
            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", template.ApplicationUserId);
            return View(template);
        }

        public async Task<IActionResult> Preview(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var template = await _context.Templates.FindAsync(id);
            if (template == null)
            {
                return NotFound();
            }

            return View(template);
        }

        // POST: Client/Templates/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Template template)
        {
            if (id != template.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(template);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TemplateExists(template.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", template.ApplicationUserId);
            return View(template);
        }

        // GET: Client/Templates/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var template = await _context.Templates
                .Include(t => t.ApplicationUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (template == null)
            {
                return NotFound();
            }

            return View(template);
        }

        // POST: Client/Templates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var template = await _context.Templates.FindAsync(id);
            _context.Templates.Remove(template);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TemplateExists(int id)
        {
            return _context.Templates.Any(e => e.Id == id);
        }
    }
}
