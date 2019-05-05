using HandlebarsDotNet;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Pdfinary.Data;
using Pdfinary.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Pdfinary.Controllers
{
    public class RendersController : BaseController
    {
        private readonly ApplicationDbContext _context;

        public RendersController(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _context = context;
        }

        // GET: Renders
        public async Task<IActionResult> Index()
        {
            IQueryable<Render> applicationDbContext = _context.Renders.Include(r => r.Template).Where(a => a.SubscriptionId == _subscriptionId).OrderByDescending(a => a.CreateDate).Take(30);
            return View(await applicationDbContext.ToListAsync());
        }


        public async Task<IActionResult> Preview(int id)
        {
            Render render = _context.Renders.Include(a => a.Template).FirstOrDefault(a => a.Id == id);

            System.Func<object, string> template = Handlebars.Compile(render.Template.ProductionHtml);

            object data = JsonConvert.DeserializeObject(render.Data);

            ViewBag.TemplateHtml = template(data); ;

            return View();

        }
    }
}
