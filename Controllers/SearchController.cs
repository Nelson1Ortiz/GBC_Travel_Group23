using GBC_Travel_Group23.Data;
using GBC_Travel_Group23.Models;
using GBC_Travel_Group23.ViewModels;
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
        public IActionResult SearchListings(SearchViewModel model)
        {
            var locations = _context.Locations;
            List<string> locationsData = new List<string>();

            foreach (var location in locations)
            {
                locationsData.Add($"{location.City}, {location.Country}");
            }
            ViewBag.LocationsData = locationsData;
            SearchViewModel viewModel = new SearchViewModel();

            bool searchHotels = model.SearchHotels;
            bool searchCars = model.SearchCars;
            bool searchFlights = model.SearchFlights;
            string from = model.From;
            string to = model.To;
            DateTime startDate = model.StartDate;
            DateTime endDate = model.EndDate;
            Location departureLocation = getLocationFromString(from);
            Location arrivalLocation = getLocationFromString(to);
            ViewBag.SearchFlights = searchFlights;
            ViewBag.SearchCars = searchCars;
            ViewBag.SearchHotels = searchHotels;


            if (searchFlights)
            {
                var availableDepartureFlights = _context.Flights
                    .Include(f => f.DepartureLocation)
                    .Include(f => f.ArrivalLocation)
                    .Where(f => f.DepartureLocation == departureLocation &&
                                     f.ArrivalLocation == arrivalLocation
                    )
                    .Select(f => new
                    {
                        Flight = f,
                        BookedSeats = _context.Bookings
                            .Where(b => b.ServiceId == f.Id &&
                                        b.Type == "flight" &&
                                        b.StartDate == startDate)
                            .Sum(b => b.GuestCount)
                    })
                    .Where(r => r.BookedSeats < r.Flight.TotalSeats)
                    .Select(r => new
                    {
                        r.Flight.Id,
                        r.Flight.FlightCode,
                        r.Flight.Airline,
                        DepartureLocation = $"{r.Flight.DepartureLocation.City}, {r.Flight.DepartureLocation.Country}",
                        ArrivalLocation = $"{r.Flight.ArrivalLocation.City}, {r.Flight.ArrivalLocation.Country}",
                        DepartureDate = r.Flight.DepartureDate.Date,
                        r.Flight.TotalSeats,
                        AvailableSeats = r.Flight.TotalSeats - r.BookedSeats
                    })
                    .ToList();

                var availableReturnFlights = _context.Flights
                    .Include(f => f.DepartureLocation)
                    .Include(f => f.ArrivalLocation)
                    .Where(f => f.DepartureLocation == arrivalLocation &&
                                     f.ArrivalLocation == departureLocation
                    )
                    .Select(f => new
                    {
                        Flight = f,
                        BookedSeats = _context.Bookings
                            .Where(b => b.ServiceId == f.Id &&
                                        b.Type == "flight" &&
                                        b.StartDate == endDate)
                            .Sum(b => b.GuestCount)
                    })
                    .Where(r => r.BookedSeats < r.Flight.TotalSeats)
                    .Select(r => new
                    {
                        r.Flight.Id,
                        r.Flight.FlightCode,
                        r.Flight.Airline,
                        DepartureLocation = $"{r.Flight.DepartureLocation.City}, {r.Flight.DepartureLocation.Country}",
                        ArrivalLocation = $"{r.Flight.ArrivalLocation.City}, {r.Flight.ArrivalLocation.Country}",
                        DepartureDate = r.Flight.DepartureDate.Date,
                        r.Flight.TotalSeats,
                        AvailableSeats = r.Flight.TotalSeats - r.BookedSeats
                    })
                    .ToList();

                ViewBag.AvailableDepartureFlights = availableDepartureFlights;
                ViewBag.AvailableReturnFlights = availableReturnFlights;
            }

            if (searchCars)
            {
                var availableCarRentals = _context.CarRentals
                    .Where(c => c.LocationId == arrivalLocation.Id)
                    .Select(c => new
                        {
                            CarRental = c,
                            BookedCars = _context.Bookings
                                .Where(b => b.ServiceId == c.Id &&
                                            b.Type == "car" &&
                                            b.StartDate < startDate &&
                                            b.EndDate > endDate)
                                .Sum(b => b.GuestCount)
                        })
                    .Where(r => r.BookedCars < r.CarRental.Count)
                    .Select(r => new
                    {
                        r.CarRental.Id,
                        r.CarRental.Make,
                        r.CarRental.Model,
                        r.CarRental.CarYear,
                        r.CarRental.Capacity,
                        AvailableCars = r.CarRental.Count - r.BookedCars,
                        r.CarRental.Rate
                    })
                    .ToList();
                ViewBag.AvailableCarRentals = availableCarRentals;
            }
            if (searchHotels) 
            {
                var availableHotelRooms = _context.HotelRooms
                    .Include(hr => hr.Hotel)
                    .Where(hr => hr.Hotel!.LocationId == arrivalLocation.Id)
                    .Select(hr => new
                    {
                        HotelRoom = hr,
                        BookedRooms = _context.Bookings
                            .Where(b => b.ServiceId == hr.Id &&
                                        b.Type == "hotel" &&
                                        b.StartDate < startDate &&
                                        b.EndDate > endDate)
                            .Sum(b => b.GuestCount)
                    })
                    .Where(r => r.BookedRooms < r.HotelRoom.RoomCount)
                    .Select(r => new
                    {
                        r.HotelRoom.Id,
                        HotelName = r.HotelRoom.Hotel!.Name,
                        r.HotelRoom.RoomName,
                        r.HotelRoom.Amenities,
                        r.HotelRoom.MaxOccupants,
                        AvailableRooms = r.HotelRoom.RoomCount - r.BookedRooms,
                        r.HotelRoom.RoomCount,
                        r.HotelRoom.Rate
                    })
                    .ToList();
                ViewBag.AvailableHotelRooms = availableHotelRooms;
            }
            return View(viewModel);

        }
        //used to get the location data from the 'city, Country' string format
        //because the dropdowns are populated with the location data it will never be null
        public Location getLocationFromString(string location)
        {
            var parts = location.Split(", ");
            return _context.Locations
                .FirstOrDefault(l => l.City == parts[0] && l.Country == parts[1])!;
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

       
    }
}
