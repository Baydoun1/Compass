namespace Compass.Dto
{
	public class PackageDto
	{
		 public int Id { get; set; }
        public DateTime Date_Start { get; set; }
        public DateTime Date_End { get; set; }
        public float Price { get; set; }
        public int Tourism_PlaceId { get; set; }
		public int FlightId {  get; set; }
		public string HotelName {  get; set; }
		public int TourId {  get; set; }
		public string RestName {  get; set; }
	}
}
