using Compass.Models;

namespace Compass.Interfaces
{
	public interface IUserRepository
	{
		ICollection<User>GetUsers();
		User GetUser(int id);
		User GetUserFromCity(string cityName);
		User Get1UserFromCity(int CityId);
		User GetUser(string username);
		//one user to many ratings
		decimal GetUserRating(int UserId);
		bool UserExists(int UserId);
		bool CreateUser(User user);
		bool DeleteUser(User user);
		bool UpdateUser(User user);
		bool Save();
	}
}
