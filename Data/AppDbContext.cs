using GBC_Travel_Group23.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace GBC_Travel_Group23.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }

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
                new CarRental { Id = 10001, LocationId = 1001, Make = "Toyota", Model = "Camry", CarYear = 2020, Capacity = 5, Count = 15, Rate = 75.00 },
                new CarRental { Id = 10002, LocationId = 1002, Make = "Ford", Model = "Explorer", CarYear = 2019, Capacity = 7, Count = 10, Rate = 85.00 },
                new CarRental { Id = 10003, LocationId = 1003, Make = "Chevrolet", Model = "Suburban", CarYear = 2021, Capacity = 8, Count = 12, Rate = 90.00 },
                new CarRental { Id = 10004, LocationId = 1004, Make = "Mercedes-Benz", Model = "Sprinter", CarYear = 2020, Capacity = 12, Count = 5, Rate = 120.00 },
                new CarRental { Id = 10005, LocationId = 1005, Make = "Honda", Model = "Accord", CarYear = 2018, Capacity = 5, Count = 20, Rate = 60.00 },
                new CarRental { Id = 10006, LocationId = 1006, Make = "Jeep", Model = "Grand Cherokee", CarYear = 2022, Capacity = 5, Count = 10, Rate = 80.00 },
                new CarRental { Id = 10007, LocationId = 1007, Make = "Nissan", Model = "Armada", CarYear = 2019, Capacity = 8, Count = 8, Rate = 95.00 },
                new CarRental { Id = 10008, LocationId = 1008, Make = "Hyundai", Model = "Palisade", CarYear = 2021, Capacity = 8, Count = 12, Rate = 100.00 },
                new CarRental { Id = 10009, LocationId = 1009, Make = "BMW", Model = "X5", CarYear = 2020, Capacity = 5, Count = 7, Rate = 110.00 },
                new CarRental { Id = 10010, LocationId = 1010, Make = "Audi", Model = "Q7", CarYear = 2022, Capacity = 7, Count = 9, Rate = 130.00 },
                new CarRental { Id = 10011, LocationId = 1011, Make = "Kia", Model = "Telluride", CarYear = 2021, Capacity = 8, Count = 6, Rate = 90.00 },
                new CarRental { Id = 10012, LocationId = 1012, Make = "Lexus", Model = "LX", CarYear = 2019, Capacity = 8, Count = 4, Rate = 150.00 },
                new CarRental { Id = 10013, LocationId = 1013, Make = "Volkswagen", Model = "Atlas", CarYear = 2020, Capacity = 7, Count = 10, Rate = 85.00 },
                new CarRental { Id = 10014, LocationId = 1014, Make = "Subaru", Model = "Outback", CarYear = 2021, Capacity = 5, Count = 18, Rate = 70.00 },
                new CarRental { Id = 10015, LocationId = 1015, Make = "Tesla", Model = "Model X", CarYear = 2022, Capacity = 7, Count = 5, Rate = 200.00 },
                new CarRental { Id = 10016, LocationId = 1016, Make = "Mazda", Model = "CX-9", CarYear = 2020, Capacity = 7, Count = 12, Rate = 80.00 }
            );

            modelBuilder.Entity<Hotel>().HasData(
                new Hotel { Id = 10001, Name = "Chill Hotel", LocationId = 1001, Address = "123 Main Street", Amenities =   "Free Wi-Fi, Pool, Gym"  },
                new Hotel { Id = 10002, Name = "Comfort Inn", LocationId = 1002, Address = "456 Oak Avenue", Amenities =  "Breakfast, Parking, Pet Friendly"  },
                new Hotel { Id = 10003, Name = "Grand Resort", LocationId = 1003, Address = "789 Pine Road", Amenities =   "Spa, Restaurant, Conference Rooms"  },
                new Hotel { Id = 10004, Name = "Cityscape Suites", LocationId = 1004, Address = "101 Skyline Boulevard", Amenities =  "City View, Rooftop Bar"  },
                new Hotel { Id = 10005, Name = "Seaside Retreat", LocationId = 1005, Address = "202 Beachfront Avenue", Amenities =  "Beach Access, Ocean View"  },
                new Hotel { Id = 10006, Name = "Mountain Lodge", LocationId = 1006, Address = "303 Summit Drive", Amenities =  "Hiking Trails, Fireplace"  },
                new Hotel { Id = 10007, Name = "Urban Oasis Hotel", LocationId = 1007, Address = "404 Downtown Street", Amenities =  "Modern Decor, Lounge"  },
                new Hotel { Id = 10008, Name = "Tranquil Inn", LocationId = 1008, Address = "505 Serene Road", Amenities =   "Garden, Reading, Room"  },
                new Hotel { Id = 10009, Name = "Historic Mansion Hotel", LocationId = 1009, Address = "606 Heritage Lane", Amenities =   "Antique Furnishings, Ballroom"  },
                new Hotel { Id = 10010, Name = "Family Suites", LocationId = 1010, Address = "707 Maple Court", Amenities =  "Kid's Play Area, Family-Friendly" },
                new Hotel { Id = 10011, Name = "Executive Stay", LocationId = 1011, Address = "808 Business Plaza", Amenities =   "Business Center, Concierge"  },
                new Hotel { Id = 10012, Name = "Woodland Lodge", LocationId = 1012, Address = "909 Forest Path", Amenities =   "Nature Trails, Bird Watching"  },
                new Hotel { Id = 10013, Name = "Elegant Plaza Hotel", LocationId = 1013, Address = "1001 Grand Avenue", Amenities =   "Luxurious Spa, Fine Dining"  },
                new Hotel { Id = 10014, Name = "Riverside Inn", LocationId = 1014, Address = "1102 Waterfront Road", Amenities =   "River View, Fishing Dock" },
                new Hotel { Id = 10015, Name = "Quaint Cottage Hotel", LocationId = 1015, Address = "1203 Cozy Lane", Amenities =   "Charming Decor, Quiet Atmosphere"  },
                new Hotel { Id = 10016, Name = "Minerland Inn", LocationId = 1016, Address = "123 Cummer Road", Amenities = "Great View, Continental Breakfast" },
                new Hotel { Id = 10017, Name = "Diamond Sword Hotel", LocationId = 1017, Address = "132 Nether Lane", Amenities = "Classy Decor, Free Drinks" }
            );

            modelBuilder.Entity<HotelRoom>().HasData(
                new HotelRoom { Id = 10001, HotelId = 10001, RoomName = "1 Single", MaxOccupants = 1, Amenities = "Ensuite Bathroom, Free Wifi", RoomCount = 4, Rate = 100.00},
                new HotelRoom { Id = 10002, HotelId = 10002, RoomName = "1 Single", MaxOccupants = 1, Amenities = "Ensuite Bathroom, Free Wifi", RoomCount = 4, Rate = 120.00 },
                new HotelRoom { Id = 10003, HotelId = 10002, RoomName = "2 Double", MaxOccupants = 4, Amenities = "Ensuite Bathroom", RoomCount = 4, Rate = 180.00 },
                new HotelRoom { Id = 10004, HotelId = 10003, RoomName = "1 Queen", MaxOccupants = 2, Amenities = "Ensuite Bathroom, Free Wifi", RoomCount = 4, Rate = 140.00 },
                new HotelRoom { Id = 10005, HotelId = 10003, RoomName = "1 King", MaxOccupants = 2, Amenities = "Ensuite Bathroom", RoomCount = 4, Rate = 140.00 },
                new HotelRoom { Id = 10006, HotelId = 10003, RoomName = "2 Twin", MaxOccupants = 4, Amenities = "Ensuite Bathroom, Free Wifi", RoomCount = 4, Rate = 180.00 },
                new HotelRoom { Id = 10007, HotelId = 10004, RoomName = "1 Double, 1 Queen", MaxOccupants = 3, Amenities = "Ensuite Bathroom, Free Wifi", RoomCount = 4, Rate = 160.00 },
                new HotelRoom { Id = 10008, HotelId = 10004, RoomName = "1 King, 1 Twin", MaxOccupants = 3, Amenities = "Ensuite Bathroom, Free Wifi", RoomCount = 4, Rate = 160.00 },
                new HotelRoom { Id = 10009, HotelId = 10005, RoomName = "1 Single", MaxOccupants = 1, Amenities = "Ensuite Bathroom, Free Wifi", RoomCount = 4, Rate = 120.00 },
                new HotelRoom { Id = 10010, HotelId = 10005, RoomName = "2 Double", MaxOccupants = 4, Amenities = "Ensuite Bathroom", RoomCount = 4, Rate = 180.00 },
                new HotelRoom { Id = 10011, HotelId = 10006, RoomName = "1 Queen", MaxOccupants = 2, Amenities = "Ensuite Bathroom, Free Wifi", RoomCount = 4, Rate = 140.00 },
                new HotelRoom { Id = 10012, HotelId = 10006, RoomName = "1 King", MaxOccupants = 2, Amenities = "Ensuite Bathroom, Free Wifi", RoomCount = 4, Rate = 140.00 },
                new HotelRoom { Id = 10013, HotelId = 10006, RoomName = "2 Twin", MaxOccupants = 4, Amenities = "Ensuite Bathroom, Free Wifi", RoomCount = 4, Rate = 180.00 },
                new HotelRoom { Id = 10014, HotelId = 10007, RoomName = "1 Double, 1 Queen", MaxOccupants = 3, Amenities = "Ensuite Bathroom", RoomCount = 4, Rate = 160.00 },
                new HotelRoom { Id = 10015, HotelId = 10007, RoomName = "1 King, 1 Twin", MaxOccupants = 3, Amenities = "Ensuite Bathroom, Free Wifi", RoomCount = 4, Rate = 160.00 },
                new HotelRoom { Id = 10016, HotelId = 10008, RoomName = "1 Single", MaxOccupants = 1, Amenities = "Ensuite Bathroom", RoomCount = 4, Rate = 120.00 },
                new HotelRoom { Id = 10017, HotelId = 10008, RoomName = "2 Double", MaxOccupants = 4, Amenities = "Ensuite Bathroom, Free Wifi", RoomCount = 4, Rate = 180.00 },
                new HotelRoom { Id = 10018, HotelId = 10009, RoomName = "1 Queen", MaxOccupants = 2, Amenities = "Ensuite Bathroom", RoomCount = 4, Rate = 140.00 },
                new HotelRoom { Id = 10019, HotelId = 10009, RoomName = "1 King", MaxOccupants = 2, Amenities = "Ensuite Bathroom, Free Wifi", RoomCount = 4, Rate = 140.00 },
                new HotelRoom { Id = 10020, HotelId = 10009, RoomName = "2 Twin", MaxOccupants = 4, Amenities = "Ensuite Bathroom, Free Wifi", RoomCount = 4, Rate = 180.00 },
                new HotelRoom { Id = 10021, HotelId = 10010, RoomName = "1 Double, 1 Queen", MaxOccupants = 3, Amenities = "Ensuite Bathroom, Free Wifi", RoomCount = 4, Rate = 160.00 },
                new HotelRoom { Id = 10022, HotelId = 10011, RoomName = "1 Queen", MaxOccupants = 2, Amenities = "Ensuite Bathroom", RoomCount = 4, Rate = 150.00 },
                new HotelRoom { Id = 10023, HotelId = 10011, RoomName = "2 Twin", MaxOccupants = 4, Amenities = "Ensuite Bathroom", RoomCount = 4, Rate = 190.00 },
                new HotelRoom { Id = 10024, HotelId = 10011, RoomName = "1 King, 1 Single", MaxOccupants = 3, Amenities = "Ensuite Bathroom", RoomCount = 4, Rate = 170.00 },
                new HotelRoom { Id = 10025, HotelId = 10012, RoomName = "2 Double", MaxOccupants = 4, Amenities = "Ensuite Bathroom, Free Wifi", RoomCount = 4, Rate = 180.00 },
                new HotelRoom { Id = 10026, HotelId = 10012, RoomName = "1 King", MaxOccupants = 2, Amenities = "Ensuite Bathroom, Free Wifi", RoomCount = 4, Rate = 160.00 },
                new HotelRoom { Id = 10027, HotelId = 10012, RoomName = "1 Queen, 1 Twin", MaxOccupants = 3, Amenities = "Ensuite Bathroom, Free Wifi", RoomCount = 4, Rate = 170.00 },
                new HotelRoom { Id = 10028, HotelId = 10013, RoomName = "1 Single", MaxOccupants = 1, Amenities = "Ensuite Bathroom, Free Wifi", RoomCount = 4, Rate = 120.00 },
                new HotelRoom { Id = 10029, HotelId = 10013, RoomName = "2 Queen", MaxOccupants = 4, Amenities = "Ensuite Bathroom", RoomCount = 4, Rate = 200.00 },
                new HotelRoom { Id = 10030, HotelId = 10013, RoomName = "1 King, 1 Double", MaxOccupants = 4, Amenities = "Ensuite Bathroom, Free Wifi", RoomCount = 4, Rate = 210.00 },
                new HotelRoom { Id = 10031, HotelId = 10014, RoomName = "1 Queen", MaxOccupants = 2, Amenities = "Ensuite Bathroom, Free Wifi", RoomCount = 4, Rate = 150.00 },
                new HotelRoom { Id = 10032, HotelId = 10014, RoomName = "1 King", MaxOccupants = 2, Amenities = "Ensuite Bathroom, Free Wifi", RoomCount = 4, Rate = 160.00 },
                new HotelRoom { Id = 10033, HotelId = 10014, RoomName = "2 Twin", MaxOccupants = 4, Amenities = "Ensuite Bathroom, Free Wifi", RoomCount = 4, Rate = 190.00 },
                new HotelRoom { Id = 10034, HotelId = 10015, RoomName = "2 Double", MaxOccupants = 4, Amenities = "Ensuite Bathroom", RoomCount = 4, Rate = 180.00 },
                new HotelRoom { Id = 10035, HotelId = 10015, RoomName = "1 Queen, 1 Twin", MaxOccupants = 3, Amenities = "Ensuite Bathroom", RoomCount = 4, Rate = 170.00 },
                new HotelRoom { Id = 10036, HotelId = 10015, RoomName = "1 King, 1 Single", MaxOccupants = 3, Amenities = "Ensuite Bathroom", RoomCount = 4, Rate = 175.00 },
                new HotelRoom { Id = 10037, HotelId = 10016, RoomName = "2 Double", MaxOccupants = 4, Amenities = "WiFi, TV", RoomCount = 3, Rate = 150.0 },
                new HotelRoom { Id = 10038, HotelId = 10016, RoomName = "1 Queen, 1 Single", MaxOccupants = 3, Amenities = "WiFi, Ensuite Bathroom", RoomCount = 2, Rate = 120.0 },
                new HotelRoom { Id = 10039, HotelId = 10016, RoomName = "1 King, 1 Double", MaxOccupants = 4, Amenities = "TV, Mini Fridge", RoomCount = 5, Rate = 180.0 },
                new HotelRoom { Id = 10040, HotelId = 10017, RoomName = "3 Single", MaxOccupants = 3, Amenities = "WiFi, TV", RoomCount = 4, Rate = 130.0 },
                new HotelRoom { Id = 10041, HotelId = 10017, RoomName = "2 Queen", MaxOccupants = 4, Amenities = "Ensuite Bathroom, Mini Fridge", RoomCount = 3, Rate = 160.0 },
                new HotelRoom { Id = 10042, HotelId = 10017, RoomName = "1 Suite", MaxOccupants = 2, Amenities = "WiFi, TV, Mini Fridge, Ensuite Bathroom", RoomCount = 2, Rate = 200.0 }
                );

            modelBuilder.Entity<Flight>().HasData(
                new Flight { Id = 10001, Airline = "American Airlines", Plane = "Boeing 777", FlightCode = "AA101", DepartureLocationId = 1001, ArrivalLocationId = 1002, DepartureDate = DateTime.Now.AddDays(1), ArrivalDate = DateTime.Now.AddDays(1).AddHours(1), TotalSeats = 300, Price = 350.00 },
                new Flight { Id = 10002, Airline = "American Airlines", Plane = "Boeing 777", FlightCode = "AA102", DepartureLocationId = 1002, ArrivalLocationId = 1001, DepartureDate = DateTime.Now.AddDays(8), ArrivalDate = DateTime.Now.AddDays(8).AddHours(1), TotalSeats = 300, Price = 360.00 },
                new Flight { Id = 10003, Airline = "United Airlines", Plane = "Boeing 787", FlightCode = "UA203", DepartureLocationId = 1003, ArrivalLocationId = 1004, DepartureDate = DateTime.Now.AddDays(1), ArrivalDate = DateTime.Now.AddDays(1).AddHours(2), TotalSeats = 250, Price = 450.00 },
                new Flight { Id = 10004, Airline = "United Airlines", Plane = "Boeing 787", FlightCode = "UA204", DepartureLocationId = 1004, ArrivalLocationId = 1003, DepartureDate = DateTime.Now.AddDays(8), ArrivalDate = DateTime.Now.AddDays(8).AddHours(2), TotalSeats = 250, Price = 460.00 },
                new Flight { Id = 10005, Airline = "Delta Airlines", Plane = "Airbus A330", FlightCode = "DL305", DepartureLocationId = 1005, ArrivalLocationId = 1006, DepartureDate = DateTime.Now.AddDays(1), ArrivalDate = DateTime.Now.AddDays(1).AddHours(3), TotalSeats = 290, Price = 550.00 },
                new Flight { Id = 10006, Airline = "Delta Airlines", Plane = "Airbus A330", FlightCode = "DL306", DepartureLocationId = 1006, ArrivalLocationId = 1005, DepartureDate = DateTime.Now.AddDays(8), ArrivalDate = DateTime.Now.AddDays(8).AddHours(3), TotalSeats = 290, Price = 560.00 },
                new Flight { Id = 10007, Airline = "British Airways", Plane = "Boeing 747", FlightCode = "BA407", DepartureLocationId = 1007, ArrivalLocationId = 1008, DepartureDate = DateTime.Now.AddDays(1), ArrivalDate = DateTime.Now.AddDays(1).AddHours(10), TotalSeats = 345, Price = 800.00 },
                new Flight { Id = 10008, Airline = "British Airways", Plane = "Boeing 747", FlightCode = "BA408", DepartureLocationId = 1008, ArrivalLocationId = 1007, DepartureDate = DateTime.Now.AddDays(8), ArrivalDate = DateTime.Now.AddDays(8).AddHours(10), TotalSeats = 345, Price = 810.00 },
                new Flight { Id = 10009, Airline = "Lufthansa", Plane = "Airbus A350", FlightCode = "LH509", DepartureLocationId = 1009, ArrivalLocationId = 1010, DepartureDate = DateTime.Now.AddDays(1), ArrivalDate = DateTime.Now.AddDays(1).AddHours(11), TotalSeats = 320, Price = 850.00 },
                new Flight { Id = 10010, Airline = "Lufthansa", Plane = "Airbus A350", FlightCode = "LH510", DepartureLocationId = 1010, ArrivalLocationId = 1009, DepartureDate = DateTime.Now.AddDays(8), ArrivalDate = DateTime.Now.AddDays(8).AddHours(11), TotalSeats = 320, Price = 860.00 },
                new Flight { Id = 10011, Airline = "Emirates", Plane = "Boeing 777", FlightCode = "EM611", DepartureLocationId = 1011, ArrivalLocationId = 1012, DepartureDate = DateTime.Now.AddDays(1), ArrivalDate = DateTime.Now.AddDays(1).AddHours(5), TotalSeats = 360, Price = 700.00 },
                new Flight { Id = 10012, Airline = "Emirates", Plane = "Boeing 777", FlightCode = "EM612", DepartureLocationId = 1012, ArrivalLocationId = 1011, DepartureDate = DateTime.Now.AddDays(8), ArrivalDate = DateTime.Now.AddDays(8).AddHours(5), TotalSeats = 360, Price = 710.00 },
                new Flight { Id = 10013, Airline = "Qantas", Plane = "Boeing 787", FlightCode = "QF713", DepartureLocationId = 1013, ArrivalLocationId = 1014, DepartureDate = DateTime.Now.AddDays(1), ArrivalDate = DateTime.Now.AddDays(1).AddHours(20), TotalSeats = 236, Price = 1000.00 },
                new Flight { Id = 10014, Airline = "Qantas", Plane = "Boeing 787", FlightCode = "QF714", DepartureLocationId = 1014, ArrivalLocationId = 1013, DepartureDate = DateTime.Now.AddDays(8), ArrivalDate = DateTime.Now.AddDays(8).AddHours(20), TotalSeats = 236, Price = 1010.00 },
                new Flight { Id = 10015, Airline = "Air France", Plane = "Boeing 777", FlightCode = "AF815", DepartureLocationId = 1015, ArrivalLocationId = 1016, DepartureDate = DateTime.Now.AddDays(1), ArrivalDate = DateTime.Now.AddDays(1).AddHours(12), TotalSeats = 310, Price = 900.00 },
                new Flight { Id = 10016, Airline = "Air France", Plane = "Boeing 777", FlightCode = "AF816", DepartureLocationId = 1016, ArrivalLocationId = 1015, DepartureDate = DateTime.Now.AddDays(8), ArrivalDate = DateTime.Now.AddDays(8).AddHours(12), TotalSeats = 310, Price = 910.00 }

            );
        }
    }
}
