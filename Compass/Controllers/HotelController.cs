using AutoMapper;
using Compass.Dto;
using Compass.Interfaces;
using Compass.Models;
using Compass.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Compass.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class HotelController : Controller
	{
		private readonly IHotelRepository _hotelRepository;
		private readonly ICityRepository _cityRepository;
		private readonly IReservationRepository _reservationRepository;
		private readonly IHotelRoomRepository _hotelRoomRepository;
		private readonly IPackageRepository _packageRepository;
		private readonly IMapper _mapper;

		public HotelController(IHotelRepository hotelRepository,
			ICityRepository cityRepository,IReservationRepository reservationRepository, IHotelRoomRepository hotelRoomRepository,IPackageRepository packageRepository, IMapper mapper)
		{
			_hotelRepository = hotelRepository;
			_cityRepository = cityRepository;
			_reservationRepository = reservationRepository;
			_hotelRoomRepository = hotelRoomRepository;
			_packageRepository = packageRepository;
			_mapper = mapper;
		}
		[HttpGet]
		[ProducesResponseType(200, Type = typeof(IEnumerable<Hotel>))]
		public IActionResult GetHotels()
		{
			var hotels = _mapper.Map<List<HotelDto>>(_hotelRepository.GetHotels());
			if (!ModelState.IsValid)
				return BadRequest(ModelState);
			return Ok(hotels);
		}

		[HttpGet("{hotelId}/reserv")]
		[ProducesResponseType(200, Type = typeof(Hotel))]
		[ProducesResponseType(400)]
		public IActionResult GetReservationOfAHotel(string hotelName)
		{
			if (!_hotelRepository.HotelExists(hotelName))
			{
				return NotFound();
			}
			//getting pokemon by owner so we're gonna be returning the pokemondto
			var hotel = _mapper.Map<List<HotelDto>>(_hotelRepository.GetReservationOfAHotel(hotelName));

			if (!ModelState.IsValid)
				return BadRequest(ModelState);
			return Ok(hotel);
		}
		[HttpPut("{HotelId}")]
		[ProducesResponseType(400)]
		[ProducesResponseType(204)]
		[ProducesResponseType(404)]
		public IActionResult UpdateHotel([FromQuery] int packageId, [FromQuery] int CityId, int HotelId, [FromBody] HotelDto updatedHotel)
		{
			if (updatedHotel == null)
				return BadRequest(ModelState);

			if (HotelId != updatedHotel.Id)
				return BadRequest(ModelState);

			if (!_hotelRepository.Hotel1Exists(HotelId))
				return NotFound();

			if (!ModelState.IsValid)
				return BadRequest();

			// تحقق من أن AirlineId موجود
			/*	if (!_hotelRepository.CityExists(updatedHotel.CityId))
				{
					ModelState.AddModelError("", "Invalid CityId.");
					return BadRequest(ModelState);
				}*/

			var HotelMap = _mapper.Map<Hotel>(updatedHotel);

			if (!_hotelRepository.UpdateHotel(packageId, CityId, HotelMap))
			{
				ModelState.AddModelError("", "something went wrong updating hotel");
				return StatusCode(500, ModelState);
			}
			return NoContent();
		}
		[HttpPost]
		[ProducesResponseType(204)]
		[ProducesResponseType(400)]
		public IActionResult CreateHotel([FromQuery] string cityName, [FromQuery] int packageId, [FromBody] HotelDto hotelcreate)
		{
			if (hotelcreate == null)
				return BadRequest(ModelState);
			var hotel = _hotelRepository.GetHotels()
				.Where(a => a.Name.Trim().ToUpper() == hotelcreate.Name.TrimEnd().ToUpper()).FirstOrDefault();
			if (hotel != null)
			{
				ModelState.AddModelError("", "hotel already exists");
				return StatusCode(422, ModelState);
			}
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var hotelMap = _mapper.Map<Hotel>(hotelcreate);
			hotelMap.City = _cityRepository.GetCity(cityName);
			hotelMap.package = _packageRepository.GetPackage(packageId);
			if (!_hotelRepository.CreateHotel(hotelMap))
			{
				ModelState.AddModelError("", "something went wrong while saving");
				return StatusCode(500, ModelState);
			}
			return Ok("Successfully created");
		}
		[HttpDelete("{HotId}")]
		[ProducesResponseType(400)]
		[ProducesResponseType(204)]
		[ProducesResponseType(404)]
		public IActionResult DeleteHotel(int HotId)
		{
			if (!_hotelRepository.Hotel1Exists(HotId))
				return NotFound();
			var RoomToDelete = _hotelRoomRepository.GetRoomsOfAHotel(HotId);
			var ReservToDelete = _reservationRepository.GetReservationOfAHotel(HotId);
			var HotelToDelete = _hotelRepository.Get1Hotel(HotId);
		
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			if (!_hotelRoomRepository.DeleteRooms(RoomToDelete.ToList()))
			{
				ModelState.AddModelError("", "SomeThing went wrong deleting rooms");
			}

			if (!_reservationRepository.DeleteReservations(ReservToDelete.ToList()))
			{
				ModelState.AddModelError("", "SomeThing went wrong deleting reservations");
			}

			if (!_hotelRepository.DeleteHotel(HotelToDelete))
			{
				ModelState.AddModelError("", "SomeThing went wrong deleting hotel");
			}
			return NoContent();
		}
	}
};
