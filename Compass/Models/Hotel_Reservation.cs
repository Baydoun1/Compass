namespace Compass.Models
{
	public class Hotel_Reservation
	{
        public int HotelId { get; set; }
        public int ReservationId { get; set; }
        public Hotel Hotel { get; set; }
        public Reservation Reservation { get; set; }
    }
}
