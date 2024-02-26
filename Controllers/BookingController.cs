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

        // Existing methods ...

        // GET: Show booking confirmation form
        public async Task<IActionResult> ConfirmBooking(int id) // Using 'id' to match your convention
        {
            // Assuming 'id' could be for any service type
            var flight = await _context.Flights.FirstOrDefaultAsync(f => f.Id == id);
            var carRental = flight == null ? await _context.CarRentals.FirstOrDefaultAsync(c => c.Id == id) : null;
            var hotel = (flight == null && carRental == null) ? await _context.Hotels.FirstOrDefaultAsync(h => h.Id == id) : null;

            // If no services are found with the given ID, return NotFound
            if (flight == null && carRental == null && hotel == null)
            {
                return NotFound();
            }

            // Create a new Booking model, and populate it with the found service
            var model = new Booking
            {
                // Populate with common properties if any
            };

            // Add additional service-specific details to the booking model if needed
            if (flight != null)
            {
                // Populate booking model with flight-specific details
            }
            else if (carRental != null)
            {
                // Populate booking model with car rental-specific details
            }
            else if (hotel != null)
            {
                // Populate booking model with hotel-specific details
            }

            return View(model);
        }


        // POST: Process booking confirmation
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmBooking(Booking model)
        {
            if (ModelState.IsValid)
            {
                // Convert BookingViewModel to your Booking entity
                var booking = new Booking
                {
                    // Map properties from BookingViewModel to Booking entity
                    ClientId = model.ClientId,
                    ServiceId = model.Id,
                    // Include other properties such as GuestCount, StartDate, EndDate
                };

                _context.Add(booking);
                await _context.SaveChangesAsync();

                // Redirect to a confirmation/thank you page, assuming such an action exists
                return RedirectToAction("ThankYou", new { id = booking.Id }); // Adjust "ThankYou" to your actual confirmation view/action
            }

            // If we got this far, something failed; re-display form
            return View(model);
        }

        // Other methods...

        private bool BookingExists(int id)
        {
            return _context.Bookings.Any(e => e.Id == id);
        }
    }
}
