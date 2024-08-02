using AutoMapper;
using Compass.Data;
using Compass.Interfaces;
using Compass.Models;

namespace Compass.Repository
{
	public class AirportRepository : IAirportRepository
	{
		private readonly DataContext _context;
		private readonly IMapper _mapper;

		public AirportRepository(DataContext context,IMapper mapper)
        {
			_context = context;
			_mapper = mapper;
		}

		public bool AirflightExists(int airflightId)
		{
			return _context.AirFlights.Any(a => a.Id == airflightId);
		}

		public bool Airport1Exists(int AirportId)
		{
			return _context.Airports.Any(a=>a.Id == AirportId);
		}

		public bool AirportExists(string AirportName)
		{
			return _context.Airports.Any(a => a.Name == AirportName);
		}

		public bool CityExists(int CityId)
		{
			return _context.Cities.Any(a => a.Id == CityId);
		}

		public bool CreateAirport(Airport airport)
		{
			_context.Add(airport);
			return Save();
		}

		public bool DeleteAirport(Airport airport)
		{
			_context.Remove(airport);
			return Save();
		}

		public bool DeleteAirports(List<Airport> airports)
		{
			_context.RemoveRange(airports);
			return Save();
		}

		public Airport GetAirport(string name)
		{
			return _context.Airports.Where(a => a.Name == name).FirstOrDefault();
		}

		public ICollection<Airport> GetAirportOfAirflight(int airflightId)
		{
			return _context.Airports.Where(a=>a.AirFlight.Id==airflightId).ToList();
		}

		public ICollection<Airport> GetAirports()
		{
			return _context.Airports.ToList();
		}

		public bool Save()
		{
			var saved = _context.SaveChanges();
			return saved > 0 ? true : false;
		}

		public bool UpdateAirport(Airport airport)
		{
			_context.Update(airport);
			return Save();
		}
	}
}
