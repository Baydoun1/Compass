namespace Compass.Models
{
	public class HotelRoom
	{
        public int Id { get; set; }
        public string Type { get; set; }
        public int Price { get; set; }
        public Hotel Hotel { get; set; }
        public int HotelId { get; set; }
    }
}
