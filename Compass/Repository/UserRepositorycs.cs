using System.Linq;
using Compass.Data;
using Compass.Interfaces;
using Compass.Models;

namespace Compass.Repository
{
	public class UserRepository : IUserRepository
	{
        private readonly DataContext _context;
        public UserRepository(DataContext context)
        {
            _context = context;
        }

		public bool CreateUser(User user)
		{
			_context.Add(user);
			return Save();
		}

		public bool DeleteUser(User user)
		{
			_context.Remove(user);
			return Save();
		}

		public User Get1UserFromCity(int CityId)
		{
			return _context.Cities.Where(u => u.Id == CityId).Select(c => c.User).FirstOrDefault();
		}

		public User GetUser(int id)
		{
			return _context.Users.Where(u => u.Id == id).FirstOrDefault();
		}

		public User GetUser(string username)
		{
			return _context.Users.Where(u => u.FirstName == username).FirstOrDefault();
		}

		public User GetUserFromCity(string cityName)
		{
			return _context.Cities.Where(u => u.Name == cityName).Select(c => c.User).FirstOrDefault();
		}

		public decimal GetUserRating(int UserId)
		{
			throw new NotImplementedException();
		}

		public ICollection<User> GetUsers() 
        {
            return _context.Users.OrderBy(u => u.Id).ToList();   
        }

		public bool Save()
		{
			var saved=_context.SaveChanges();
			return saved > 0 ? true : false;
		}

		public bool UpdateUser(User user)
		{
			_context.Update(user);
			return Save();
		}

		public bool UserExists(int UserId)
		{
			return _context.Users.Any(u=>u.Id == UserId);
		}
	}
}
