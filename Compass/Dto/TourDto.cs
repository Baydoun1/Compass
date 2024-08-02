namespace Compass.Dto
{
	public class TourDto
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Location { get; set; }
		public int YearlyVisitors { get; set; }
		public string Description { get; set; }
		public int CityId {  get; set; }
		public int PackId {  get; set; }
	}
}
