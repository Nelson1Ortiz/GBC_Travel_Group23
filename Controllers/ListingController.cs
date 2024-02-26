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

        // GET: Listing/Flights
        public async Task<IActionResult> Flights()
        {
            var flights = await _context.Flights.ToListAsync();
            return View(flights);
        }

        // GET: Listing/Hotels
        public async Task<IActionResult> Hotels()
        {
            var hotels = await _context.Hotels.Include(h => h.Rooms).ToListAsync();
            return View(hotels);
        }

        // GET: Listing/CarRentals
        public async Task<IActionResult> CarRentals()
        {
            var carRentals = await _context.CarRentals.ToListAsync();
            return View(carRentals);
        }
    }
}