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
            //Config
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

            SeedData(modelBuilder);
       


        }
        private void SeedData(ModelBuilder modelBuilder)
        {
            List<Client> clients = new List<Client>
            {
                new Client { Id = 10001, FullName = "Frederica Cottem", Email = "fcottem0@linkedin.com", Phone = "+7 (375) 654-3590" },
                new Client { Id = 10002, FullName = "Annabella Comellini", Email = "acomellini1@cnet.com", Phone = "+93 (923) 606-7690" },
                new Client { Id = 10003, FullName = "Redd Daniel", Email = "rdaniel2@sfgate.com", Phone = "+86 (422) 538-4788" },
                new Client { Id = 10004, FullName = "Tana Bister", Email = "tbister3@so-net.ne.jp", Phone = "+7 (528) 881-6250" },
                new Client { Id = 10005, FullName = "Nikaniki Boulder", Email = "nboulder4@cisco.com", Phone = "+93 (657) 895-3488" },
                new Client { Id = 10006, FullName = "Ariana Plaide", Email = "aplaide5@smh.com.au", Phone = "+57 (863) 320-2637" },
                new Client { Id = 10007, FullName = "Burr Livzey", Email = "blivzey6@businessweek.com", Phone = "+675 (218) 591-6032" },
                new Client { Id = 10008, FullName = "Waring Barrow", Email = "wbarrow7@arstechnica.com", Phone = "+86 (398) 693-2908" },
                new Client { Id = 10009, FullName = "Brade Brymham", Email = "bbrymham8@usgs.gov", Phone = "+55 (979) 581-5768" }
            };

            List<Location> locations = new List<Location>
            {
                new Location { Id = 1001, Country = "USA", City = "New York", Region = "NY", AirportCode = "JFK" },
                new Location { Id = 1002, Country = "Canada", City = "Toronto", Region = "ON", AirportCode = "YYZ" },
                new Location { Id = 1003, Country = "United Kingdom", City = "London", Region = "ENG", AirportCode = "LHR" },
                new Location { Id = 1004, Country = "Germany", City = "Berlin", Region = "BE", AirportCode = "TXL" },
                new Location { Id = 1005, Country = "Japan", City = "Tokyo", Region = "13", AirportCode = "HND" },
                new Location { Id = 1006, Country = "Australia", City = "Sydney", Region = "NSW", AirportCode = "SYD" },
                new Location { Id = 1007, Country = "Brazil", City = "Rio de Janeiro", Region = "RJ", AirportCode = "GIG" },
                new Location { Id = 1008, Country = "South Africa", City = "Cape Town", Region = "WC", AirportCode = "CPT" },
                new Location { Id = 1009, Country = "India", City = "Mumbai", Region = "MH", AirportCode = "BOM" },
                new Location { Id = 1010, Country = "China", City = "Beijing", Region = "BJ", AirportCode = "PEK" },
                new Location { Id = 1011, Country = "France", City = "Paris", Region = "IDF", AirportCode = "CDG" },
                new Location { Id = 1012, Country = "Italy", City = "Rome", Region = "RM", AirportCode = "FCO" },
                new Location { Id = 1013, Country = "Spain", City = "Barcelona", Region = "CT", AirportCode = "BCN" },
                new Location { Id = 1014, Country = "Russia", City = "Moscow", Region = "MOW", AirportCode = "SVO" },
                new Location { Id = 1015, Country = "South Korea", City = "Seoul", Region = "11", AirportCode = "ICN" }
            };

            List<Hotel> hotels = GenerateHotels(locations);

            List<CarRental> carRentals = GenerateCarRentals(locations);

            modelBuilder.Entity<Client>().HasData(clients);

            modelBuilder.Entity<Location>().HasData(locations);

            modelBuilder.Entity<CarRental>().HasData(carRentals);

            modelBuilder.Entity<Hotel>().HasData(hotels);
        }

        public List<CarRental> GenerateCarRentals(List<Location> locations)
        {
            string[,] seedCars = {
                { "Toyota", "Camry", "2020", "5" },
                { "Ford", "Explorer", "2019", "7" },
                { "Chevrolet", "Suburban", "2021", "8" },
                { "Mercedes-Benz", "Sprinter", "2020", "12" },
                { "Honda", "Accord", "2018", "5" },
                { "Jeep", "Grand Cherokee", "2022", "5" },
                { "Nissan", "Armada", "2019", "8" },
                { "Hyundai", "Palisade", "2021", "8" },
                { "BMW", "X5", "2020", "5" },
                { "Audi", "Q7", "2022", "7" },
                { "Kia", "Telluride", "2021", "8" },
                { "Lexus", "LX", "2019", "8" },  
                { "Volkswagen", "Atlas", "2020", "7" },
                { "Subaru", "Outback", "2021", "5" },
                { "Tesla", "Model X", "2022", "7" },
                { "Mazda", "CX-9", "2020", "7" }
            };
            Random random = new Random();
            List<CarRental> carRentals = new List<CarRental>();
            int nextId = 10000;

            foreach (Location location in locations)
            {
                for (int i = 0; i < 3; i++)
                {
                    int randCarInx = random.Next(seedCars.GetLength(0));
                    int count = random.Next(1, 31);
                    double rate = random.NextDouble() * (300.0 - 60.0) + 60.0;

                    CarRentals.Add( new CarRental
                    {
                        Id = nextId++,
                        LocationId = location.Id,
                        Make = seedCars[randCarInx, 0],
                        Model = seedCars[randCarInx, 1],
                        CarYear = int.Parse(seedCars[randCarInx, 2]),
                        Capacity = int.Parse(seedCars[randCarInx, 3]),
                        Count = count,
                        Rate = rate
                    });
                }
            }
            return carRentals;
        }

        public List<Hotel> GenerateHotels(List<Location> locations)
        {
            List<string[]> seedHotels = new List<string[]>
            {
                new string[] { "Luxury Hotel", "123 Main Street", "Free Wi-Fi, Pool, Gym" },
                new string[] { "Comfort Inn", "456 Oak Avenue", "Breakfast, Parking, Pet Friendly" },
                new string[] { "Grand Resort", "789 Pine Road", "Spa, Restaurant, Conference Rooms" },
                new string[] { "Cityscape Suites", "101 Skyline Boulevard", "City View, Rooftop Bar" },
                new string[] { "Seaside Retreat", "202 Beachfront Avenue", "Beach Access, Ocean View" },
                new string[] { "Mountain Lodge", "303 Summit Drive", "Hiking Trails, Fireplace" },
                new string[] { "Urban Oasis Hotel", "404 Downtown Street", "Modern Decor, Lounge" },
                new string[] { "Tranquil Inn", "505 Serene Road", "Garden, Reading Room" },
                new string[] { "Historic Mansion Hotel", "606 Heritage Lane", "Antique Furnishings, Ballroom" },
                new string[] { "Family Suites", "707 Maple Court", "Kid's Play Area, Family-Friendly" },
                new string[] { "Executive Stay", "808 Business Plaza", "Business Center, Concierge" },
                new string[] { "Woodland Lodge", "909 Forest Path", "Nature Trails, Bird Watching" },
                new string[] { "Elegant Plaza Hotel", "1001 Grand Avenue", "Luxurious Spa, Fine Dining" },
                new string[] { "Riverside Inn", "1102 Waterfront Road", "River View, Fishing Dock" },
                new string[] { "Quaint Cottage Hotel", "1203 Cozy Lane", "Charming Decor, Quiet Atmosphere" },
            };
            Random random = new Random();
            List<Hotel> Hotels = new List<Hotel>();
            int nextId = 10000;
            foreach (Location location in locations)
            {
                for (int i = 0; i < 2; i++)
                {
                    string[] randInfo = seedHotels[random.Next(seedHotels.Count)];
                    Hotels.Add(new Hotel
                    {
                        Id = nextId++,
                        Name = randInfo[0],
                        LocationId = location.Id,
                        Address = randInfo[1],
                        Amenities = randInfo[2].Split(", ")
                    });
                }
            }
            return Hotels;
        }
    }
}
