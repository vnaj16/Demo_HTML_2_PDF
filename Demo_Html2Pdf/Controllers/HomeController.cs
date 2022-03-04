using Demo_Html2Pdf.Models;
using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Xml.Linq;

namespace Demo_Html2Pdf.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        IConverter _converter;

        public HomeController(ILogger<HomeController> logger, IConverter converter)
        {
            _logger = logger;
            _converter = converter;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy([FromQuery] string name="default")
        {
            var html = $"<h1>Hola PDF, soy {name}, creado desde ASP.NET Core</h1>";
            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings = {
                    PaperSize = PaperKind.A4,
                    Orientation = Orientation.Portrait,
                    Out = "Demo_Aspnet_core.pdf"
                },
                Objects = {
                    new ObjectSettings()
                    {
                        HtmlContent = html
                    }
                }
            };
            _converter.Convert(doc);
            _logger.LogInformation("PDF Creado en teoria");
            return View();
        }

        public IActionResult DownloadPdf([FromQuery] string name = "VNAJ")
{
            var html = $"<h1>Hola PDF, soy {name}, creado desde ASP.NET Core</h1>";
            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings = {
                    PaperSize = PaperKind.A4,
                    Orientation = Orientation.Portrait
                },
                Objects = {
                    new ObjectSettings()
                    {
                        HtmlContent = html
                    }
                }
            };
            var pdfInBytes = _converter.Convert(doc);
            return File(pdfInBytes, "application/pdf", "dwpdf-aspnet.pdf");
            //Other Content-Type: application/vnd.openxmlformats-officedocument.spreadsheetml.sheet
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}