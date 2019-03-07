using HtmlToPdf.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;

namespace HtmlToPdf.Controllers
{
    public class HomeController : BaseController
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
                Uri tmp = new Uri(url.Trim());

                Console.WriteLine("Protocol: {0}", tmp.Scheme);
                Console.WriteLine("Host: {0}", tmp.Host);
                Console.WriteLine("Path: {0}", tmp.AbsolutePath);
                Console.WriteLine("Query: {0}", tmp.Query);

                string parsedUrl = $"{tmp.Scheme}://{tmp.Host}{tmp.AbsolutePath}{tmp.Query}";

                using (WebClient client = new WebClient())
                {
                    MemoryStream pdfMemoryStream = new MemoryStream(client.DownloadData($"https://pdf-render-pdfinary.herokuapp.com/api/render?url={parsedUrl}&scrollPage=true&emulateScreenMedia=true&pdf.scale=0.7"));

                    return new FileStreamResult(pdfMemoryStream, "application/pdf");
                }
            }
            catch (Exception ex)
            {
                AlertDanger(ex.Message);
                return RedirectToAction("Index");
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
