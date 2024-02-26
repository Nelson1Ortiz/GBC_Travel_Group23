using GBC_Travel_Group23.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace GBC_Travel_Group23.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Booking> Bookings { get; set; }
        public DbSet<CarRental> CarRentals { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<HotelRoom> HotelRooms { get; set; }
        public DbSet<Location> Locations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Flight)
                .WithMany(f => f.Bookings)
                .HasForeignKey(b => b.ServiceId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.HotelRoom)
                .WithMany(h => h.Bookings)
                .HasForeignKey(b => b.ServiceId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.CarRental)
                .WithMany(c => c.Bookings)
                .HasForeignKey(b => b.ServiceId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<HotelRoom>()
                .HasOne(hr => hr.Hotel)
                .WithMany(h => h.Rooms)
                .HasForeignKey(hr => hr.HotelId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Client)
                .WithMany(c => c.Bookings)
                .HasForeignKey(b => b.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Flight>()
                .HasOne(f => f.DepartureLocation)
                .WithMany(l => l.DepartureFlights)
                .HasForeignKey(f => f.DepartureLocationId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Flight>()
                .HasOne(f => f.ArrivalLocation)
                .WithMany(l => l.ArrivalFlights)
                .HasForeignKey(f => f.ArrivalLocationId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Hotel>()
                .HasOne(h => h.Location)
                .WithMany(l => l.Hotels)
                .HasForeignKey(h => h.LocationId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CarRental>()
                .HasOne(c => c.Location)
                .WithMany(l => l.CarRentals)
                .HasForeignKey(c => c.LocationId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Client>().HasData(
                new Client { Id = 10001, FullName = "Frederica Cottem", Email = "fcottem0@linkedin.com", Phone = "+7 (375) 654-3590" },
                new Client { Id = 10002, FullName = "Annabella Comellini", Email = "acomellini1@cnet.com", Phone = "+93 (923) 606-7690" },
                new Client { Id = 10003, FullName = "Redd Daniel", Email = "rdaniel2@sfgate.com", Phone = "+86 (422) 538-4788" },
                new Client { Id = 10004, FullName = "Tana Bister", Email = "tbister3@so-net.ne.jp", Phone = "+7 (528) 881-6250" },
                new Client { Id = 10005, FullName = "Nikaniki Boulder", Email = "nboulder4@cisco.com", Phone = "+93 (657) 895-3488" },
                new Client { Id = 10006, FullName = "Ariana Plaide", Email = "aplaide5@smh.com.au", Phone = "+57 (863) 320-2637" },
                new Client { Id = 10007, FullName = "Burr Livzey", Email = "blivzey6@businessweek.com", Phone = "+675 (218) 591-6032" },
                new Client { Id = 10008, FullName = "Waring Barrow", Email = "wbarrow7@arstechnica.com", Phone = "+86 (398) 693-2908" },
                new Client { Id = 10009, FullName = "Brade Brymham", Email = "bbrymham8@usgs.gov", Phone = "+55 (979) 581-5768" }

            );


            modelBuilder.Entity<Location>().HasData(
                new Location { Id = 1001, Country = "USA", City = "New York", Region = "NY", AirportCode = "JFK" },
                new Location { Id = 1002, Country = "Canada", City = "Toronto", Region = "ON", AirportCode = "YYZ" },
                new Location { Id = 1003, Country = "USA", City = "New York", Region = "NY", AirportCode = "JFK" },
                new Location { Id = 1004, Country = "Canada", City = "Toronto", Region = "ON", AirportCode = "YYZ" },
                new Location { Id = 1005, Country = "United Kingdom", City = "London", Region = "ENG", AirportCode = "LHR" },
                new Location { Id = 1006, Country = "Germany", City = "Berlin", Region = "BE", AirportCode = "TXL" },
                new Location { Id = 1007, Country = "Japan", City = "Tokyo", Region = "13", AirportCode = "HND" },
                new Location { Id = 1008, Country = "Australia", City = "Sydney", Region = "NSW", AirportCode = "SYD" },
                new Location { Id = 1009, Country = "Brazil", City = "Rio de Janeiro", Region = "RJ", AirportCode = "GIG" },
                new Location { Id = 1010, Country = "South Africa", City = "Cape Town", Region = "WC", AirportCode = "CPT" },
                new Location { Id = 1011, Country = "India", City = "Mumbai", Region = "MH", AirportCode = "BOM" },
                new Location { Id = 1012, Country = "China", City = "Beijing", Region = "BJ", AirportCode = "PEK" },
                new Location { Id = 1013, Country = "France", City = "Paris", Region = "IDF", AirportCode = "CDG" },
                new Location { Id = 1014, Country = "Italy", City = "Rome", Region = "RM", AirportCode = "FCO" },
                new Location { Id = 1015, Country = "Spain", City = "Barcelona", Region = "CT", AirportCode = "BCN" },
                new Location { Id = 1016, Country = "Russia", City = "Moscow", Region = "MOW", AirportCode = "SVO" },
                new Location { Id = 1017, Country = "South Korea", City = "Seoul", Region = "11", AirportCode = "ICN" }
            );

            modelBuilder.Entity<CarRental>().HasData(
                new CarRental { Id = 1, LocationId = 1001, Make = "Toyota", Model = "Camry", CarYear = 2020, Capacity = 5, Count = 15, Rate = 75.00 },
                new CarRental { Id = 2, LocationId = 1002, Make = "Ford", Model = "Explorer", CarYear = 2019, Capacity = 7, Count = 10, Rate = 85.00 },
                new CarRental { Id = 3, LocationId = 1003, Make = "Chevrolet", Model = "Suburban", CarYear = 2021, Capacity = 8, Count = 12, Rate = 90.00 },
                new CarRental { Id = 4, LocationId = 1004, Make = "Mercedes-Benz", Model = "Sprinter", CarYear = 2020, Capacity = 12, Count = 5, Rate = 120.00 },
                new CarRental { Id = 5, LocationId = 1005, Make = "Honda", Model = "Accord", CarYear = 2018, Capacity = 5, Count = 20, Rate = 60.00 },
                new CarRental { Id = 6, LocationId = 1006, Make = "Jeep", Model = "Grand Cherokee", CarYear = 2022, Capacity = 5, Count = 10, Rate = 80.00 },
                new CarRental { Id = 7, LocationId = 1007, Make = "Nissan", Model = "Armada", CarYear = 2019, Capacity = 8, Count = 8, Rate = 95.00 },
                new CarRental { Id = 8, LocationId = 1008, Make = "Hyundai", Model = "Palisade", CarYear = 2021, Capacity = 8, Count = 12, Rate = 100.00 },
                new CarRental { Id = 9, LocationId = 1009, Make = "BMW", Model = "X5", CarYear = 2020, Capacity = 5, Count = 7, Rate = 110.00 },
                new CarRental { Id = 10, LocationId = 1010, Make = "Audi", Model = "Q7", CarYear = 2022, Capacity = 7, Count = 9, Rate = 130.00 },
                new CarRental { Id = 11, LocationId = 1011, Make = "Kia", Model = "Telluride", CarYear = 2021, Capacity = 8, Count = 6, Rate = 90.00 },
                new CarRental { Id = 12, LocationId = 1012, Make = "Lexus", Model = "LX", CarYear = 2019, Capacity = 8, Count = 4, Rate = 150.00 },
                new CarRental { Id = 13, LocationId = 1013, Make = "Volkswagen", Model = "Atlas", CarYear = 2020, Capacity = 7, Count = 10, Rate = 85.00 },
                new CarRental { Id = 14, LocationId = 1014, Make = "Subaru", Model = "Outback", CarYear = 2021, Capacity = 5, Count = 18, Rate = 70.00 },
                new CarRental { Id = 15, LocationId = 1015, Make = "Tesla", Model = "Model X", CarYear = 2022, Capacity = 7, Count = 5, Rate = 200.00 },
                new CarRental { Id = 16, LocationId = 1016, Make = "Mazda", Model = "CX-9", CarYear = 2020, Capacity = 7, Count = 12, Rate = 80.00 }
            );

            modelBuilder.Entity<Hotel>().HasData(
                new Hotel { Id = 1, Name = "Luxury Hotel", LocationId = 1001, Address = "123 Main Street", Amenities =   "Free Wi-Fi, Pool, Gym"  },
                new Hotel { Id = 2, Name = "Comfort Inn", LocationId = 1002, Address = "456 Oak Avenue", Amenities =  "Breakfast, Parking, Pet Friendly"  },
                new Hotel { Id = 3, Name = "Grand Resort", LocationId = 1003, Address = "789 Pine Road", Amenities =   "Spa, Restaurant, Conference Rooms"  },
                new Hotel { Id = 4, Name = "Cityscape Suites", LocationId = 1004, Address = "101 Skyline Boulevard", Amenities =  "City View, Rooftop Bar"  },
                new Hotel { Id = 5, Name = "Seaside Retreat", LocationId = 1005, Address = "202 Beachfront Avenue", Amenities =  "Beach Access, Ocean View"  },
                new Hotel { Id = 6, Name = "Mountain Lodge", LocationId = 1006, Address = "303 Summit Drive", Amenities =  "Hiking Trails, Fireplace"  },
                new Hotel { Id = 7, Name = "Urban Oasis Hotel", LocationId = 1007, Address = "404 Downtown Street", Amenities =  "Modern Decor, Lounge"  },
                new Hotel { Id = 8, Name = "Tranquil Inn", LocationId = 1008, Address = "505 Serene Road", Amenities =   "Garden, Reading, Room"  },
                new Hotel { Id = 9, Name = "Historic Mansion Hotel", LocationId = 1009, Address = "606 Heritage Lane", Amenities =   "Antique Furnishings, Ballroom"  },
                new Hotel { Id = 10, Name = "Family Suites", LocationId = 1010, Address = "707 Maple Court", Amenities =  "Kid's Play Area, Family-Friendly" },
                new Hotel { Id = 11, Name = "Executive Stay", LocationId = 1011, Address = "808 Business Plaza", Amenities =   "Business Center, Concierge"  },
                new Hotel { Id = 12, Name = "Woodland Lodge", LocationId = 1012, Address = "909 Forest Path", Amenities =   "Nature Trails, Bird Watching"  },
                new Hotel { Id = 13, Name = "Elegant Plaza Hotel", LocationId = 1013, Address = "1001 Grand Avenue", Amenities =   "Luxurious Spa, Fine Dining"  },
                new Hotel { Id = 14, Name = "Riverside Inn", LocationId = 1014, Address = "1102 Waterfront Road", Amenities =   "River View, Fishing Dock" },
                new Hotel { Id = 15, Name = "Quaint Cottage Hotel", LocationId = 1015, Address = "1203 Cozy Lane", Amenities =   "Charming Decor, Quiet Atmosphere"  }
            );
        }

    }
}
