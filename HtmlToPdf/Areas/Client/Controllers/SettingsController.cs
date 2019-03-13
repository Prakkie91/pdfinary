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
    public class SettingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SettingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Client/Settings
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Payments.Include(p => p.ApplicationUser);
            return View(await applicationDbContext.ToListAsync());
        }

        // POST: Client/Settings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id )
        {
                return NotFound();
        }

    }
}
