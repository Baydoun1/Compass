using Compass.Models;

namespace Compass.Interfaces
{
	public interface IFlightClassRepository
	{
		ICollection<FlightClass>GetFlightClasses();
		FlightClass GetFlightClass(int id);
		//many flightclasses to one airflight
		ICollection<FlightClass> GetClassesOfAFlight(int airflightId);
		bool ClassExists(int ClassId);
		bool CreateClass(FlightClass flightClass);
		bool UpdateClass(FlightClass flightClass);
		bool FlightExists(int FlightId);
		bool DeleteFlightclass(FlightClass flightClass);
		bool DeleteFlightClasses(List<FlightClass> flightClasses);
		bool Save();
	}
}
