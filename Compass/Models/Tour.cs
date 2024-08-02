namespace Compass.Models
{
	public class Tour
	{
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public int YearlyVisitors { get; set; }
        public string Description { get; set; }
        public TourCategory TourCategory { get; set; }
        public City City { get; set; }
        public int CityId { get; set; }
        public Package Package { get; set; }
        public int PackageId { get; set; }

    }
}
