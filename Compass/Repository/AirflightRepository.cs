using AutoMapper;
using Compass.Data;
using Compass.Interfaces;
using Compass.Models;
using Microsoft.EntityFrameworkCore;

namespace Compass.Repository
{
	public class AirflightRepository : IAirflightRepository
	{
		private readonly DataContext _context;
		private readonly IMapper _mapper;

		public AirflightRepository(DataContext context,IMapper mapper)
        {
			_context = context;
			_mapper = mapper;
		}
        public bool AirflightExists(int id)
		{
			return _context.AirFlights.Any(a=>a.Id== id);
		}

		public bool AirlineExists(int airlineId)
		{
			return _context.Airlines.Any(a => a.Id == airlineId);
		}

		public bool CreateAirflight(AirFlight airFlight)
		{
			//change tracker
			_context.Add(airFlight);
			return Save();
		}

		public bool DeleteAirflight(AirFlight airFlight)
		{
			_context.Remove(airFlight);
			return Save();
		}

		public AirFlight GetAirFlight(int id)
		{
			return _context.AirFlights.Where(_a => _a.Id == id).FirstOrDefault();
		}

		public AirFlight GetAirflightFromAirline(int airlineId)
		{
			return _context.Airlines.Where(u => u.Id == airlineId).Select(c => c.AirFlight).FirstOrDefault();
		}

		public ICollection<AirFlight> GetAirFlights()
		{
			return _context.AirFlights.ToList();
		}

		public ICollection<Airport> GetAirportFromAFlight(int flightId)
		{
			return _context.Airports.Where(c => c.AirFlight.Id == flightId).ToList();
		}

		public AirFlight GetFlightFromPackage(int packageId)
		{
			return _context.Packages.Where(u => u.Id == packageId).Select(c => c.AirFlight).FirstOrDefault();
		}

		public bool Save()
		{
			var saved = _context.SaveChanges();	
			return saved > 0 ? true : false;
		}

		public bool UpdateAirflight(AirFlight airFlight)
		{
			_context.Update(airFlight);
			return Save();
		}
	}
}
