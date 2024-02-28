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
                var availableDepartureFlights = _context.Flights
                    .Include(f => f.DepartureLocation)
                    .Include(f => f.ArrivalLocation)
                    .Select(flight => new
                    {
                        Flight = flight,
                        BookedSeats = _context.Bookings
                            .Where(b => b.ServiceId == flight.Id &&
                                        b.Type == "flight" &&
                                        b.StartDate == startDate)
                            .Sum(b => b.GuestCount)
                    })
                    .Where(result => result.BookedSeats < result.Flight.TotalSeats)
                    .Select(result => new
                    {
                        result.Flight.Id,
                        result.Flight.DepartureLocation,
                        result.Flight.ArrivalLocation,
                        result.Flight.DepartureDate,
                        result.Flight.TotalSeats,
                        AvailableSeats = result.Flight.TotalSeats - result.BookedSeats
                    })
                    .ToList();

                var availableReturnFlights = _context.Flights
                    .Include(f => f.DepartureLocation)
                    .Include(f => f.ArrivalLocation)
                    .Select(flight => new
                    {
                        Flight = flight,
                        BookedSeats = _context.Bookings
                            .Where(b => b.ServiceId == flight.Id &&
                                        b.Type == "flight" &&
                                        b.StartDate == endDate)
                            .Sum(b => b.GuestCount)
                    })
                    .Where(result => result.BookedSeats < result.Flight.TotalSeats)
                    .Select(result => new
                    {
                        result.Flight.Id,
                        result.Flight.DepartureLocation,
                        result.Flight.ArrivalLocation,
                        result.Flight.DepartureDate,
                        result.Flight.TotalSeats,
                        AvailableSeats = result.Flight.TotalSeats - result.BookedSeats
                    })
                    .ToList();

                ViewBag.departureFlights = availableDepartureFlights;
                ViewBag.returnFlights = availableReturnFlights;
            } else {
                ViewBag.departureFlights = false;
                ViewBag.returnFlights = false;
            }

            if (searchCars)
            {
                var availableCarRentals = _context.CarRentals
                    .Where(carRental => carRental.LocationId == arrivalLocation.Id)
                    .Select(carRental => new
                        {
                            CarRental = carRental,
                            BookedCars = _context.Bookings
                                .Where(b => b.ServiceId == carRental.Id &&
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
                ViewBag.CarRentals = availableCarRentals;
            } else
            {
                ViewBag.CarRentals = false;
            }
            if (searchHotels) 
            {
                var availableHotelRooms = _context.HotelRooms
                    .Where(hotelRoom => hotelRoom.Hotel!.LocationId == arrivalLocation.Id)
                    .Select(hotelRoom => new
                    {
                        HotelRoom = hotelRoom,
                        BookedRooms = _context.Bookings
                            .Where(b => b.ServiceId == hotelRoom.Id &&
                                        b.Type == "hotel" &&
                                        b.StartDate < startDate &&
                                        b.EndDate > endDate)
                            .Sum(b => b.GuestCount)
                    })
                    .Where(result => result.BookedRooms < result.HotelRoom.RoomCount)
                    .Select(result => new
                    {
                        result.HotelRoom.Id,
                        result.HotelRoom.RoomName,
                        result.HotelRoom.MaxOccupants,
                        result.HotelRoom.Amenities,
                        result.HotelRoom.RoomCount,
                        AvailableRooms = result.HotelRoom.RoomCount - result.BookedRooms,
                        result.HotelRoom.Rate
                    })
                    .ToList();
                ViewBag.HotelRooms = availableHotelRooms;
            } else
            {
                ViewBag.Hotelrooms = false;
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
