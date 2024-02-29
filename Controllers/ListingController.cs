using Microsoft.AspNetCore.Mvc;
using GBC_Travel_Group23.Data;
using GBC_Travel_Group23.Models;
using GBC_Travel_Group23.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using GBC_Travel_Group23.ViewModels;

namespace GBC_Travel_Group23.Controllers
{
    public class ListingController : Controller
    {
        private readonly AppDbContext _context;

        public ListingController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult AddListing() 
        {
            ViewBag.LocationsData = Utils.getAllLocationsString(_context)
                .Select(location => new SelectListItem { Text = location, Value = location });
            ViewBag.Hotels = Utils.GetHotelNames(_context).Select(Hotel => new SelectListItem { Text = Hotel, Value = Hotel });

            return View(); 
        }

        [HttpPost]
        public IActionResult AddFlight(AddListingViewModel model)
        {
            Flight newFlight = new Flight
            {
                Airline = model.Flight.Airline,
                Plane = model.Flight.Plane,
                FlightCode = model.Flight.FlightCode,
                DepartureLocation = Utils.getLocationFromString(model.FromLocation, _context),
                ArrivalLocation = Utils.getLocationFromString(model.ToLocation, _context),
                DepartureDate = model.Flight.DepartureDate,
                ArrivalDate = model.Flight.ArrivalDate,
                TotalSeats = model.Flight.TotalSeats,
                Price = model.Flight.Price
            };
            _context.Flights.Add(newFlight);
            _context.SaveChanges();
            return RedirectToAction("FlightDetails", new { id = newFlight.Id});
        }

        [HttpPost]
        public IActionResult AddCarRental(AddListingViewModel model)
        {
            CarRental newCarRental = new CarRental
            {
                Make = model.CarRental.Make,
                Model = model.CarRental.Model,
                CarYear = model.CarRental.CarYear,
                Capacity = model.CarRental.Capacity,
                Count = model.CarRental.Count,
                Rate = model.CarRental.Rate,
                Location = Utils.getLocationFromString(model.ToLocation, _context)
            };
            _context.CarRentals.Add(newCarRental);
            _context.SaveChanges();
            return RedirectToAction("CarRentalDetails", new {id = newCarRental.Id});
        }

        [HttpPost]
        public IActionResult AddHotel(AddListingViewModel model)
        {
            Hotel newHotel = new Hotel
            {
                Name = model.Hotel.Name,
                Location = Utils.getLocationFromString(model.ToLocation, _context),
                Address = model.Hotel.Address,
                Amenities = model.Hotel.Amenities
            };
            _context.Hotels.Add(newHotel);
            _context.SaveChanges();
            return RedirectToAction("HotelDetails", new { id = newHotel.Id });
        }

        [HttpPost]
        public IActionResult AddHotelRoom(AddListingViewModel model)
        {
            HotelRoom newRoom = new HotelRoom
            {
                Hotel = Utils.GetHotelFromString(model.HotelName, _context),
                RoomName = model.HotelRoom.RoomName,
                MaxOccupants = model.HotelRoom.MaxOccupants,
                Amenities = model.HotelRoom.Amenities,
                RoomCount = model.HotelRoom.RoomCount,
                Rate = model.HotelRoom.Rate
            };
            _context.HotelRooms.Add(newRoom);
            _context.SaveChanges();

            return RedirectToAction("HotelRoomDetails", new {id = newRoom.Id});
        }

        [HttpGet]
        public IActionResult FlightDetails(int id, string mode)
        {
            Flight? flight = _context.Flights
                .Include(f => f.DepartureLocation)
                .Include(f => f.ArrivalLocation)
                .FirstOrDefault(f => f.Id == id);

            if (flight == null)
            {
                return NotFound();
            }
            ViewBag.Flight = flight;
            ViewBag.AvailableSeats = flight.TotalSeats - _context.Bookings.Count(b => b.ServiceId == flight.Id);
            ViewBag.Mode = mode;
            ViewBag.DepartureLocationsData = Utils.getAllLocationsString(_context)
                .Select(location => new SelectListItem { Text = location, Value = location, Selected = (location == Utils.GetLocationString(flight.DepartureLocation)) });
            ViewBag.ArrivalLocationsData = Utils.getAllLocationsString(_context)
                .Select(location => new SelectListItem { Text = location, Value = location, Selected = location == Utils.GetLocationString(flight.ArrivalLocation) });
            return View(flight);
        }

        [HttpPost]
        public IActionResult UpdateFlight(Flight flight)
        {
            var existingFlight = _context.Flights.Find(flight.Id)!;
            existingFlight.Airline = flight.Airline;
            existingFlight.Plane = flight.Plane;
            existingFlight.FlightCode = flight.FlightCode;
            existingFlight.DepartureLocation = flight.DepartureLocation;
            existingFlight.ArrivalLocation = flight.ArrivalLocation;
            existingFlight.DepartureDate = flight.DepartureDate;
            existingFlight.ArrivalDate = flight.ArrivalDate;
            existingFlight.TotalSeats = flight.TotalSeats;
            existingFlight.Price = flight.Price;
            _context.SaveChanges();

            return RedirectToAction("FlightDetails", new { id = existingFlight.Id, mode = "view" });
        }


        [HttpGet]
        public IActionResult CarRentalDetails(int id)
        {
            CarRental? carRental = _context.CarRentals
                .Include(c => c.Location)
                .FirstOrDefault(c => c.Id == id);

            if (carRental == null)
            {
                return NotFound();
            }

            return View(carRental);
        }

        [HttpGet]
        public IActionResult HotelDetails(int id)
        {
            var hotel = _context.Hotels
                .Include(h => h.Location)
                .FirstOrDefault(h => h.Id == id);

            if (hotel == null)
            {
                return NotFound();
            }

            return View(hotel);
        }

        [HttpGet]
        public IActionResult HotelRoomDetails(int id)
        {
            var hotelRoom = _context.HotelRooms
                .Include(hr => hr.Hotel)
                .FirstOrDefault(hr => hr.Id == id);

            if (hotelRoom == null)
            {
                return NotFound();
            }

            return View(hotelRoom);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hotel = await _context.Hotels.Include(h => h.Rooms).FirstOrDefaultAsync(h => h.Id == id);
            if (hotel != null)
            {
                // If the hotel has rooms, delete them
                if (hotel.Rooms.Any())
                {
                    _context.HotelRooms.RemoveRange(hotel.Rooms);
                }

                // Now delete the hotel
                _context.Hotels.Remove(hotel);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index)); // Redirect to the list of hotels
        }




    }
}
