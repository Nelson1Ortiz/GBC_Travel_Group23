    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    namespace GBC_Travel_Group23.Models
    {
        public class HotelRoom
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int Id { get; set; }

            [Required]
            [ForeignKey("Hotel")]
            public int HotelId { get; set; }

            public Hotel Hotel { get; set; } = new Hotel();

            [Required] public string RoomName { get; set; } = string.Empty;

            [Required] public int MaxOccupants { get; set; }

            [Required] public string[] Amenities { get; set; } = [];

            [Required] public int RoomCount { get; set; }

            [Required] public double Rate { get; set; }

            [InverseProperty("HotelRoom")]
            public ICollection<Booking>? Bookings { get; set; }
        }
    }
