using HtmlToPdf.Areas.Client.Models;
using HtmlToPdf.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace HtmlToPdf.Areas.Client.Controllers
{
    [Area("Client")]
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Client/Dashboard
        public async Task<IActionResult> Index()
        {
            var user = HttpContext.User;

            var userAccount = _context.ApplicationUsers.Include(a => a.SubscriptionType).Include(a => a.Templates).FirstOrDefault(a => a.NormalizedUserName == user.Identity.Name);


            var requests = _context.ApiRequests.Where(a => a.ApplicationUserId == userAccount.Id);

            var model = new DashboardViewModel()
            {
                SubscriptionName = userAccount.SubscriptionType.Name,
                ApiKey = userAccount.ApiKey,
                SubscriptionRequests = userAccount.SubscriptionType.NumberOfRequests,
                DailyRequests = requests.GroupBy(a => a.CreateDate.Date).Select(a => new { Key = a.Key, Count = a.Count() }).ToDictionary(a => a.Key, a => a.Count),
                FailedRequests = requests.Where(a => a.Status == "Failed").Count(),
                TotalRequests = requests.Where(a => a.Status == "Success").Count()
            };

            return View(model);
        }

    }
}
