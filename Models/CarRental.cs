using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace GBC_Travel_Group23.Models
{
    public class CarRental
    {
        [Key] public int Id { get; set; }

        [Required]
        [ForeignKey("Location")]
        public int LocationId { get; set; }
        public Location Location { get; set; }

        [Required] public string Make { get; set; } = string.Empty;
        [Required] public string Model { get; set; } = string.Empty;
        [Required] public int CarYear { get; set; }
        [Required] public int Capacity { get; set; }
        [Required] public int Count { get; set; }
        [Required] public double Rate { get; set; }

        [InverseProperty("CarRental")]
        public ICollection<Booking>? Bookings { get; set; }
    }
}
