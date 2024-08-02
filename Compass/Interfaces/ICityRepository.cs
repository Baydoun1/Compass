using Compass.Models;

namespace Compass.Interfaces
{
	public interface ICityRepository
	{
		ICollection<City> GetCities();
		City GetCity(string name);
		City Get1City(int CityId);
		City GetCityByUser(int id);
		ICollection<User>GetUsersFromACity(string cityName);
		ICollection<User> Get1UsersFromACity(int CityId);
		bool CityExists(string name);
		bool City1Exists(int CityId);
		bool CreateCity(City city);
		bool UpdateCity(City city);
		bool DeleteCity(City city);
		bool Save();
		
	}
}
