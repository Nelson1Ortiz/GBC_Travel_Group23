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
        public async Task<IActionResult> AddFlight()
        {
            var locationOptions = new SelectList(await _context.Locations.OrderBy(l => l.City).ToListAsync(), "Id", "City");
            ViewBag.DepartureLocationId = locationOptions;
            ViewBag.ArrivalLocationId = locationOptions;
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

        [HttpGet]
        public async Task<IActionResult> AddHotel()
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
        public async Task<IActionResult> AddHotel(Hotel hotel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hotel);
                await _context.SaveChangesAsync();

                
                return RedirectToAction("AddHotelRoom", new { hotelId = hotel.Id });
            }

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
        public async Task<IActionResult> AddHotelRoom(int? hotelId)
        {
            var hotelsSelectList = await _context.Hotels
                .Select(h => new SelectListItem
                {
                    Value = h.Id.ToString(),
                    Text = h.Name,
                    Selected = h.Id == hotelId 
                })
                .ToListAsync();

            ViewBag.Hotels = new SelectList(hotelsSelectList, "Value", "Text");
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
                return RedirectToAction("HotelDetails", new { id = hotelRoom.HotelId });
            }
          //instead of redirecting it to the HotelDeatils page we need to make it go to the listing view.... make sure to add this functionality
          //to all the post actions in this controller to make sure they can see the recent object made



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
