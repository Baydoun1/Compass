using AutoMapper;
using Compass.Data;
using Compass.Interfaces;
using Compass.Models;

namespace Compass.Repository
{
	public class FlightClassRepository : IFlightClassRepository
	{
		private readonly DataContext _context;
		private readonly IMapper _mapper;

		public FlightClassRepository(DataContext context,IMapper mapper)
        {
			_context = context;
			_mapper = mapper;
		}
        public bool ClassExists(int ClassId)
		{
			return _context.FlightClasses.Any(f=>f.Id== ClassId);
		}

		public bool CreateClass(FlightClass flightClass)
		{
			_context.Add(flightClass);
			return Save();
		}

		public bool DeleteFlightclass(FlightClass flightClass)
		{
			_context.Remove(flightClass);
			return Save();
		}

		public bool DeleteFlightClasses(List<FlightClass> flightClasses)
		{
			_context.RemoveRange(flightClasses);
			return Save();
		}

		public bool FlightExists(int FlightId)
		{
			return _context.AirFlights.Any(f => f.Id == FlightId);
		}

		public ICollection<FlightClass> GetClassesOfAFlight(int airflightId)
		{
			return _context.FlightClasses.Where(ar => ar.AirFlight.Id == airflightId).ToList();
		}

		public FlightClass GetFlightClass(int id)
		{
			return _context.FlightClasses.Where(f => f.Id == id).FirstOrDefault();
		}

		public ICollection<FlightClass> GetFlightClasses()
		{
			return _context.FlightClasses.ToList();
		}

		public bool Save()
		{
			var saved= _context.SaveChanges();
			return saved > 0 ? true : false;
		}

		public bool UpdateClass( FlightClass flightClass)
		{
			_context.Update(flightClass);
			return Save();
		}
	}
}
