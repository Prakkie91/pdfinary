using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HtmlToPdf.Data;
using HtmlToPdf.Models;
using System.Net;
using System.IO;

namespace HtmlToPdf.Areas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RenderController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RenderController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Render/5
        [HttpGet()]
        public async Task<ActionResult<ApiRequest>> RenderUrlToBlob(string url)
        {
            try
            {
                var tmp = new Uri(url.Trim());

                Console.WriteLine("Protocol: {0}", tmp.Scheme);
                Console.WriteLine("Host: {0}", tmp.Host);
                Console.WriteLine("Path: {0}", tmp.AbsolutePath);
                Console.WriteLine("Query: {0}", tmp.Query);

                var parsedUrl = $"{tmp.Scheme}://{tmp.Host}{tmp.AbsolutePath}{tmp.Query}";

                using (var client = new WebClient())
                {
                    var pdfMemoryStream = new MemoryStream(client.DownloadData($"https://pdf-render-pdfinary.herokuapp.com/api/render?url={parsedUrl}&scrollPage=true&emulateScreenMedia=true&pdf.scale=0.7"));

                    return new FileStreamResult(pdfMemoryStream, "application/pdf");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<ApiRequest>> RenderUrlToStream(string url)
        {
            try
            {
                var tmp = new Uri(url.Trim());

                Console.WriteLine("Protocol: {0}", tmp.Scheme);
                Console.WriteLine("Host: {0}", tmp.Host);
                Console.WriteLine("Path: {0}", tmp.AbsolutePath);
                Console.WriteLine("Query: {0}", tmp.Query);

                var parsedUrl = $"{tmp.Scheme}://{tmp.Host}{tmp.AbsolutePath}{tmp.Query}";

                using (var client = new WebClient())
                {
                    var pdfMemoryStream = new MemoryStream(client.DownloadData($"https://pdf-render-pdfinary.herokuapp.com/api/render?url={parsedUrl}&scrollPage=true&emulateScreenMedia=true&pdf.scale=0.7"));

                    return new FileStreamResult(pdfMemoryStream, "application/pdf");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }
        }

    }
}
