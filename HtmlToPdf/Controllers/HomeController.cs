using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HtmlToPdf.Models;
using System.Net;
using System.IO;

namespace HtmlToPdf.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RenderPdf(string url)
        {
            try
            {
                using (var client = new WebClient())
                {
                    var pdfMemoryStream = new MemoryStream(client.DownloadData($"https://pdf-render-pdfinary.herokuapp.com/api/render?url={url}&scrollPage=true"));

                    return new FileStreamResult(pdfMemoryStream, "application/pdf");
                }
            }
            catch
            {
                return Redirect($"https://pdf-render-pdfinary.herokuapp.com/api/render?url={url}");
            }
        }

        

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
