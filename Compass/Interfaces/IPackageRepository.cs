using Compass.Models;

namespace Compass.Interfaces
{
	public interface IPackageRepository
	{
		ICollection<Package> GetPackages();
		Package GetPackage(int id);
		ICollection<Resturant> GetResturantOfAPacakage(int packageId);
		Package GetPackageOfAirflight(int airflightId);
		bool PackageExists(int packageId);
		bool UpdatePackage(Package package);
		bool FlightExists(int FlightId);
		bool DeletePackage(Package package);
		bool CreatePackage(string resturantName,Package package);
		bool Save();
	}
}
