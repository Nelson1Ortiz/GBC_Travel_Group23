﻿using Microsoft.AspNetCore.Mvc;
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





    }
}
