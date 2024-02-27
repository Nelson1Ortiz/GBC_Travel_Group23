using GBC_Travel_Group23.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using GBC_Travel_Group23.Data;

namespace GBC_Travel_Group23.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;

        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        public IActionResult Index()
        {
            var locations = _context.Locations;
            List<string> locationsData = new List<string>();

            foreach (var location in locations)
            {
                locationsData.Add($"{location.City}, {location.Country}");
            }
            ViewBag.LocationsData = locationsData;
            return View();
        }
        public IActionResult About()
        {
            return View();
        }
        public IActionResult oldIndex() { return View(); }
        




        public IActionResult NotFound(int statusCode)
        {
            if (statusCode == 404)
            {
                return View("NotFound");

            }
            return View("Error");
        }
    }
}
