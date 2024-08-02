using Compass.Models;

namespace Compass.Interfaces
{
	public interface ITourCategoryRepository
	{
		ICollection<TourCategory> GetTourCategories();
		TourCategory GetTourCategory(int id);
		ICollection<Tour>GetTourByTourCategory(int categoryId);
		bool CategoryExists(int id);
		TourCategory GetCategoryOfTour(int id);
		bool DeleteCategory(TourCategory category);
		bool CreateCategory(TourCategory tourCategory);
		bool UpdateCategory(TourCategory tourCategory);
		bool Save();
	}
}
