using System.Xml.Linq;
using AutoMapper;
using Compass.Data;
using Compass.Interfaces;
using Compass.Models;

namespace Compass.Repository
{
	public class CityRepository : ICityRepository
	{
		private readonly DataContext _context;
		private readonly IMapper _mapper;

		public CityRepository(DataContext context,IMapper mapper)
        {
			_context = context;
			_mapper = mapper;
		}

		public bool City1Exists(int CityId)
		{
			return _context.Cities.Any(c => c.Id == CityId);
		}

		public bool CityExists(string name)
		{
			return _context.Cities.Any(c=>c.Name == name);
		}

		public bool CreateCity(City city)
		{
			_context.Add(city);
			return Save();
		}

		public bool DeleteCity(City city)
		{
			_context.Remove(city);
			return Save();
		}

		public City Get1City(int CityId)
		{
			return _context.Cities.Where(e => e.Id == CityId).FirstOrDefault();
		}

		public ICollection<User> Get1UsersFromACity(int CityId)
		{
			return _context.Users.Where(c => c.Nationality.Id == CityId).ToList();
		}

		public ICollection<City> GetCities()
		{
			return _context.Cities.ToList();
		}

		public City GetCity(string name)
		{
			return _context.Cities.Where(e => e.Name == name).FirstOrDefault();
		}

		public City GetCityByUser(int userId)
		{
			return _context.Users.Where(u=>u.Id==userId).Select(c=>c.Nationality).FirstOrDefault();
		}

		
		public ICollection<User> GetUsersFromACity(string cityName)
		{
			return _context.Users.Where(c=>c.Nationality.Name== cityName).ToList();
		}

		public bool Save()
		{
			var saved = _context.SaveChanges();
			return saved > 0 ? true : false;
		}

		public bool UpdateCity(City city)
		{
			_context.Update(city);
			return Save();
		}
	}
}
