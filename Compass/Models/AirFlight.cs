namespace Compass.Models
{
	public class AirFlight
	{
        public int Id { get; set; }
        public DateTime Departure_datetime { get; set; }
        public DateTime Arrival_datetime { get; set; }
        public float Price { get; set; }
        public Airline Airline { get; set; }
        public int AirlineId { get; set; }
        public ICollection<Airport> Airports { get; set; }
        public ICollection<FlightClass> FlightClasses { get; set; }
        public Package Package { get; set; }
        public int PackageId { get; set; }
    }
}
