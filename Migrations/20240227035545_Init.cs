using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GBC_Travel_Group23.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Region = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AirportCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CarRentals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocationId = table.Column<int>(type: "int", nullable: false),
                    Make = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CarYear = table.Column<int>(type: "int", nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    Rate = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarRentals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarRentals_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Flights",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Airline = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Plane = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FlightCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepartureLocationId = table.Column<int>(type: "int", nullable: false),
                    ArrivalLocationId = table.Column<int>(type: "int", nullable: false),
                    DepartureDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ArrivalDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalSeats = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flights", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Flights_Locations_ArrivalLocationId",
                        column: x => x.ArrivalLocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Flights_Locations_DepartureLocationId",
                        column: x => x.DepartureLocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Hotels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LocationId = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amenities = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hotels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hotels_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HotelRooms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HotelId = table.Column<int>(type: "int", nullable: false),
                    RoomName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaxOccupants = table.Column<int>(type: "int", nullable: false),
                    Amenities = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoomCount = table.Column<int>(type: "int", nullable: false),
                    Rate = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotelRooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HotelRooms_Hotels_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Hotels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ServiceId = table.Column<int>(type: "int", nullable: false),
                    GuestCount = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bookings_CarRentals_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "CarRentals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bookings_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bookings_Flights_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Flights",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bookings_HotelRooms_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "HotelRooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "Email", "FullName", "Phone" },
                values: new object[,]
                {
                    { 10001, "fcottem0@linkedin.com", "Frederica Cottem", "+7 (375) 654-3590" },
                    { 10002, "acomellini1@cnet.com", "Annabella Comellini", "+93 (923) 606-7690" },
                    { 10003, "rdaniel2@sfgate.com", "Redd Daniel", "+86 (422) 538-4788" },
                    { 10004, "tbister3@so-net.ne.jp", "Tana Bister", "+7 (528) 881-6250" },
                    { 10005, "nboulder4@cisco.com", "Nikaniki Boulder", "+93 (657) 895-3488" },
                    { 10006, "aplaide5@smh.com.au", "Ariana Plaide", "+57 (863) 320-2637" },
                    { 10007, "blivzey6@businessweek.com", "Burr Livzey", "+675 (218) 591-6032" },
                    { 10008, "wbarrow7@arstechnica.com", "Waring Barrow", "+86 (398) 693-2908" },
                    { 10009, "bbrymham8@usgs.gov", "Brade Brymham", "+55 (979) 581-5768" }
                });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "Id", "AirportCode", "City", "Country", "Region" },
                values: new object[,]
                {
                    { 1001, "JFK", "New York", "USA", "NY" },
                    { 1002, "YYZ", "Toronto", "Canada", "ON" },
                    { 1003, "JFK", "New York", "USA", "NY" },
                    { 1004, "YYZ", "Toronto", "Canada", "ON" },
                    { 1005, "LHR", "London", "United Kingdom", "ENG" },
                    { 1006, "TXL", "Berlin", "Germany", "BE" },
                    { 1007, "HND", "Tokyo", "Japan", "13" },
                    { 1008, "SYD", "Sydney", "Australia", "NSW" },
                    { 1009, "GIG", "Rio de Janeiro", "Brazil", "RJ" },
                    { 1010, "CPT", "Cape Town", "South Africa", "WC" },
                    { 1011, "BOM", "Mumbai", "India", "MH" },
                    { 1012, "PEK", "Beijing", "China", "BJ" },
                    { 1013, "CDG", "Paris", "France", "IDF" },
                    { 1014, "FCO", "Rome", "Italy", "RM" },
                    { 1015, "BCN", "Barcelona", "Spain", "CT" },
                    { 1016, "SVO", "Moscow", "Russia", "MOW" },
                    { 1017, "ICN", "Seoul", "South Korea", "11" }
                });

            migrationBuilder.InsertData(
                table: "CarRentals",
                columns: new[] { "Id", "Capacity", "CarYear", "Count", "LocationId", "Make", "Model", "Rate" },
                values: new object[,]
                {
                    { 10001, 5, 2020, 15, 1001, "Toyota", "Camry", 75.0 },
                    { 10002, 7, 2019, 10, 1002, "Ford", "Explorer", 85.0 },
                    { 10003, 8, 2021, 12, 1003, "Chevrolet", "Suburban", 90.0 },
                    { 10004, 12, 2020, 5, 1004, "Mercedes-Benz", "Sprinter", 120.0 },
                    { 10005, 5, 2018, 20, 1005, "Honda", "Accord", 60.0 },
                    { 10006, 5, 2022, 10, 1006, "Jeep", "Grand Cherokee", 80.0 },
                    { 10007, 8, 2019, 8, 1007, "Nissan", "Armada", 95.0 },
                    { 10008, 8, 2021, 12, 1008, "Hyundai", "Palisade", 100.0 },
                    { 10009, 5, 2020, 7, 1009, "BMW", "X5", 110.0 },
                    { 10010, 7, 2022, 9, 1010, "Audi", "Q7", 130.0 },
                    { 10011, 8, 2021, 6, 1011, "Kia", "Telluride", 90.0 },
                    { 10012, 8, 2019, 4, 1012, "Lexus", "LX", 150.0 },
                    { 10013, 7, 2020, 10, 1013, "Volkswagen", "Atlas", 85.0 },
                    { 10014, 5, 2021, 18, 1014, "Subaru", "Outback", 70.0 },
                    { 10015, 7, 2022, 5, 1015, "Tesla", "Model X", 200.0 },
                    { 10016, 7, 2020, 12, 1016, "Mazda", "CX-9", 80.0 }
                });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "Id", "Airline", "ArrivalDate", "ArrivalLocationId", "DepartureDate", "DepartureLocationId", "FlightCode", "Plane", "Price", "TotalSeats" },
                values: new object[,]
                {
                    { 10001, "American Airlines", new DateTime(2024, 2, 27, 23, 55, 44, 498, DateTimeKind.Local).AddTicks(2710), 1002, new DateTime(2024, 2, 27, 22, 55, 44, 498, DateTimeKind.Local).AddTicks(2669), 1001, "AA101", "Boeing 777", 350.0, 300 },
                    { 10002, "American Airlines", new DateTime(2024, 3, 5, 23, 55, 44, 498, DateTimeKind.Local).AddTicks(2714), 1001, new DateTime(2024, 3, 5, 22, 55, 44, 498, DateTimeKind.Local).AddTicks(2713), 1002, "AA102", "Boeing 777", 360.0, 300 },
                    { 10003, "United Airlines", new DateTime(2024, 2, 28, 0, 55, 44, 498, DateTimeKind.Local).AddTicks(2719), 1004, new DateTime(2024, 2, 27, 22, 55, 44, 498, DateTimeKind.Local).AddTicks(2717), 1003, "UA203", "Boeing 787", 450.0, 250 },
                    { 10004, "United Airlines", new DateTime(2024, 3, 6, 0, 55, 44, 498, DateTimeKind.Local).AddTicks(2723), 1003, new DateTime(2024, 3, 5, 22, 55, 44, 498, DateTimeKind.Local).AddTicks(2721), 1004, "UA204", "Boeing 787", 460.0, 250 },
                    { 10005, "Delta Airlines", new DateTime(2024, 2, 28, 1, 55, 44, 498, DateTimeKind.Local).AddTicks(2727), 1006, new DateTime(2024, 2, 27, 22, 55, 44, 498, DateTimeKind.Local).AddTicks(2725), 1005, "DL305", "Airbus A330", 550.0, 290 },
                    { 10006, "Delta Airlines", new DateTime(2024, 3, 6, 1, 55, 44, 498, DateTimeKind.Local).AddTicks(2731), 1005, new DateTime(2024, 3, 5, 22, 55, 44, 498, DateTimeKind.Local).AddTicks(2730), 1006, "DL306", "Airbus A330", 560.0, 290 },
                    { 10007, "British Airways", new DateTime(2024, 2, 28, 8, 55, 44, 498, DateTimeKind.Local).AddTicks(2735), 1008, new DateTime(2024, 2, 27, 22, 55, 44, 498, DateTimeKind.Local).AddTicks(2734), 1007, "BA407", "Boeing 747", 800.0, 345 },
                    { 10008, "British Airways", new DateTime(2024, 3, 6, 8, 55, 44, 498, DateTimeKind.Local).AddTicks(2740), 1007, new DateTime(2024, 3, 5, 22, 55, 44, 498, DateTimeKind.Local).AddTicks(2738), 1008, "BA408", "Boeing 747", 810.0, 345 },
                    { 10009, "Lufthansa", new DateTime(2024, 2, 28, 9, 55, 44, 498, DateTimeKind.Local).AddTicks(2744), 1010, new DateTime(2024, 2, 27, 22, 55, 44, 498, DateTimeKind.Local).AddTicks(2742), 1009, "LH509", "Airbus A350", 850.0, 320 },
                    { 10010, "Lufthansa", new DateTime(2024, 3, 6, 9, 55, 44, 498, DateTimeKind.Local).AddTicks(2748), 1009, new DateTime(2024, 3, 5, 22, 55, 44, 498, DateTimeKind.Local).AddTicks(2746), 1010, "LH510", "Airbus A350", 860.0, 320 },
                    { 10011, "Emirates", new DateTime(2024, 2, 28, 3, 55, 44, 498, DateTimeKind.Local).AddTicks(2752), 1012, new DateTime(2024, 2, 27, 22, 55, 44, 498, DateTimeKind.Local).AddTicks(2751), 1011, "EM611", "Boeing 777", 700.0, 360 },
                    { 10012, "Emirates", new DateTime(2024, 3, 6, 3, 55, 44, 498, DateTimeKind.Local).AddTicks(2756), 1011, new DateTime(2024, 3, 5, 22, 55, 44, 498, DateTimeKind.Local).AddTicks(2755), 1012, "EM612", "Boeing 777", 710.0, 360 },
                    { 10013, "Qantas", new DateTime(2024, 2, 28, 18, 55, 44, 498, DateTimeKind.Local).AddTicks(2761), 1014, new DateTime(2024, 2, 27, 22, 55, 44, 498, DateTimeKind.Local).AddTicks(2759), 1013, "QF713", "Boeing 787", 1000.0, 236 },
                    { 10014, "Qantas", new DateTime(2024, 3, 6, 18, 55, 44, 498, DateTimeKind.Local).AddTicks(2765), 1013, new DateTime(2024, 3, 5, 22, 55, 44, 498, DateTimeKind.Local).AddTicks(2763), 1014, "QF714", "Boeing 787", 1010.0, 236 },
                    { 10015, "Air France", new DateTime(2024, 2, 28, 10, 55, 44, 498, DateTimeKind.Local).AddTicks(2769), 1016, new DateTime(2024, 2, 27, 22, 55, 44, 498, DateTimeKind.Local).AddTicks(2767), 1015, "AF815", "Boeing 777", 900.0, 310 },
                    { 10016, "Air France", new DateTime(2024, 3, 6, 10, 55, 44, 498, DateTimeKind.Local).AddTicks(2773), 1015, new DateTime(2024, 3, 5, 22, 55, 44, 498, DateTimeKind.Local).AddTicks(2771), 1016, "AF816", "Boeing 777", 910.0, 310 }
                });

            migrationBuilder.InsertData(
                table: "Hotels",
                columns: new[] { "Id", "Address", "Amenities", "LocationId", "Name" },
                values: new object[,]
                {
                    { 10001, "123 Main Street", "Free Wi-Fi, Pool, Gym", 1001, "Chill Hotel" },
                    { 10002, "456 Oak Avenue", "Breakfast, Parking, Pet Friendly", 1002, "Comfort Inn" },
                    { 10003, "789 Pine Road", "Spa, Restaurant, Conference Rooms", 1003, "Grand Resort" },
                    { 10004, "101 Skyline Boulevard", "City View, Rooftop Bar", 1004, "Cityscape Suites" },
                    { 10005, "202 Beachfront Avenue", "Beach Access, Ocean View", 1005, "Seaside Retreat" },
                    { 10006, "303 Summit Drive", "Hiking Trails, Fireplace", 1006, "Mountain Lodge" },
                    { 10007, "404 Downtown Street", "Modern Decor, Lounge", 1007, "Urban Oasis Hotel" },
                    { 10008, "505 Serene Road", "Garden, Reading, Room", 1008, "Tranquil Inn" },
                    { 10009, "606 Heritage Lane", "Antique Furnishings, Ballroom", 1009, "Historic Mansion Hotel" },
                    { 10010, "707 Maple Court", "Kid's Play Area, Family-Friendly", 1010, "Family Suites" },
                    { 10011, "808 Business Plaza", "Business Center, Concierge", 1011, "Executive Stay" },
                    { 10012, "909 Forest Path", "Nature Trails, Bird Watching", 1012, "Woodland Lodge" },
                    { 10013, "1001 Grand Avenue", "Luxurious Spa, Fine Dining", 1013, "Elegant Plaza Hotel" },
                    { 10014, "1102 Waterfront Road", "River View, Fishing Dock", 1014, "Riverside Inn" },
                    { 10015, "1203 Cozy Lane", "Charming Decor, Quiet Atmosphere", 1015, "Quaint Cottage Hotel" },
                    { 10016, "123 Cummer Road", "Great View, Continental Breakfast", 1016, "Minerland Inn" },
                    { 10017, "132 Nether Lane", "Classy Decor, Free Drinks", 1017, "Diamond Sword Hotel" }
                });

            migrationBuilder.InsertData(
                table: "HotelRooms",
                columns: new[] { "Id", "Amenities", "HotelId", "MaxOccupants", "Rate", "RoomCount", "RoomName" },
                values: new object[,]
                {
                    { 10001, "Ensuite Bathroom, Free Wifi", 10001, 1, 100.0, 4, "1 Single" },
                    { 10002, "Ensuite Bathroom, Free Wifi", 10002, 1, 120.0, 4, "1 Single" },
                    { 10003, "Ensuite Bathroom", 10002, 4, 180.0, 4, "2 Double" },
                    { 10004, "Ensuite Bathroom, Free Wifi", 10003, 2, 140.0, 4, "1 Queen" },
                    { 10005, "Ensuite Bathroom", 10003, 2, 140.0, 4, "1 King" },
                    { 10006, "Ensuite Bathroom, Free Wifi", 10003, 4, 180.0, 4, "2 Twin" },
                    { 10007, "Ensuite Bathroom, Free Wifi", 10004, 3, 160.0, 4, "1 Double, 1 Queen" },
                    { 10008, "Ensuite Bathroom, Free Wifi", 10004, 3, 160.0, 4, "1 King, 1 Twin" },
                    { 10009, "Ensuite Bathroom, Free Wifi", 10005, 1, 120.0, 4, "1 Single" },
                    { 10010, "Ensuite Bathroom", 10005, 4, 180.0, 4, "2 Double" },
                    { 10011, "Ensuite Bathroom, Free Wifi", 10006, 2, 140.0, 4, "1 Queen" },
                    { 10012, "Ensuite Bathroom, Free Wifi", 10006, 2, 140.0, 4, "1 King" },
                    { 10013, "Ensuite Bathroom, Free Wifi", 10006, 4, 180.0, 4, "2 Twin" },
                    { 10014, "Ensuite Bathroom", 10007, 3, 160.0, 4, "1 Double, 1 Queen" },
                    { 10015, "Ensuite Bathroom, Free Wifi", 10007, 3, 160.0, 4, "1 King, 1 Twin" },
                    { 10016, "Ensuite Bathroom", 10008, 1, 120.0, 4, "1 Single" },
                    { 10017, "Ensuite Bathroom, Free Wifi", 10008, 4, 180.0, 4, "2 Double" },
                    { 10018, "Ensuite Bathroom", 10009, 2, 140.0, 4, "1 Queen" },
                    { 10019, "Ensuite Bathroom, Free Wifi", 10009, 2, 140.0, 4, "1 King" },
                    { 10020, "Ensuite Bathroom, Free Wifi", 10009, 4, 180.0, 4, "2 Twin" },
                    { 10021, "Ensuite Bathroom, Free Wifi", 10010, 3, 160.0, 4, "1 Double, 1 Queen" },
                    { 10022, "Ensuite Bathroom", 10011, 2, 150.0, 4, "1 Queen" },
                    { 10023, "Ensuite Bathroom", 10011, 4, 190.0, 4, "2 Twin" },
                    { 10024, "Ensuite Bathroom", 10011, 3, 170.0, 4, "1 King, 1 Single" },
                    { 10025, "Ensuite Bathroom, Free Wifi", 10012, 4, 180.0, 4, "2 Double" },
                    { 10026, "Ensuite Bathroom, Free Wifi", 10012, 2, 160.0, 4, "1 King" },
                    { 10027, "Ensuite Bathroom, Free Wifi", 10012, 3, 170.0, 4, "1 Queen, 1 Twin" },
                    { 10028, "Ensuite Bathroom, Free Wifi", 10013, 1, 120.0, 4, "1 Single" },
                    { 10029, "Ensuite Bathroom", 10013, 4, 200.0, 4, "2 Queen" },
                    { 10030, "Ensuite Bathroom, Free Wifi", 10013, 4, 210.0, 4, "1 King, 1 Double" },
                    { 10031, "Ensuite Bathroom, Free Wifi", 10014, 2, 150.0, 4, "1 Queen" },
                    { 10032, "Ensuite Bathroom, Free Wifi", 10014, 2, 160.0, 4, "1 King" },
                    { 10033, "Ensuite Bathroom, Free Wifi", 10014, 4, 190.0, 4, "2 Twin" },
                    { 10034, "Ensuite Bathroom", 10015, 4, 180.0, 4, "2 Double" },
                    { 10035, "Ensuite Bathroom", 10015, 3, 170.0, 4, "1 Queen, 1 Twin" },
                    { 10036, "Ensuite Bathroom", 10015, 3, 175.0, 4, "1 King, 1 Single" },
                    { 10037, "WiFi, TV", 10016, 4, 150.0, 3, "2 Double" },
                    { 10038, "WiFi, Ensuite Bathroom", 10016, 3, 120.0, 2, "1 Queen, 1 Single" },
                    { 10039, "TV, Mini Fridge", 10016, 4, 180.0, 5, "1 King, 1 Double" },
                    { 10040, "WiFi, TV", 10017, 3, 130.0, 4, "3 Single" },
                    { 10041, "Ensuite Bathroom, Mini Fridge", 10017, 4, 160.0, 3, "2 Queen" },
                    { 10042, "WiFi, TV, Mini Fridge, Ensuite Bathroom", 10017, 2, 200.0, 2, "1 Suite" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_ClientId",
                table: "Bookings",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_ServiceId",
                table: "Bookings",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_CarRentals_LocationId",
                table: "CarRentals",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Flights_ArrivalLocationId",
                table: "Flights",
                column: "ArrivalLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Flights_DepartureLocationId",
                table: "Flights",
                column: "DepartureLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_HotelRooms_HotelId",
                table: "HotelRooms",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_Hotels_LocationId",
                table: "Hotels",
                column: "LocationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "CarRentals");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Flights");

            migrationBuilder.DropTable(
                name: "HotelRooms");

            migrationBuilder.DropTable(
                name: "Hotels");

            migrationBuilder.DropTable(
                name: "Locations");
        }
    }
}
