using Compass.Models;

namespace Compass.Interfaces
{
	public interface ITourRepository
	{
		ICollection<Tour> GetTours();
		Tour GetTour(int id);
		ICollection<Tour> GetTourOfAPackage(int PackageId);
		bool TourExists(int TourId);
		bool CreateTour(Tour tour);
		bool DeleteTour(Tour tour);
		bool UpdateTour(int packageId,Tour tour);
		bool DeleteTours(List<Tour> tours);
		bool Save();
	}
}
