namespace GBC_Travel_Group23.ViewModels
{
    public class SearchViewModel
    {
        public List<string> SearchOptions { get; set; }
        public bool SearchHotels { get { return SearchOptions.Contains("Hotels"); } }
        public bool SearchCars { get { return SearchOptions.Contains("CarRentals");}}
        public bool SearchFlights { get { return SearchOptions.Contains("Flights"); } }
        public string From { get; set; }
        public string To { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int GuestCount { get; set; }
        public int MaxPrice { get; set; } = 999999;
        public int MinPrice { get; set; } = 0;
    }
}
