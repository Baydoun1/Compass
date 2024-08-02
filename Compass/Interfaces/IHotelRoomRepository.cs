using Compass.Models;

namespace Compass.Interfaces
{
	public interface IHotelRoomRepository
	{
		ICollection<HotelRoom> GetHotelRooms();
		HotelRoom GetHotelRoom(int id);
		//many hotelrooms to one hotel
		ICollection<HotelRoom> GetRoomsOfAHotel(int hotelId);
		bool RoomExists(int RoomId);
		bool DeleteRoom(HotelRoom room);
		bool CreateRoom(HotelRoom hotelRoom);
		bool HotelExists(int HotelId);
		bool UpdateRoom(HotelRoom hotelRoom);
		bool DeleteRooms(List<HotelRoom> rooms);
		bool Save();
	}
}
