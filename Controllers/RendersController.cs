using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pdfinary.Data;
using Pdfinary.Models;

namespace Pdfinary.Controllers
{
    public class RendersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RendersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Renders
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Renders.Include(r => r.Template);
            return View(await applicationDbContext.ToListAsync());
        }
    }
}
