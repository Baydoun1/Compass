namespace Compass.Models
{
	public class City
	{
        public int Id { get; set; }
        public string Name { get; set; }
        public Airport Airport { get; set; }
        public Hotel Hotel { get; set; }
        public Resturant Resturant { get; set; }
        public User User { get; set; }
        public Tour Tour { get; set; }
    }
}
