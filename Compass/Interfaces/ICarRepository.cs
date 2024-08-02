using Compass.Models;

namespace Compass.Interfaces
{
	public interface ICarRepository
	{
		ICollection<Car> GetCars();
		Car GetCar(int id);
		//many to many car user
		ICollection<Car> GetCarOfAUser(int userId);
		ICollection<User> GetUserByCar(int CarId);
		bool DeleteCar(Car car);
		bool CarExists(int CarId);
		bool CreateCar(Car car);
		bool UpdateCar(int UserId,Car car);
		bool Save();
	}
}
