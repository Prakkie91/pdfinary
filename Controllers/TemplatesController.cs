using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pdfinary.Data;
using Pdfinary.Models;

namespace Pdfinary.Controllers
{
    public class TemplatesController : BaseController
    {
        private readonly ApplicationDbContext _context;

        public TemplatesController(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _context = context;
        }

        // GET: Templates
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Templates.Include(t => t.Subscription).Where(a => a.SubscriptionId == _subscriptionId);
            return View(await applicationDbContext.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> SetDraftToProduction(int id)
        {
            var template = _context.Templates.FirstOrDefault(a => a.Id == id);
            template.ProductionHtml = template.DraftHtml;
            _context.Update(template);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Render(int id, bool draft)
        {
            var template = _context.Templates.FirstOrDefault(a => a.Id == id);

            ViewBag.Html = template.DraftHtml;

            if (draft)
            {
                ViewBag.Html = template.DraftHtml;
            }
            else
            {
                ViewBag.Html = template.ProductionHtml;

            }
            return View();
        }

        // GET: Templates/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Templates/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Template template)
        {
            if (ModelState.IsValid)
            {
                template.EmulateScreenMedia = true;
                template.IsLandscape = false;
                template.PageFormat = "A4";
                template.Scale = 0.7;
                template.ScrollPage = true;
                template.SubscriptionId = _subscriptionId;
                _context.Add(template);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(template);
        }

        // GET: Templates/Edit/5
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
            ViewData["SubscriptionId"] = new SelectList(_context.Subscriptions, "Id", "Id", template.SubscriptionId);
            return View(template);
        }

        // POST: Templates/Edit/5
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

            return View(template);
        }

        // GET: Templates/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var template = await _context.Templates
                .Include(t => t.Subscription)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (template == null)
            {
                return NotFound();
            }

            return View(template);
        }

        // POST: Templates/Delete/5
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
