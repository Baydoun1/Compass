namespace Compass.Models
{
	public class Airport
	{
        public int Id { get; set; }
        public string Name { get; set; }
        public City City { get; set; }
        public int CityId { get; set; }
        public AirFlight AirFlight { get; set; }
        public int AirFlightId { get; set; }
    }
}
