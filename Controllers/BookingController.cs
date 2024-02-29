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

        // GET: Show booking confirmation form
        public async Task<IActionResult> ConfirmBooking(int? id, string type) // id for service, type for service type
        {
            if (id == null || string.IsNullOrEmpty(type))
            {
                return NotFound();
            }

            Booking booking = new();

            switch (type.ToLower())
            {
                case "flight":
                    var flight = await _context.Flights.FirstOrDefaultAsync(f => f.Id == id);
                    if (flight != null)
                    {
                        booking.ServiceId = flight.Id;
                        booking.Type = "Flight";
                    }
                    break;
                case "carrental":
                    var carRental = await _context.CarRentals.FirstOrDefaultAsync(c => c.Id == id);
                    if (carRental != null)
                    {
                        booking.ServiceId = carRental.Id;
                        booking.Type = "CarRental";
                    }
                    break;
                case "hotel":
                    var hotel = await _context.Hotels.FirstOrDefaultAsync(h => h.Id == id);
                    if (hotel != null)
                    {
                        booking.ServiceId = hotel.Id;
                        booking.Type = "Hotel";
                    }
                    break;
                default:
                    return NotFound();
            }

            if (booking.ServiceId <= 0)
            {
                return NotFound();
            }

            return View(booking);
        }

        // POST: Process booking confirmation
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

        // Other controller methods...

        private bool BookingExists(int id)
        {
            return _context.Bookings.Any(e => e.Id == id);
        }

        [HttpGet]
        public IActionResult MakeBookingClient()
        {
            return View(new Booking());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult MakeBookingClient(Booking booking)
        {
            if (ModelState.IsValid)
            {
                // Since you're not saving the details, just pass them to the confirmation view
                return RedirectToAction("BookingConfirmation", booking);
            }
            return View(booking);
        }

        public IActionResult BookingConfirmation()
        {
            return View();
        }

        public IActionResult BookedDetails()
        {
            return View();
        }
       

    }
}
