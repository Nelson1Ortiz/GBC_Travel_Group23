using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GBC_Travel_Group23.Migrations
{
    /// <inheritdoc />
    public partial class Update_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    { 1, 5, 2020, 15, 1001, "Toyota", "Camry", 75.0 },
                    { 2, 7, 2019, 10, 1002, "Ford", "Explorer", 85.0 },
                    { 3, 8, 2021, 12, 1003, "Chevrolet", "Suburban", 90.0 },
                    { 4, 12, 2020, 5, 1004, "Mercedes-Benz", "Sprinter", 120.0 },
                    { 5, 5, 2018, 20, 1005, "Honda", "Accord", 60.0 },
                    { 6, 5, 2022, 10, 1006, "Jeep", "Grand Cherokee", 80.0 },
                    { 7, 8, 2019, 8, 1007, "Nissan", "Armada", 95.0 },
                    { 8, 8, 2021, 12, 1008, "Hyundai", "Palisade", 100.0 },
                    { 9, 5, 2020, 7, 1009, "BMW", "X5", 110.0 },
                    { 10, 7, 2022, 9, 1010, "Audi", "Q7", 130.0 },
                    { 11, 8, 2021, 6, 1011, "Kia", "Telluride", 90.0 },
                    { 12, 8, 2019, 4, 1012, "Lexus", "LX", 150.0 },
                    { 13, 7, 2020, 10, 1013, "Volkswagen", "Atlas", 85.0 },
                    { 14, 5, 2021, 18, 1014, "Subaru", "Outback", 70.0 },
                    { 15, 7, 2022, 5, 1015, "Tesla", "Model X", 200.0 },
                    { 16, 7, 2020, 12, 1016, "Mazda", "CX-9", 80.0 }
                });

            migrationBuilder.InsertData(
                table: "Hotels",
                columns: new[] { "Id", "Address", "Amenities", "LocationId", "Name" },
                values: new object[,]
                {
                    { 1, "123 Main Street", "Free Wi-Fi, Pool, Gym", 1001, "Luxury Hotel" },
                    { 2, "456 Oak Avenue", "Breakfast, Parking, Pet Friendly", 1002, "Comfort Inn" },
                    { 3, "789 Pine Road", "Spa, Restaurant, Conference Rooms", 1003, "Grand Resort" },
                    { 4, "101 Skyline Boulevard", "City View, Rooftop Bar", 1004, "Cityscape Suites" },
                    { 5, "202 Beachfront Avenue", "Beach Access, Ocean View", 1005, "Seaside Retreat" },
                    { 6, "303 Summit Drive", "Hiking Trails, Fireplace", 1006, "Mountain Lodge" },
                    { 7, "404 Downtown Street", "Modern Decor, Lounge", 1007, "Urban Oasis Hotel" },
                    { 8, "505 Serene Road", "Garden, Reading, Room", 1008, "Tranquil Inn" },
                    { 9, "606 Heritage Lane", "Antique Furnishings, Ballroom", 1009, "Historic Mansion Hotel" },
                    { 10, "707 Maple Court", "Kid's Play Area, Family-Friendly", 1010, "Family Suites" },
                    { 11, "808 Business Plaza", "Business Center, Concierge", 1011, "Executive Stay" },
                    { 12, "909 Forest Path", "Nature Trails, Bird Watching", 1012, "Woodland Lodge" },
                    { 13, "1001 Grand Avenue", "Luxurious Spa, Fine Dining", 1013, "Elegant Plaza Hotel" },
                    { 14, "1102 Waterfront Road", "River View, Fishing Dock", 1014, "Riverside Inn" },
                    { 15, "1203 Cozy Lane", "Charming Decor, Quiet Atmosphere", 1015, "Quaint Cottage Hotel" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CarRentals",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "CarRentals",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "CarRentals",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "CarRentals",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "CarRentals",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "CarRentals",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "CarRentals",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "CarRentals",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "CarRentals",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "CarRentals",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "CarRentals",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "CarRentals",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "CarRentals",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "CarRentals",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "CarRentals",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "CarRentals",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 10001);

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 10002);

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 10003);

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 10004);

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 10005);

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 10006);

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 10007);

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 10008);

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 10009);

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: 1017);

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: 1001);

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: 1002);

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: 1003);

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: 1004);

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: 1005);

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: 1006);

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: 1007);

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: 1008);

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: 1009);

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: 1010);

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: 1011);

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: 1012);

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: 1013);

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: 1014);

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: 1015);

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: 1016);
        }
    }
}
