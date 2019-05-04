using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pdfinary.Data;
using Pdfinary.Models;
using System.Linq;
using System.Threading.Tasks;

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
            Microsoft.EntityFrameworkCore.Query.IIncludableQueryable<Render, Template> applicationDbContext = _context.Renders.Include(r => r.Template);
            return View(await applicationDbContext.ToListAsync());
        }


        public async Task<IActionResult> Preview(int id)
        {
            Render render = _context.Renders.Include(a => a.Template).FirstOrDefault(a => a.Id == id);

            return View(render.Template.ProductionHtml);

        }
    }
}
