using Compass.Models;

namespace Compass.Interfaces
{
	public interface IAirflightRepository
	{
		ICollection<AirFlight>GetAirFlights();
		ICollection<Airport> GetAirportFromAFlight(int flightId);
		AirFlight GetAirFlight(int id);
		bool AirflightExists(int id);
		AirFlight GetAirflightFromAirline(int airlineId);
		AirFlight GetFlightFromPackage(int packageId);
		bool CreateAirflight(AirFlight airFlight);
		bool UpdateAirflight(AirFlight airFlight);
		bool DeleteAirflight(AirFlight airFlight);
		bool AirlineExists(int airlineId);
		bool Save();
	}
}
