using Compass.Models;

namespace Compass.Interfaces
{
	public interface IAirportRepository
	{
		ICollection<Airport>GetAirports();
		Airport GetAirport(string name);
		ICollection<Airport> GetAirportOfAirflight(int airflightId);
		bool AirportExists(string AirportName);
		bool Airport1Exists(int AirportId);
		bool AirflightExists(int airflightId);
		bool CityExists(int CityId);
		bool CreateAirport(Airport airport);
		bool DeleteAirport(Airport airport);
		bool DeleteAirports(List<Airport> airports);
		bool UpdateAirport(Airport airport);
		bool Save();
	}
}
