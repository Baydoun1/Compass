using Compass.Data;
using Compass.Interfaces;
using Compass.Models;

namespace Compass.Repository
{
	public class AirlineRepository : IAirlineRepository
	{
		private readonly DataContext _context;

		public AirlineRepository(DataContext context)
        {
			_context = context;
		}
        public bool AirlineExists(int AirlineId)
		{
			return _context.Airlines.Any(a => a.Id == AirlineId);
		}

		public bool CreateAirline(Airline airline)
		{
			_context.Add(airline);
			return Save();
		}

		public bool DeleteAirline(Airline airline)
		{
			_context.Remove(airline);
			return Save();
		}

		public Airline GetAirline(int id)
		{
			return _context.Airlines.Where(a => a.Id == id).FirstOrDefault();
		}

		public Airline GetAirlineByAirFlight(int AirLineId)
		{
			return _context.AirFlights.Where(u => u.Id == AirLineId).Select(c => c.Airline).FirstOrDefault();
		}

		public ICollection<Airline> GetAirlines()
		{
			return _context.Airlines.ToList();
		}

		public bool Save()
		{
			var saved = _context.SaveChanges();
			return saved > 0 ? true : false;
		}

		public bool UpdateAirline(Airline airline)
		{
			_context.Update(airline);
			return Save();
		}
	}
}
