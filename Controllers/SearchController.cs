using GBC_Travel_Group23.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GBC_Travel_Group23.Controllers
{
    public class SearchController : Controller
    {
        private readonly AppDbContext _context;
        public SearchController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult SearchFlights(string from, string to, DateTime departureDate)
        {
            // Assuming you have a dbContext to interact with your database
            var flights = _context.Flights
                .Include(f => f.DepartureLocation)
                .Include(f => f.ArrivalLocation)
                .Where(f => f.DepartureLocation.AirportCode == from &&
                            f.ArrivalLocation.AirportCode == to &&
                            f.DepartureDate.Date == departureDate.Date)
                .ToList();

            return View(flights);
        }


        // This action is for searching hotels based on location
        public IActionResult SearchHotels(string country, string city)
        {
            var location = _context.Locations.FirstOrDefault(l => l.Country == country && l.City == city);

            if (location == null)
            {
                // Handle the case where the location is not found
                return View("NotFound");
            }

            var hotels = _context.Hotels
                .Include(h => h.Location)
                .Where(h => h.LocationId == location.Id)
                .ToList();

            return View(hotels);
        }

        public IActionResult HotelDetails(int id)
        {
            var hotel = _context.Hotels
                .Include(h => h.Location)
                .Include(h => h.Rooms) 
                .FirstOrDefault(h => h.Id == id);

            if (hotel == null)
            {
                return NotFound();
            }

                
            return View(hotel);
        }

        public IActionResult SearchCarRentals(int locationId)
        {
            var carRentals = _context.CarRentals
                .Include(cr => cr.Location)
                .Where(cr => cr.LocationId == locationId)
                .ToList();

            return View(carRentals);
        }
    }
}
