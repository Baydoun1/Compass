using Compass.Data;
using Compass.Interfaces;
using Compass.Models;

namespace Compass.Repository
{
	public class TourCategoryRepository : ITourCategoryRepository
	{
		private DataContext _context;
        public TourCategoryRepository(DataContext context)
        {
            _context=context;
        }
        public bool CategoryExists(int id)
		{
			return _context.TourCategories.Any(c => c.Id == id);
		}

		public bool CreateCategory(TourCategory tourCategory)
		{
			_context.Add(tourCategory);
			return Save();
		}

		public bool DeleteCategory(TourCategory category)
		{
			_context.Remove(category);
			return Save();
		}

		public TourCategory GetCategoryOfTour(int id)
		{
			return _context.Tourism_Places.Where(u => u.Id == id).Select(c => c.TourCategory).FirstOrDefault();
		}

		public ICollection<Tour> GetTourByTourCategory(int categoryId)
		{
			return _context.TourCategories.Where(e=>e.Id == categoryId).Select(c => c.Tour).ToList();
		}

		public ICollection<TourCategory> GetTourCategories()
		{
			return _context.TourCategories.ToList();
		}

		public TourCategory GetTourCategory(int id)
		{
			return _context.TourCategories.Where(e => e.Id == id).FirstOrDefault();
		}

		public bool Save()
		{
			var saved= _context.SaveChanges();
			return saved > 0 ? true : false;
		}

		public bool UpdateCategory( TourCategory tourCategory)
		{
			_context.Update(tourCategory);
			return Save();
		}
	}
}
