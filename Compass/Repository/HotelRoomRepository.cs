using AutoMapper;
using Compass.Data;
using Compass.Interfaces;
using Compass.Models;

namespace Compass.Repository
{
	public class HotelRoomRepository : IHotelRoomRepository
	{
		private readonly DataContext _context;
		private readonly IMapper _mapper;

		public HotelRoomRepository(DataContext context,IMapper mapper)
        {
			_context = context;
			_mapper = mapper;
		}

		public bool CreateRoom(HotelRoom hotelRoom)
		{
			_context.Add(hotelRoom);
			return Save();
		}

		public bool DeleteRoom(HotelRoom room)
		{
			_context.Remove(room);
			return Save();
		}

		public bool DeleteRooms(List<HotelRoom> rooms)
		{
			_context.RemoveRange(rooms);
			return Save();
		}

		public HotelRoom GetHotelRoom(int id)
		{
			return _context.HotelRooms.Where(r => r.Id == id).FirstOrDefault();
		}

		public ICollection<HotelRoom> GetHotelRooms()
		{
			return _context.HotelRooms.ToList();
		}

		public ICollection<HotelRoom> GetRoomsOfAHotel(int hotelId)
		{
			return _context.HotelRooms.Where(ar => ar.Hotel.Id == hotelId).ToList();
		}

		public bool HotelExists(int HotelId)
		{
			return _context.Hotels.Any(r => r.Id == HotelId);
		}

		public bool RoomExists(int RoomId)
		{
			return _context.HotelRooms.Any(r=>r.Id== RoomId);
		}

		public bool Save()
		{
			var saved= _context.SaveChanges();
			return saved > 0 ? true : false;
		}

		public bool UpdateRoom(HotelRoom hotelRoom)
		{
			_context.Update(hotelRoom);
			return Save();
		}
	}
}
