using CalcTaxApp.Models;
using CalcTaxApp.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Reflection;

namespace CalcTaxApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITaxService _taxService;

        public HomeController(ILogger<HomeController> logger, ITaxService taxService)
        {
            _logger = logger;
            _taxService = taxService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult MyCv()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CalTax(NigeriaTaxViewModel model)
        {
            var result = _taxService.Calculate(model.GrossIncome);

            model.CRA = result.cra;
            model.TaxableIncome = result.taxableIncome;
            model.Tax = result.tax;
            model.NetIncome = model.GrossIncome - model.Tax;

            return View(model);
        }

        [HttpGet]
        public IActionResult CalTax()
        {
            return View(new NigeriaTaxViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> UploadDocument(IFormFile file)
        {
            var model = new DocumentViewModel();

            if (file != null && file.Length > 0)
            {
                var uploadsFolder = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot",
                    "uploads");

                Directory.CreateDirectory(uploadsFolder);

                var filePath = Path.Combine(
                    uploadsFolder,
                    file.FileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                model.DocumentText = ExtractTextFromWord(filePath);
            }

            return View("Index");
        }

        private string ExtractTextFromWord(string filePath)
        {
            //using (WordprocessingDocument document =
            //       WordprocessingDocument.Open(filePath, false))
            //{
            //    return document.MainDocumentPart
            //                   ?.Document
            //                   ?.Body
            //                   ?.InnerText ?? "";
            //}
            return "";
        }

        public IActionResult Download()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/files/MyCV.pdf");
            var fileBytes = System.IO.File.ReadAllBytes(filePath);

            return File(fileBytes, "application/pdf", "MyCV.pdf");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
