namespace Compass.Models
{
	public class Hotel
	{
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
		public string Location { get; set; }
        public float Stars { get; set; }
        public int Phone { get; set; }
        public string Website { get; set; }
        public City City { get; set; }
        public int CityId { get; set; }
        public Package package { get; set; }
        public int PackageId { get; set; }
        public ICollection<HotelRoom> HotelRooms { get; set; }
        public ICollection<Hotel_Reservation> Hotel_Reservations { get; set; }
    }
}
