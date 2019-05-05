using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Pdfinary.Data;
using Pdfinary.Models;
using Pdfinary.Models.ApiModels;
using Pdfinary.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Pdfinary.Controllers
{
    [Route("api/Render/[action]")]
    [ApiController]
    [Produces("application/json")]
    public class RenderApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly BlobStorageService _blobStorageService;

        public RenderApiController(ApplicationDbContext context, BlobStorageService blobStorageService)
        {
            _context = context;
            _blobStorageService = blobStorageService;
        }

        // GET: api/RenderApi
        [HttpGet]
        public IEnumerable<Render> GetRenders()
        {
            Random rnd = new Random();
            int days = rnd.Next(1, 30);

            _context.Add(new Render() { CreateDate = DateTime.UtcNow.AddDays(-days), BlobUrl = "https://", Data = "", RenderType = RenderType.Url, SubscriptionId = 1 });
            _context.SaveChanges();
            return _context.Renders;
        }

        [HttpGet]
        [ActionName("RenderUrl")]
        public async Task<IActionResult> RenderUrl(string url, string key, bool storeBlob = true, bool scrollPage = false, bool emulateScreenMedia = true, double scale = 1, int pageRanges = 0, string format = "A4", bool landscape = false)
        {
            try
            {
                Subscription subscription = _context.Subscriptions.FirstOrDefault(a => a.ApiKey == key);

                Uri tmp = new Uri(url.Trim());

                Console.WriteLine("Protocol: {0}", tmp.Scheme);
                Console.WriteLine("Host: {0}", tmp.Host);
                Console.WriteLine("Path: {0}", tmp.AbsolutePath);
                Console.WriteLine("Query: {0}", tmp.Query);

                string parsedUrl = $"{tmp.Scheme}://{tmp.Host}{tmp.AbsolutePath}{tmp.Query}";


                if (storeBlob)
                {
                    byte[] randomKey = new byte[15];
                    using (RandomNumberGenerator generator = RandomNumberGenerator.Create())
                    {
                        generator.GetBytes(randomKey);
                    }

                    string randomFileName = Convert.ToBase64String(randomKey);

                    Regex rgx = new Regex("[^a-zA-Z0-9 -]");
                    randomFileName = rgx.Replace(randomFileName, "");

                    string filename = $"{randomFileName}.pdf";

                    Render render = new Render()
                    {
                        CreateDate = DateTime.UtcNow,
                        Data = JsonConvert.SerializeObject(new
                        {
                            Url = url
                        }),
                        RenderType = RenderType.Url,
                        SubscriptionId = subscription.Id,
                        BlobUrl = $"https://pdfinary.blob.core.windows.net/pdfinary/{filename}"
                    };

                    _context.Renders.Add(render);
                    _context.SaveChanges();

                    using (WebClient client = new WebClient())
                    {
                        MemoryStream pdfMemoryStream = new MemoryStream(client.DownloadData($"https://pdf-render-pdfinary.herokuapp.com/api/render?url={parsedUrl}&scrollPage={scrollPage}&emulateScreenMedia={emulateScreenMedia}&pdf.scale={scale}&pdf.format={format}&pdf.landscape={landscape}"));

                        await _blobStorageService.UploadFileAsync(pdfMemoryStream, filename);

                        pdfMemoryStream.Position = 0;

                        return new FileStreamResult(pdfMemoryStream, "application/pdf");
                    }
                }
                else
                {

                    using (WebClient client = new WebClient())
                    {
                        MemoryStream pdfMemoryStream = new MemoryStream(client.DownloadData($"https://pdf-render-pdfinary.herokuapp.com/api/render?url={parsedUrl}&scrollPage={scrollPage}&emulateScreenMedia={emulateScreenMedia}&pdf.scale={scale}&pdf.format={format}&pdf.landscape={landscape}"));

                        pdfMemoryStream.Position = 0;

                        return new FileStreamResult(pdfMemoryStream, "application/pdf");
                    }

                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [ActionName("RenderTemplate")]
        public async Task<IActionResult> RenderTemplate(RenderTemplateRequest data)
        {
            try
            {
                Subscription subscription = _context.Subscriptions.FirstOrDefault(a => a.ApiKey == data.Key);

                byte[] randomKey = new byte[15];
                using (RandomNumberGenerator generator = RandomNumberGenerator.Create())
                {
                    generator.GetBytes(randomKey);
                }

                string randomFileName = Convert.ToBase64String(randomKey);

                Regex rgx = new Regex("[^a-zA-Z0-9 -]");
                randomFileName = rgx.Replace(randomFileName, "");

                string filename = $"{randomFileName}.pdf";

                Render render = new Render()
                {
                    CreateDate = DateTime.UtcNow,
                    Data = JsonConvert.SerializeObject(data.Data),
                    RenderType = RenderType.Url,
                    SubscriptionId = subscription.Id,
                    TemplateId = data.TemplateId,
                    BlobUrl = $"https://pdfinary.blob.core.windows.net/pdfinary/{filename}"
                };

                _context.Renders.Add(render);
                _context.SaveChanges();

                using (WebClient client = new WebClient())
                {
                    string urlOut = $"http://pdfinary.com/Renders/Preview/{render.Id}";

                    MemoryStream pdfMemoryStream = new MemoryStream(client.DownloadData($"https://pdf-render-pdfinary.herokuapp.com/api/render?url={urlOut}&scrollPage=true&emulateScreenMedia=true&pdf.scale=0.7"));

                    await _blobStorageService.UploadFileAsync(pdfMemoryStream, filename);

                    pdfMemoryStream.Position = 0;

                    return new FileStreamResult(pdfMemoryStream, "application/pdf");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}