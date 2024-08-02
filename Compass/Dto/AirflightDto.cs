namespace Compass.Dto
{
	public class AirflightDto
	{
		public int Id { get; set; }
		public int AirlineId {  get; set; }
		public DateTime Departure_datetime { get; set; }
		public DateTime Arrival_datetime { get; set; }
		public float Price { get; set; }
	}
}
