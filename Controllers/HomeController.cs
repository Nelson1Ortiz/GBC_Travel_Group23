using GBC_Travel_Group23.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using GBC_Travel_Group23.Data;
using GBC_Travel_Group23.ViewModels;

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
            ViewBag.LocationsData = getAllLocationsString();
            SearchViewModel viewModel = new SearchViewModel();
            return View(viewModel);
        }
        public IActionResult About()
        {
            return View();
        }

        public List<string> getAllLocationsString()
        {
            var locations = _context.Locations;
            List<string> locationsData = new List<string>();

            foreach (var location in locations)
            {
                locationsData.Add($"{location.City}, {location.Country}");
            }
            return locationsData;
        }
        
        




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
