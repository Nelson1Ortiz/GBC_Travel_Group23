using Microsoft.AspNetCore.Mvc;
using GBC_Travel_Group23.Data;
using GBC_Travel_Group23.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GBC_Travel_Group23.Controllers
{
    public class ListingController : Controller
    {
        private readonly AppDbContext _context;

        public ListingController(AppDbContext context)
        {
            _context = context;
        }

        
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
        
        [HttpGet]
        public IActionResult AddFlight()
        {
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddFlight(Flight flight)
        {
            if (ModelState.IsValid)
            {
                _context.Add(flight);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(BookNow)); 
            }
            return View(flight);
        }


        public async Task<IActionResult> AddHotel()
        {
            // Fetch the locations from the database
            var locations = await _context.Locations
                .Select(l => new SelectListItem
                {
                    Value = l.Id.ToString(),
                    Text = l.City
                })
                .ToListAsync();

            // Pass the locations to the view for the dropdown
            ViewBag.LocationId = locations;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddHotel(Hotel hotel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hotel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(BookNow)); // Or wherever you'd like to redirect
            }

            // If we get here, something went wrong. Re-populate the LocationId dropdown.
            ViewBag.LocationId = await _context.Locations
                .Select(l => new SelectListItem
                {
                    Value = l.Id.ToString(),
                    Text = l.City
                })
                .ToListAsync();

            return View(hotel);
        }


        [HttpGet]
        public IActionResult AddHotelRoom()
        {
            
            ViewBag.Hotels = new SelectList(_context.Hotels, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddHotelRoom(HotelRoom hotelRoom)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hotelRoom);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(BookNow));
            }
          
            ViewBag.Hotels = new SelectList(_context.Hotels, "Id", "Name");
            return View(hotelRoom);
        }

        public async Task<IActionResult> AddCarRental()
        {
            
            var locations = await _context.Locations
                .Select(l => new SelectListItem
                {
                    Value = l.Id.ToString(),
                    Text = l.City 
                })
                .ToListAsync();

            
            ViewBag.LocationId = locations;

            return View();
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCarRental(CarRental carRental)
        {
            if (ModelState.IsValid)
            {
                _context.Add(carRental);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(BookNow)); 
            }

            
            ViewBag.LocationId = await _context.Locations
                .Select(l => new SelectListItem
                {
                    Value = l.Id.ToString(),
                    Text = l.City
                })
                .ToListAsync();

            return View(carRental);
        }


    }
}
