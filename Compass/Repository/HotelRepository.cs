using System.Xml.Linq;
using AutoMapper;
using Compass.Data;
using Compass.Interfaces;
using Compass.Models;

namespace Compass.Repository
{
	public class HotelRepository : IHotelRepository
	{
		private readonly DataContext _context;
		private readonly IMapper _mapper;

		public HotelRepository(DataContext context,IMapper mapper)
        {
			_context = context;
			_mapper = mapper;
		}

		public bool CityExists(int CityId)
		{
			return _context.Cities.Any(h => h.Id == CityId);
		}

		public bool CreateHotel(Hotel hotel)
		{
			_context.Add(hotel);
			return Save();
		}

		public bool DeleteHotel(Hotel hotel)
		{
			_context.Remove(hotel);
			return Save();
		}

		public Hotel Get1Hotel(int Id)
		{
			return _context.Hotels.Where(h => h.Id == Id).FirstOrDefault();
		}

		public Hotel GetHotel(string name)
		{
			return _context.Hotels.Where(h => h.Name == name).FirstOrDefault();
		}

		public ICollection<Hotel> GetHotelOfAReservation(int reservId)
		{
			return _context.Hotel_Reservations.Where(u => u.Reservation.Id == reservId).Select(c => c.Hotel).ToList();
		}

		public Hotel GetHotelOfPackage(int Id)
		{
			return _context.Packages.Where(u => u.Id == Id).Select(c => c.Hotel).FirstOrDefault();
		}

		public ICollection<Hotel> GetHotels()
		{
			return _context.Hotels.ToList();
		}

		public ICollection<Reservation> GetReservationOfAHotel(string name)
		{
			return _context.Hotel_Reservations.Where(u => u.Hotel.Name == name).Select(c => c.Reservation).ToList();
		}

		public bool Hotel1Exists(int hotelId)
		{
			return _context.Hotels.Any(h => h.Id == hotelId);
		}

		public bool HotelExists(string hotelName)
		{
			return _context.Hotels.Any(h=>h.Name== hotelName);
		}

		public bool Save()
		{
			var saved = _context.SaveChanges();
			return saved > 0 ? true : false;
		}

		public bool UpdateHotel(int packageId,int CityId, Hotel hotel)
		{
			_context.Update(hotel);
			return Save();
		}
	}
}
