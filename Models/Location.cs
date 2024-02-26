using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GBC_Travel_Group23.Models
{
    public class Location
    {
        [Key] public int Id { get; set; }
        [Required] public string Country { get; set; } = string.Empty;
        [Required] public string City { get; set; } = string.Empty;
        [Required] public string Region { get; set; } = string.Empty;
        [Required] public string AirportCode { get; set; } = string.Empty;

        [InverseProperty("DepartureLocation")]
        public ICollection<Flight>? DepartureFlights { get; set; }

        [InverseProperty("ArrivalLocation")]
        public ICollection<Flight>? ArrivalFlights { get; set; }

        [InverseProperty("Location")]
        public ICollection<CarRental>? CarRentals { get; set; }

        [InverseProperty("Location")]
        public ICollection<Hotel>? Hotels { get; set; }

    }
}
