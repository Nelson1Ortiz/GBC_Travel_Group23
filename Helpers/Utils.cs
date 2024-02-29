using GBC_Travel_Group23.Data;
using GBC_Travel_Group23.Models;
using Microsoft.EntityFrameworkCore;
namespace GBC_Travel_Group23.Helpers
{
    public class Utils
    {
        public static Location getLocationFromString(string location, AppDbContext _context)
        {
            var parts = location.Split(", ");
            return _context.Locations
                .FirstOrDefault(l => l.City == parts[0] && l.Country == parts[1])!;
        }

        public static List<string> getAllLocationsString(AppDbContext _context)
        {
            return _context.Locations
                .Select(location => $"{location.City}, {location.Country}")
                .ToList();
        }
        public static List<string> GetHotelNames(AppDbContext _context)
        {
            return _context.Hotels
                .Select(hotel => hotel.Name)
                .ToList();
        }

        public static Hotel GetHotelFromString(string name,  AppDbContext _context)
        {
            return _context.Hotels.FirstOrDefault(h => h.Name == name)!;
        }
    }

}
