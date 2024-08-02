namespace Compass.Models
{
	public class FlightClass
	{
        public int Id { get; set; }
        public string Name { get; set; }
        public AirFlight AirFlight { get; set; }
        public int AirFlightId { get; set; }
    }
}
