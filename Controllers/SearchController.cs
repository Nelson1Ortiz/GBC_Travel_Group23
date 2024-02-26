using GBC_Travel_Group23.Data;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult SearchFlights(string from, string to, DateTime departureDate, DateTime returnDate)
        {
            // Your flight search logic here
            // This could involve querying a database, calling an external API, etc.
            // For demonstration purposes, let's just return a dummy result
            var flights = new List<string> { "Flight 1", "Flight 2", "Flight 3" };
            return Json(flights);
        }

        // This action method handles searching for hotels
        [HttpPost]
        public IActionResult SearchHotels(string country, string city, DateTime checkInDate, DateTime checkOutDate)
        {
            // Your hotel search logic here
            // This could involve querying a database, calling an external API, etc.
            // For demonstration purposes, let's just return a dummy result
            var hotels = new List<string> { "Hotel 1", "Hotel 2", "Hotel 3" };
            return Json(hotels);
        }

        // This action method handles searching for car rentals
        [HttpPost]
        public IActionResult SearchCarRentals(string country, string city, DateTime pickUpDate, DateTime dropOffDate)
        {
            // Your car rental search logic here
            // This could involve querying a database, calling an external API, etc.
            // For demonstration purposes, let's just return a dummy result
            var carRentals = new List<string> { "Car Rental 1", "Car Rental 2", "Car Rental 3" };
            return Json(carRentals);



        }
        
    }
}
