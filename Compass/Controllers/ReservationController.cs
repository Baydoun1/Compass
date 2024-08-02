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
	public class ReservationController : Controller
	{
		private readonly IReservationRepository _reservationRepository;
		private readonly IHotelRepository _hotelRepository;
		private readonly IResturantRepository _resturantRepository;
		private readonly IMapper _mapper;

		public ReservationController(IReservationRepository reservationRepository,IHotelRepository hotelRepository,
			IResturantRepository resturantRepository,IMapper mapper)
        {
			_reservationRepository = reservationRepository;
			_hotelRepository = hotelRepository;
			_resturantRepository = resturantRepository;
			_mapper = mapper;
		}
		[HttpGet]
		[ProducesResponseType(200, Type = typeof(IEnumerable<Reservation>))]
		public IActionResult GetReservations()
		{
			var reserv = _mapper.Map<List<ReservationDto>>(_reservationRepository.GetReservations());
			if (!ModelState.IsValid)
				return BadRequest(ModelState);
			return Ok(reserv);
		}
		[HttpGet("{reservId}")]
		[ProducesResponseType(200, Type = typeof(Reservation))]
		[ProducesResponseType(400)]
		public IActionResult GetReservation(int reservId)
		{
			if (!_reservationRepository.ReservationExists(reservId))
				return NotFound();
			var reserv = _mapper.Map<ReservationDto>(_reservationRepository.ReservationExists(reservId));
			if (!ModelState.IsValid)
				return BadRequest(ModelState);
			return Ok(reserv);
		}
		[HttpPost]
		[ProducesResponseType(204)]
		[ProducesResponseType(400)]
		public IActionResult CreateReservation([FromQuery] string HotelName, [FromQuery] string RestName, [FromBody] ReservationDto reservcreate)
		{
			if (reservcreate == null)
				return BadRequest(ModelState);
			var reserv = _reservationRepository.GetReservations()
				.Where(a => a.Id == reservcreate.Id).FirstOrDefault();
			if (reserv != null)
			{
				ModelState.AddModelError("", "reservation already exists");
				return StatusCode(422, ModelState);
			}
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var reservMap = _mapper.Map<Reservation>(reservcreate);
			if (!_reservationRepository.CreateReservation(RestName, HotelName, reservMap))
			{
				ModelState.AddModelError("", "something went wrong while saving");
				return StatusCode(500, ModelState);
			}
			return Ok("Successfully created");
		}
		[HttpPut("{ReservId}")]
		[ProducesResponseType(400)]
		[ProducesResponseType(204)]
		[ProducesResponseType(404)]
		public IActionResult UpdateAirflight([FromQuery] int RestId, [FromQuery] int HotelId,
		  int ReservId, [FromBody] ReservationDto updatedReserv)
		{
			if (updatedReserv == null)
				return BadRequest(ModelState);

			if (ReservId != updatedReserv.Id)
				return BadRequest(ModelState);

			if (!_reservationRepository.ReservationExists(ReservId))
				return NotFound();

			if (!ModelState.IsValid)
				return BadRequest();

			var ReservMap = _mapper.Map<Reservation>(updatedReserv);

			if (!_reservationRepository.UpdateReservation(RestId,HotelId, ReservMap))
			{
				ModelState.AddModelError("", "something went wrong updating reservation");
				return StatusCode(500, ModelState);
			}
			return NoContent();
		}
		[HttpDelete("{ReservId}")]
		[ProducesResponseType(400)]
		[ProducesResponseType(204)]
		[ProducesResponseType(404)]
		public IActionResult DeleteRoom(int ReservId)
		{
			if (!_reservationRepository.ReservationExists(ReservId))
				return NotFound();
			var ReservToDelete = _reservationRepository.GetReservation(ReservId);
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			if (!_reservationRepository.DeleteReservation(ReservToDelete))
			{
				ModelState.AddModelError("", "SomeThing went wrong deleting reservation");
			}
			return NoContent();
		}
	}
}
