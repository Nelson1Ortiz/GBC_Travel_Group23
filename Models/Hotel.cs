using Microsoft.EntityFrameworkCore;
using Mono.TextTemplating;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace GBC_Travel_Group23.Models
{
    public class Hotel
    {
        [Key] public int Id { get; set; }
        [Required] public string Name { get; set; } = string.Empty;

        [Required]
        [ForeignKey("Location")]
        public int LocationId { get; set; }
        public Location Location { get; set; } = new Location();
        [Required] public string Address { get; set; } = string.Empty;
        [Required] public string[] Amenities { get; set; } = Array.Empty<string>();
        
        [InverseProperty("Hotel")]
        public ICollection<HotelRoom>? Rooms { get; set; }
        
    }
}
