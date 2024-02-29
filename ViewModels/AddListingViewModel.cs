using GBC_Travel_Group23.Models;

namespace GBC_Travel_Group23.ViewModels
{
    public class AddListingViewModel
    {
        public string Type { get; set; }
        public string FromLocation { get; set; } = "";
        public string ToLocation { get; set; } = "";
        public string HotelName { get; set; } = "";
        public Flight Flight { get; set; } = new Flight();
        public CarRental CarRental { get; set; } = new CarRental();
        public Hotel Hotel { get; set; } = new Hotel();
        public HotelRoom HotelRoom { get; set; } = new HotelRoom();
    }
}
