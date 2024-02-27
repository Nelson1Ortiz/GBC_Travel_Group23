using Microsoft.AspNetCore.Mvc;
using GBC_Travel_Group23.Data;
using GBC_Travel_Group23.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace GBC_Travel_Group23.Controllers
{
    public class ListingController : Controller
    {
        private readonly AppDbContext _context;

        public ListingController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Listing/BookNow
        public async Task<IActionResult> BookNow()
        {
            var viewModel = new
            {
                Flights = await _context.Flights.ToListAsync(),
                Hotels = await _context.Hotels.Include(h => h.Rooms).ToListAsync(),
                CarRentals = await _context.CarRentals.ToListAsync()
            };

            return View();
        }

        public async Task<IActionResult> Search()
        {
            ViewBag.Flights = await _context.Flights.ToListAsync();
            ViewBag.CarRentals = await _context.CarRentals.ToListAsync();
            ViewBag.Hotels = await _context.Hotels.ToListAsync();

            return View();
        }
    }
}
