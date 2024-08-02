using System.Xml.Linq;
using AutoMapper;
using Compass.Data;
using Compass.Interfaces;
using Compass.Models;

namespace Compass.Repository
{
	public class ResturantRepository : IResturantRepository
	{
		private readonly DataContext _context;
		private readonly IMapper _mapper;

		public ResturantRepository(DataContext context,IMapper mapper)
        {
			_context = context;
			_mapper = mapper;
		}

		public bool CreateResturant(Resturant resturant)
		{
			_context.Add(resturant);
			return Save();
		}

		public bool DeleteRest(Resturant resturant)
		{
			_context.Remove(resturant);
			return Save();
		}

		public bool DeleteRests(List<Resturant> resturants)
		{
			_context.RemoveRange(resturants);
			return Save() ;
		}

		public Resturant Get1Resturant(int id)
		{
			return _context.Resturants.Where(r => r.Id == id).FirstOrDefault();
		}

		public ICollection<Reservation> GetReservationOfAResturant(string resturantName)
		{
			return _context.Resturant_Reservations.Where(u => u.Resturant.Name == resturantName).Select(c => c.Reservation).ToList();
		}

		public Resturant GetResturant(string name)
		{
			return _context.Resturants.Where(r => r.Name == name).FirstOrDefault();
		}

		public ICollection<Resturant> GetResturantOfAPackage(int packageId)
		{
			return _context.Package_Resturants.Where(u => u.Package.Id == packageId).Select(c => c.Resturant).ToList();
		}

		public Resturant GetResturantOfCity(int CityId)
		{
			return _context.Cities.Where(u => u.Id == CityId).Select(c => c.Resturant).FirstOrDefault();
		}

		public ICollection<Resturant> GetResturants()
		{
			return _context.Resturants.ToList();
		}

		public bool Resturant1Exists(int RestId)
		{
			return _context.Resturants.Any(r => r.Id == RestId);
		}

		public bool ResturantExists(string name)
		{
			return _context.Resturants.Any(r => r.Name == name);
		}

		public bool Save()
		{
			var saved = _context.SaveChanges();
			return saved > 0 ? true : false;
		}

		public bool UpdateResturant(Resturant resturant)
		{

			_context.Update(resturant);
			return Save();
		}
	}
}
