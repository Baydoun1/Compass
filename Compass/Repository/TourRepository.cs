using AutoMapper;
using Compass.Data;
using Compass.Interfaces;
using Compass.Models;

namespace Compass.Repository
{
	public class TourRepository : ITourRepository
	{
		private readonly DataContext _context;
		private readonly IMapper _mapper;

		public TourRepository(DataContext context,IMapper mapper)
        {
			_context = context;
			_mapper = mapper;
		}

		public bool CreateTour(Tour tour)
		{
			_context.Add(tour);
			return Save();
		}

		public bool DeleteTour(Tour tour)
		{
			_context.Remove(tour);
			return Save();
		}

		public bool DeleteTours(List<Tour> tours)
		{
			_context.RemoveRange(tours);
			return Save();
		}

		public Tour GetTour(int id)
		{
			return _context.Tourism_Places.Where(t => t.Id == id).FirstOrDefault();
		}

		public ICollection<Tour> GetTourOfAPackage(int PackageId)
		{
			return _context.Tourism_Places.Where(t=>t.Package.Id==PackageId).ToList();
		}

		public ICollection<Tour> GetTours()
		{
			return _context.Tourism_Places.ToList();
		}

		public bool Save()
		{
			var saved = _context.SaveChanges();
			return saved > 0 ? true : false;
		}

		public bool TourExists(int TourId)
		{
			return _context.Tourism_Places.Any(t=>t.Id== TourId);
		}

		public bool UpdateTour(int packageId, Tour tour)
		{
			_context.Update(tour);
			return Save();
		}
	}
}
