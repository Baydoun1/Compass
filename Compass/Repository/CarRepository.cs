using Compass.Data;
using Compass.Interfaces;
using Compass.Models;

namespace Compass.Repository
{
	public class CarRepository : ICarRepository
	{
		private readonly DataContext _context;

		public CarRepository(DataContext context)
        {
			_context = context;
		}
        public bool CarExists(int CarId)
		{
			return _context.Cars.Any(c => c.Id == CarId);
		}

		public bool CreateCar(Car car)
		{
			_context.Add(car);
			return Save();
		}

		public bool DeleteCar(Car car)
		{
			_context.Remove(car);
			return Save();
		}

		public Car GetCar(int id)
		{
			return _context.Cars.Where(c => c.Id == id).FirstOrDefault();
		}

		public ICollection<Car> GetCarOfAUser(int userId)
		{
			return _context.Car_Users.Where(u=>u.User.Id == userId).Select(c=>c.Car).ToList();
		}

		public ICollection<Car> GetCars()
		{
			return _context.Cars.ToList();
		}

		public ICollection<User> GetUserByCar(int CarId)
		{
			return _context.Car_Users.Where(c => c.Car.Id == CarId).Select(u => u.User).ToList();
		}

		public bool Save()
		{
			var saved=_context.SaveChanges();
			return saved > 0 ? true : false;
		}

		public bool UpdateCar(int UserId, Car car)
		{
			_context.Update(car);
			return Save();
		}
	}
}
