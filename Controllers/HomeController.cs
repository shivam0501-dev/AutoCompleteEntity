using AutoComplete.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace AutoComplete.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DataContext _db;

        public HomeController(ILogger<HomeController> logger,DataContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            var country = _db.countries.Select(a => new SelectListItem()
            {
                Text = a.CountryName,
                Value = a.CountryId.ToString()
            }).ToList();
            ViewBag.items= country;
            return View();
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
        [HttpPost]
        public IActionResult AutoComplete(string prefix, string type, string tablename)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            var autodata = _db.countries.Where(a=>a.CountryName.Contains(prefix)).ToList();
            return Json(autodata);
        }

        [HttpPost]
        public IActionResult AddCountry(string CountryName)
        {
            if (CountryName != null)
            {
                Country country = new() {
                    CountryName=CountryName
                };
                var addCountry = _db.countries.Add(country);
                    _db.SaveChanges();
                TempData["Message"] = "Country Added Successfully";
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public IActionResult GetAllCountry()
        {
            var country = _db.countries.Select(a=>new SelectListItem()
            {
                Text=a.CountryName,
                Value=a.CountryId.ToString()
            }).ToList();
            
            return RedirectToAction(nameof(Index));
        }
    }
}