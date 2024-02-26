using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GBC_Travel_Group23.Models
{
    public class Client
    {
        [Key] public int Id { get; set; }
        [Required] public string FullName { get; set; } = string.Empty;
        [Required][EmailAddress] public string Email { get; set; } = string.Empty;
        [Required][Phone] public string Phone { get; set; } = string.Empty;

        [InverseProperty("Client")]
        public ICollection<Booking>? Bookings { get; set; }
    }
}
