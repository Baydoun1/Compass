using AutoMapper;
using Compass.Data;
using Compass.Interfaces;
using Compass.Models;

namespace Compass.Repository
{
	public class ReservationRepository : IReservationRepository
	{
		private readonly DataContext _context;
		private readonly IMapper _mapper;

		public ReservationRepository(DataContext context,IMapper mapper)
        {
			_context = context;
			_mapper = mapper;
		}

		public bool CreateReservation(string resturnatName,string hotelName, Reservation reservation)
		{
			var RestReservEntity = _context.Resturants.Where(a => a.Name == resturnatName).FirstOrDefault();
			var hotelRestEntity = _context.Hotels.Where(h => h.Name == hotelName).FirstOrDefault();

			var Resturant_Reservation = new Resturant_Reservation()
			{
				Resturant = RestReservEntity,
				Reservation = reservation,
			};
			_context.Add(Resturant_Reservation);
			var Hotel_Reservation = new Hotel_Reservation()
			{
				Hotel = hotelRestEntity,
				Reservation = reservation,
			};
			_context.Add(Hotel_Reservation);
			_context.Add(reservation);
			return Save();
		}

		public bool DeleteReservation(Reservation reservation)
		{
			_context.Remove(reservation);
			return Save();
		}

		public bool DeleteReservations(List<Reservation> reservations)
		{
			_context.RemoveRange(reservations);
			return Save();
		}

		public Reservation GetReservation(int id)
		{
			return _context.Reservations.Where(r => r.Id == id).FirstOrDefault();
		}

		public ICollection<Reservation> GetReservationOfAHotel(int hotelId)
		{
			return _context.Hotel_Reservations.Where(u => u.Hotel.Id == hotelId).Select(c => c.Reservation).ToList();
		}

		public ICollection<Reservation> GetReservationOfAResturant(int resturantId)
		{
			return _context.Resturant_Reservations.Where(u => u.Resturant.Id == resturantId).Select(c => c.Reservation).ToList();
		}

		public ICollection<Reservation> GetReservations()
		{
			return _context.Reservations.ToList();
		}

		public bool ReservationExists(int reserveId)
		{
			return _context.Reservations.Any(r => r.Id == reserveId);
		}

		public bool Save()
		{
			var saved = _context.SaveChanges();
			return saved > 0 ? true : false;
		}

		public bool UpdateReservation(int RestId, int HotelId, Reservation reservation)
		{
			_context.Update(reservation);
			return Save();
		}
	}
}
