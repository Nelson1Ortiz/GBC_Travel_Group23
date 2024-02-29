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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmBooking(Booking booking)
        {
            if (ModelState.IsValid)
            {
                _context.Add(booking);
                await _context.SaveChangesAsync();
                return RedirectToAction("ThankYou", new { id = booking.Id });
            }
            return View(booking);
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
                ViewBag.Service = carRental;
            } else
            {
                HotelRoom hotelRoom = _context.HotelRooms.FirstOrDefault(hr => hr.Id == id)!;
                ViewBag.Service = hotelRoom;
            }
            return View(new Booking());
        }

        public IActionResult BookedDetails()
        {
            return View();
        }
       

    }
}
