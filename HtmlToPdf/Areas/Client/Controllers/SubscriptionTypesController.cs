using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HtmlToPdf.Data;
using HtmlToPdf.Models;

namespace HtmlToPdf.Areas.Client.Controllers
{
    [Area("Client")]
    public class SubscriptionTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SubscriptionTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Client/SubscriptionTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.SubscriptionTypes.ToListAsync());
        }

        // GET: Client/SubscriptionTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subscriptionType = await _context.SubscriptionTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subscriptionType == null)
            {
                return NotFound();
            }

            return View(subscriptionType);
        }

        // GET: Client/SubscriptionTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Client/SubscriptionTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,NumberOfRequests,MonthlyCost")] SubscriptionType subscriptionType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(subscriptionType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(subscriptionType);
        }

        // GET: Client/SubscriptionTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subscriptionType = await _context.SubscriptionTypes.FindAsync(id);
            if (subscriptionType == null)
            {
                return NotFound();
            }
            return View(subscriptionType);
        }

        // POST: Client/SubscriptionTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,NumberOfRequests,MonthlyCost")] SubscriptionType subscriptionType)
        {
            if (id != subscriptionType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subscriptionType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubscriptionTypeExists(subscriptionType.Id))
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
            return View(subscriptionType);
        }

        // GET: Client/SubscriptionTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subscriptionType = await _context.SubscriptionTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subscriptionType == null)
            {
                return NotFound();
            }

            return View(subscriptionType);
        }

        // POST: Client/SubscriptionTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var subscriptionType = await _context.SubscriptionTypes.FindAsync(id);
            _context.SubscriptionTypes.Remove(subscriptionType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubscriptionTypeExists(int id)
        {
            return _context.SubscriptionTypes.Any(e => e.Id == id);
        }
    }
}
