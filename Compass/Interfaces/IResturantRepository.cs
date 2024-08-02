using Compass.Models;

namespace Compass.Interfaces
{
	public interface IResturantRepository
	{
		ICollection<Resturant>GetResturants();
		Resturant GetResturant(string name);
		Resturant Get1Resturant(int id);
		ICollection<Reservation> GetReservationOfAResturant(string resturantName);
		ICollection<Resturant> GetResturantOfAPackage(int packageId);
		Resturant GetResturantOfCity(int CItyId);
		bool UpdateResturant(Resturant resturant);
		bool ResturantExists(string name);
		bool Resturant1Exists(int RestId);
		bool CreateResturant(Resturant resturant);
		bool DeleteRests(List<Resturant> resturants);
		bool DeleteRest(Resturant resturant);
		bool Save();
	}
}
