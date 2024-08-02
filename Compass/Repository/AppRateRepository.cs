using AutoMapper;
using Compass.Data;
using Compass.Interfaces;
using Compass.Models;

namespace Compass.Repository
{
	public class AppRateRepository : IAppRateRepository
	{
		private readonly DataContext _context;
		private readonly IMapper _mapper;

		public AppRateRepository(DataContext context,IMapper mapper)
        {
			_context = context;
			_mapper = mapper;
		}

		public bool CreateRate(AppRate apprate)
		{
		_context.Add(apprate);
			return Save();
		}

		public bool DeleteRate(AppRate appRate)
		{
			_context.Remove(appRate);
			return Save();
		}

		public AppRate GetAppRate(int id)
		{
			return _context.AppRates.Where(ar => ar.Id == id).FirstOrDefault();
		}

		public ICollection<AppRate> GetAppRates()
		{
			return _context.AppRates.ToList();

		}

		public ICollection<AppRate> GetRatesOfAUser(int userId)
		{
			return _context.AppRates.Where(ar=>ar.User.Id == userId).ToList();
		}

		public bool RateExists(int RateId)
		{
			return _context.AppRates.Any(r=>r.Id == RateId);
		}

		public bool Save()
		{
			var saved= _context.SaveChanges();
			return saved > 0 ? true : false;
		}

		public bool UpdateRate(AppRate appRate)
		{
			_context.Update(appRate);
			return Save();
		}
	}
}
