using Compass.Models;

namespace Compass.Interfaces
{
	public interface IAirlineRepository
	{
		ICollection<Airline>GetAirlines();
		Airline GetAirline(int id);
		Airline GetAirlineByAirFlight(int id);
		bool AirlineExists(int AirlineId);
		bool CreateAirline(Airline airline);
		bool DeleteAirline(Airline airline);
		bool UpdateAirline(Airline airline);
		bool Save();
	}
}
