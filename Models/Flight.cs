using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace GBC_Travel_Group23.Models
{
    public class Flight
    {
        [Key] public int Id { get; set; }
        [Required] public string Airline { get; set; } = string.Empty;
        [Required] public string Plane { get; set; } = string.Empty;
        [Required] public string FlightCode { get; set; } = string.Empty;

        [Required]
        [ForeignKey("DepartureLocation")]
        public int DepartureLocationId { get; set; }
        public Location DepartureLocation { get; set; } = new Location();

        [Required]
        [ForeignKey("ArrivalLocation")]
        public int ArrivalLocationId { get; set; }
        public Location ArrivalLocation { get; set; } = new Location();

        [Required] public DateTime DepartureDate { get; set; }
        
        [Required] public DateTime ArrivalDate { get; set; }
        
        [Required] public int TotalSeats { get; set; }
        
        [Required] public double Price { get; set; }

        [InverseProperty("Flight")]
        public ICollection<Booking>? Bookings { get; set; }
    }
}
