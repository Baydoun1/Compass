namespace Compass.Models
{
	public class TourCategory
	{
        public int Id { get; set; }
        public string Name { get; set; }
        public Tour Tour { get; set; }
        public int TourId { get; set; }
    }
}
