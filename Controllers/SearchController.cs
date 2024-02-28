using GBC_Travel_Group23.Data;
using GBC_Travel_Group23.Models;
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
        public IActionResult SearchListings(bool searchHotels, bool searchCars, bool searchFlights, string from, string to, DateTime startDate, DateTime endDate)
        {
            Location departureLocation = getLocationFromString(from);
            Location arrivalLocation = getLocationFromString(to);
            if (searchFlights)
            {
                var departureFlights = _context.Flights
                    .Include(f => f.DepartureLocation)
                    .Include(f => f.ArrivalLocation)
                    .Where(f => f.DepartureLocation == departureLocation &&
                                f.ArrivalLocation == arrivalLocation &&
                                f.DepartureDate == startDate)
                    .ToList();
                var returnFlights = _context.Flights
                    .Include(f => f.DepartureLocation)
                    .Include(f => f.ArrivalLocation)
                    .Where(f => f.DepartureLocation == arrivalLocation &&
                                f.ArrivalLocation == departureLocation &&
                                f.DepartureDate == endDate);
                ViewBag.departureFlights = departureFlights;
                ViewBag.returnFlights = returnFlights;
            } else {
                ViewBag.departureFlights = false;
                ViewBag.returnFlights = false;
            }

            if (searchCars)
            {
                var Bookings = _context.Bookings
                    .Include(b => b.CarRental )
                    .Where(b => b.Type == "car").ToList();

                var carRentals = _context.CarRentals
                    .Include(c => c.Location)
                    .Where(c => c.Location == arrivalLocation)
                    .ToList();
            }
            if (searchHotels) 
            {

            }
            return View();

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

        //used to get the location data from the 'city, Country' string format
        //because the dropdowns are populated with the location data it will never be null
        public Location getLocationFromString(string location)
        {
            var parts = location.Split(", ");
            return _context.Locations
                .FirstOrDefault(l => l.City == parts[0] && l.Country == parts[1])!;
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
