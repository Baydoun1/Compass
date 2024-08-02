using Compass.Models;

namespace Compass.Interfaces
{
	public interface IReservationRepository
	{
		ICollection<Reservation>GetReservations();
		Reservation GetReservation(int id);
		ICollection<Reservation> GetReservationOfAResturant(int resturantId);
		ICollection<Reservation> GetReservationOfAHotel(int hotelId);
		bool ReservationExists(int reserveId);
		bool DeleteReservation(Reservation reservation);
		bool DeleteReservations(List<Reservation> reservations);
		bool CreateReservation(string resturnatName,string hotelName, Reservation reservation);
		bool UpdateReservation(int RestId,int HotelId,Reservation reservation);
		bool Save();
	}
}
