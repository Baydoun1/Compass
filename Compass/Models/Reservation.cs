namespace Compass.Models
{
	public class Reservation
	{
        public int Id { get; set; }
        public float Price { get; set; }
        public DateTime Date { get; set; }
        public int Capacity { get; set; }
        public int Number { get; set; }
        public ICollection<Hotel_Reservation> Hotel_Reservations { get; set; }
        public ICollection<Resturant_Reservation> Resturant_Reservations { get; set; }
    }
}
