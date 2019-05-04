using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pdfinary.Data;
using Pdfinary.Models;

namespace Pdfinary.Controllers
{
    public class QuickStartController : BaseController
    {
        private readonly ApplicationDbContext _context;

        public QuickStartController(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            Subscription subscription = _context.Subscriptions.FirstOrDefault(a => a.Id == _subscriptionId);

            ViewBag.Subscription = subscription;

            return View();
        }
    }
}