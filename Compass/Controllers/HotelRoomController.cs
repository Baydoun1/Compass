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
	public class HotelRoomController:Controller
	{
		private readonly IHotelRoomRepository _hotelRoomRepository;
		private readonly IHotelRepository _hotelRepository;
		private readonly IMapper _mapper;

		public HotelRoomController(IHotelRoomRepository hotelRoomRepository,
			IHotelRepository hotelRepository,IMapper mapper)
        {
			_hotelRoomRepository = hotelRoomRepository;
			_hotelRepository = hotelRepository;
			_mapper = mapper;
		}
		[HttpGet]
		[ProducesResponseType(200, Type = typeof(IEnumerable<HotelRoom>))]
		public IActionResult GetRooms()
		{
			var rooms = _mapper.Map<List<HotelRoomDto>>(_hotelRoomRepository.GetHotelRooms());
			if (!ModelState.IsValid)
				return BadRequest(ModelState);
			return Ok(rooms);
		}
		[HttpGet("{RoomId}")]
		[ProducesResponseType(200, Type = typeof(HotelRoom))]
		[ProducesResponseType(400)]
		public IActionResult GetFlightClass(int RoomId)
		{
			if (!_hotelRoomRepository.RoomExists(RoomId))
				return NotFound();
			var room = _mapper.Map<FlightClassDto>(_hotelRoomRepository.GetHotelRoom(RoomId));
			if (!ModelState.IsValid)
				return BadRequest(ModelState);
			return Ok(room);
		}
		[HttpGet("hotel/{HotelId}")]
		[ProducesResponseType(200, Type = typeof(HotelRoom))]
		[ProducesResponseType(400)]
		public IActionResult GetRoomsOfAHotel(int hotelId)
		{
			var room = _mapper.Map<List<HotelRoomDto>>(_hotelRoomRepository.GetRoomsOfAHotel(hotelId));
			if (!ModelState.IsValid)
				return BadRequest();
			return Ok(room);
		}
		[HttpPut("{RoomId}")]
		[ProducesResponseType(400)]
		[ProducesResponseType(204)]
		[ProducesResponseType(404)]
		public IActionResult UpdateAirflight(int RoomId, [FromBody] HotelRoomDto updatedRoom)
		{
			if (updatedRoom == null)
				return BadRequest(ModelState);

			if (RoomId != updatedRoom.Id)
				return BadRequest(ModelState);

			if (!_hotelRoomRepository.RoomExists(RoomId))
				return NotFound();

			if (!ModelState.IsValid)
				return BadRequest();


			// تحقق من أن AirlineId موجود
			if (!_hotelRoomRepository.HotelExists(updatedRoom.HotelId))
			{
				ModelState.AddModelError("", "Invalid HotelName.");
				return BadRequest(ModelState);
			}

			var RoomMap = _mapper.Map<HotelRoom>(updatedRoom);

			if (!_hotelRoomRepository.UpdateRoom(RoomMap))
			{
				ModelState.AddModelError("", "something went wrong updating airflight");
				return StatusCode(500, ModelState);
			}
			return NoContent();
		}
		[HttpPost]
		[ProducesResponseType(204)]
		[ProducesResponseType(400)]
		public IActionResult CreateRoom([FromQuery] string hotelName, [FromBody] HotelRoomDto roomcreate)
		{
			if (roomcreate == null)
				return BadRequest(ModelState);
			var room = _hotelRoomRepository.GetHotelRooms()
				.Where(a => a.Id == roomcreate.Id).FirstOrDefault();
			if (room != null)
			{
				ModelState.AddModelError("", "room already exists");
				return StatusCode(422, ModelState);
			}
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var roomMap = _mapper.Map<HotelRoom>(roomcreate);
			roomMap.Hotel = _hotelRepository.GetHotel(hotelName);
			if (!_hotelRoomRepository.CreateRoom(roomMap))
			{
				ModelState.AddModelError("", "something went wrong while saving");
				return StatusCode(500, ModelState);
			}
			return Ok("Successfully created");
		}
		[HttpDelete("{RoomId}")]
		[ProducesResponseType(400)]
		[ProducesResponseType(204)]
		[ProducesResponseType(404)]
		public IActionResult DeleteRoom(int RoomId)
		{
			if (!_hotelRoomRepository.RoomExists(RoomId))
				return NotFound();
			var RoomToDelete = _hotelRoomRepository.GetHotelRoom(RoomId);
			//var PackageToDelete = _packageRepository.GetPackageFromResturant(RestId);
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			if (!_hotelRoomRepository.DeleteRoom(RoomToDelete))
			{
				ModelState.AddModelError("", "SomeThing went wrong deleting room");
			}
			return NoContent();
		}
	}
}
