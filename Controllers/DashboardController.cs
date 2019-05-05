using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pdfinary.Data;
using Pdfinary.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Pdfinary.Controllers
{
    public class DashboardController : BaseController
    {
        private readonly ApplicationDbContext _context;

        public DashboardController(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _context = context;
        }

        // GET: Dashboard
        public async Task<IActionResult> Index()
        {
            Subscription subscription = _context.Subscriptions.FirstOrDefault(a => a.Id == _subscriptionId);

            ViewBag.Subscription = subscription;

            System.Collections.Generic.List<Render> renders = _context.Renders.Include(r => r.Template).Where(a => a.SubscriptionId == _subscriptionId).ToList();

            return View(renders);
        }

    }
}
