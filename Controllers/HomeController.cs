using GBC_Travel_Group23.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace GBC_Travel_Group23.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
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
