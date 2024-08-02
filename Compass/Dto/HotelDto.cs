namespace Compass.Dto
{
	public class HotelDto
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string Location { get; set; }
		public float Stars { get; set; }
		public int Phone { get; set; }
		public string Website { get; set; }
		public int CityId {  get; set; }
		public int packageId {  get; set; }
	}
}
