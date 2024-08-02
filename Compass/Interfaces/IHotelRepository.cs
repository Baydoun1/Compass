using Compass.Models;

namespace Compass.Interfaces
{
	public interface IHotelRepository
	{
		ICollection<Hotel> GetHotels();
		Hotel GetHotel(string name);
		Hotel Get1Hotel(int Id);
		ICollection<Hotel> GetHotelOfAReservation(int reservId);
		ICollection<Reservation> GetReservationOfAHotel(string name);
		Hotel GetHotelOfPackage(int Id);
		bool HotelExists(string hotelName);
		bool Hotel1Exists(int hotelId);
		bool CityExists(int CityId);
		bool DeleteHotel(Hotel hotel);
		bool CreateHotel(Hotel hotel);
		bool UpdateHotel(int packageId,int CityId,Hotel hotel);
		bool Save();
	}
}
