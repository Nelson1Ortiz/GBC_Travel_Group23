using GBC_Travel_Group23.Data;
using GBC_Travel_Group23.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GBC_Travel_Group23.Controllers
{
    public class BookingController : Controller
    {
        private readonly AppDbContext _context;

        public BookingController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult CreateBooking(string type, int id, DateTime startDate, DateTime endDate)
        {
            ViewBag.StartDate = startDate;
            ViewBag.EndDate = endDate;
            ViewBag.Type = type;
            if (type == "flight")
            {
                Flight flight = _context.Flights
                    .Include(f => f.DepartureLocation)
                    .Include(f => f.ArrivalLocation)
                    .FirstOrDefault(f => f.Id == id)!;
                ViewBag.Service = flight; 
                ViewBag.AvailableSeats = flight.TotalSeats - _context.Bookings.Count(b => b.ServiceId == flight.Id);
            } else if (type == "car")
            {
                CarRental carRental = _context.CarRentals.FirstOrDefault(c => c.Id == id)!;
                ViewBag.Total = (endDate - startDate).Days * carRental.Rate;
                ViewBag.Service = carRental;
            } else
            {
                HotelRoom hotelRoom = _context.HotelRooms
                    .Include(hr => hr.Hotel)
                    .FirstOrDefault(hr => hr.Id == id)!;
                ViewBag.Total = (endDate - startDate).Days * hotelRoom.Rate;
                ViewBag.Service = hotelRoom;
            }
            return View(new Booking());
        }

        [HttpPost]
        public IActionResult ConfirmBooking(Booking booking)
        {
            if (booking.Type == "flight")
            {
                booking.Flight = _context.Flights.Find(booking.ServiceId)!;
                booking.Flight.ArrivalLocation = _context.Locations.Find(booking.Flight.ArrivalLocationId)!;
                booking.Flight.DepartureLocation = _context.Locations.Find(booking.Flight.DepartureLocationId)!;
            }
            else if (booking.Type == "car")
            {
                booking.CarRental = _context.CarRentals.Find(booking.ServiceId)!;
                booking.CarRental.Location = _context.Locations.Find(booking.CarRental.LocationId)!;
                ViewBag.Total = (booking.EndDate - booking.StartDate).Days * booking.CarRental!.Rate;
            }
            else
            {
                booking.HotelRoom = _context.HotelRooms.Find(booking.ServiceId)!;
                booking.HotelRoom.Hotel = _context.Hotels.Find(booking.HotelRoom.HotelId)!;
                booking.HotelRoom.Hotel.Location = _context.Locations.Find(booking.HotelRoom.Hotel.LocationId)!;
                ViewBag.Total = (booking.EndDate - booking.StartDate).Days * booking.HotelRoom!.Rate;
            }
            
            return View(booking);
        }

        [HttpPost]
        public IActionResult CompleteBooking(Booking booking)
        {
            
                var client = _context.Clients.FirstOrDefault(c => c.Email == booking.Client.Email);
				if (client is Client)
                {
                    booking.ClientId = client.Id;
                    booking.Client = client;
                } else
                {
                    _context.Clients.Add(booking.Client);
                }

                _context.Add(booking);
                _context.SaveChanges();
            return RedirectToAction("BookingsDetails", "Booking", new { clientEmail = booking.Client.Email});
        }

        [HttpGet]
        public IActionResult BookingsDetails(string clientEmail)
        {
            List<Booking> bookings = _context.Bookings
                .Include(b => b.Flight)
                    .ThenInclude(f => f.ArrivalLocation)
                .Include(b => b.Flight)
                    .ThenInclude(f => f.DepartureLocation)
                .Include(b => b.HotelRoom)
                    .ThenInclude(hr => hr.Hotel)
                        .ThenInclude(h => h.Location)
                .Include(b => b.CarRental)
                    .ThenInclude(cr => cr.Location)
                .Include(b => b.Client)
                .Where(b => b.Client.Email == clientEmail)
                .ToList();
            return View(bookings);
        }
       

    }
}
