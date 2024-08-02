namespace Compass.Models
{
	public class Resturant_Reservation
	{
        public int ResturantId { get; set; }
        public int ReservationId { get; set; }
        public Resturant Resturant { get; set; }
        public Reservation Reservation { get; set; }
    }
}
