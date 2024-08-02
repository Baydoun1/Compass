namespace Compass.Models
{
	public class Package
	{
        public int Id { get; set; }
        public DateTime Date_Start { get; set; }
        public DateTime Date_End { get; set; }
        public float Price { get; set; }
        public int Tourism_PlaceId { get; set; }
        public AirFlight AirFlight { get; set; }
        public int AirFlightId { get; set; }
        public Hotel Hotel { get; set; }
        public ICollection<Tour> Tours { get; set; }
        public ICollection<Package_Resturant> Package_Resturants { get; set; }
    }
}
