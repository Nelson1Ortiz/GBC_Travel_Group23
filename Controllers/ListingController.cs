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

        //Add Listings Link
        public IActionResult AddListing() 
        {
            ViewBag.LocationsData = Utils.getAllLocationsString(_context)
                .Select(location => new SelectListItem { Text = location, Value = location });
            ViewBag.Hotels = Utils.GetHotelNames(_context).Select(Hotel => new SelectListItem { Text = Hotel, Value = Hotel });

            return View(); 
        }


        //Add Listings Db methods
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

        //Listing Details
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

        [HttpGet]
        public IActionResult CarRentalDetails(int id, string mode)
        {
            CarRental? carRental = _context.CarRentals
                .Include(c => c.Location)
                .FirstOrDefault(c => c.Id == id);

            if (carRental == null)
            {
                return NotFound();
            }

            ViewBag.LocationsData = Utils.getAllLocationsString(_context)
                .Select(location => new SelectListItem { Text = location, Value = location, Selected = location == Utils.GetLocationString(carRental.Location) });
            ViewBag.Mode = mode;
            return View(carRental);
        }

        [HttpGet]
        public IActionResult HotelDetails(int id, string mode)
        {
            var hotel = _context.Hotels
                .Include(h => h.Location)
                .FirstOrDefault(h => h.Id == id);

            if (hotel == null)
            {
                return NotFound();
            }

            
            ViewBag.LocationsData = Utils.getAllLocationsString(_context)
                .Select(location => new SelectListItem { Text = location, Value = location, Selected = location == Utils.GetLocationString(hotel.Location) });
            ViewBag.Mode = mode;
            return View(hotel);
        }

        [HttpGet]
        public IActionResult HotelRoomDetails(int id, string mode)
        {
            var hotelRoom = _context.HotelRooms
                .Include(hr => hr.Hotel)
                    .ThenInclude(h => h.Location)
                .FirstOrDefault(hr => hr.Id == id);

            if (hotelRoom == null)
            {
                return NotFound();
            }

         
            ViewBag.HotelLocation = Utils.GetLocationString(hotelRoom.Hotel.Location);
            ViewBag.Mode = mode;
            return View(hotelRoom);
        }

        //Update Methods
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
        
        [HttpPost]
        public IActionResult UpdateCarRental(CarRental carRental)
        {
            var existingCarRental = _context.CarRentals.Find(carRental.Id)!;
            existingCarRental.Make = carRental.Make;
            existingCarRental.Model = carRental.Model;
            existingCarRental.CarYear = carRental.CarYear;
            existingCarRental.Capacity = carRental.Capacity;
            existingCarRental.Count = carRental.Count;
            existingCarRental.Rate = carRental.Rate;
            existingCarRental.Location = carRental.Location;

            _context.SaveChanges();


            return RedirectToAction("CarRentalDetails", new { id = existingCarRental.Id, mode = "view" });
        }
       
        [HttpPost]
        public IActionResult UpdateHotel(Hotel hotel)
        {
            var existingHotel = _context.Hotels.Find(hotel.Id);
            existingHotel!.Name = hotel.Name;
            existingHotel.Location = hotel.Location; 
            existingHotel.Address = hotel.Address;
            existingHotel.Amenities = hotel.Amenities;

            _context.SaveChanges();


            return RedirectToAction("HotelDetails", new { id = existingHotel.Id, mode = "view" });
        }

        [HttpPost]
        public IActionResult UpdateHotelRoom(HotelRoom hotelRoom)
        {
            var existingHotelRoom = _context.HotelRooms.Find(hotelRoom.Id);
            existingHotelRoom!.RoomName = hotelRoom.RoomName;
            existingHotelRoom.MaxOccupants = hotelRoom.MaxOccupants;
            existingHotelRoom.Amenities = hotelRoom.Amenities;
            existingHotelRoom.RoomCount = hotelRoom.RoomCount;
            existingHotelRoom.Rate = hotelRoom.Rate;

            _context.SaveChanges();


            return RedirectToAction("HotelDetails", new { id = existingHotelRoom.Id, mode = "view" });
        }
        


        //Delete methods
        [HttpGet]
        public IActionResult DeleteFlight(int id)
        {
            var flight = _context.Flights.Find(id);
            if (flight != null)
            {
                var assocBookings = _context.Bookings.Where(b => b.Type == "flight" && b.ServiceId == id).ToList();
                if (assocBookings.Count > 0)
                {
                    _context.Bookings.RemoveRange(assocBookings);
                }
                _context.Flights.Remove(flight);
                _context.SaveChanges();
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult DeleteCarRental(int id)
        {
            var carRental = _context.CarRentals.Find(id);
            if (carRental != null)
            {
                var assocBookings = _context.Bookings.Where(b => b.Type == "car" && b.ServiceId == id).ToList();
                if (assocBookings.Count > 0)
                {
                    _context.Bookings.RemoveRange(assocBookings);
                }
                _context.CarRentals.Remove(carRental);
                _context.SaveChanges();
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult DeleteHotelRoom(int id)
        {
            var hotelRoom = _context.HotelRooms.Find(id);
            if (hotelRoom != null)
            {
                var assocBookings = _context.Bookings.Where(b => b.Type == "car" && b.ServiceId == id).ToList();
                if (assocBookings.Count > 0)
                {
                    _context.Bookings.RemoveRange(assocBookings);
                }
                _context.HotelRooms.Remove(hotelRoom);
                _context.SaveChanges();
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult DeleteHotel(int id)
        {
            var hotel = _context.Hotels.Include(h => h.Rooms).FirstOrDefault(h => h.Id == id);
            if (hotel != null)
            {
                var assocBookings = _context.Bookings.Where(b => b.Type == "car" && b.ServiceId == id).ToList();
                if (assocBookings.Count > 0)
                {
                    _context.Bookings.RemoveRange(assocBookings);
                }
                if (hotel.Rooms.Count > 0)
                {
                    _context.HotelRooms.RemoveRange(hotel.Rooms);
                }

                _context.Hotels.Remove(hotel);
                _context.SaveChanges();
            }

            return RedirectToAction("Index", "Home");
        }
       

    }
}
