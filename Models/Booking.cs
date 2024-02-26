using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;
namespace GBC_Travel_Group23.Models
{
    public class Booking
    {
        [Key] public int Id { get; set; }

        [Required]
        [ForeignKey("Client")]
        public int ClientId { get; set; }
        public Client Client { get; set; } = new Client();

        [Required]
        [RegularExpression("(hotelroom|flight|car)", ErrorMessage = "The Type must be either flight, hotel, or car!")]
        public string Type { get; set; } = "hotelroom";

        [Required] public int ServiceId { get; set; }

        [Required] public int GuestCount { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [NotMapped] public Flight? Flight { get; private set; }

        [NotMapped] public HotelRoom? HotelRoom { get; private set; }

        [NotMapped] public CarRental? CarRental { get; private set; }


        public void SetService(object service)
        {
            switch (service)
            {
                case Flight flight:
                    Type = "Flight";
                    Flight = flight;
                    ServiceId = flight.Id;
                    break;
                case HotelRoom hotelroom:
                    Type = "HotelRoom";
                    HotelRoom = hotelroom;
                    ServiceId = hotelroom.Id;
                    break;
                case CarRental carRental:
                    Type = "CarRental";
                    CarRental = carRental;
                    ServiceId = carRental.Id;
                    break;
                default:
                    throw new ArgumentException("Invalid service type", nameof(service));
            }
        }
    }
}
