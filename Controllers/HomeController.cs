using GBC_Travel_Group23.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using GBC_Travel_Group23.Data;
using GBC_Travel_Group23.ViewModels;
using GBC_Travel_Group23.Helpers;

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
            ViewBag.LocationsData = Utils.getAllLocationsString(_context);
            SearchViewModel viewModel = new SearchViewModel();
            return View(viewModel);
        }
        public IActionResult About()
        {
            return View();
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
