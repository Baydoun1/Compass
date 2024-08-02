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
	public class AirlineController : Controller
	{
		private readonly IAirlineRepository _airlineRepository;
		private readonly IAirflightRepository _airflightRepository;
		private readonly IMapper _mapper;

		public AirlineController(IAirlineRepository airlineRepository,IAirflightRepository airflightRepository, IMapper mapper)
		{
			_airlineRepository = airlineRepository;
			_airflightRepository = airflightRepository;
			_mapper = mapper;
		}
		[HttpGet]
		[ProducesResponseType(200, Type = typeof(IEnumerable<Airline>))]
		public IActionResult GetAirlines()
		{
			var airlines = _mapper.Map<List<AirlineDto>>(_airlineRepository.GetAirlines());
			if (!ModelState.IsValid)
				return BadRequest(ModelState);
			return Ok(airlines);
		}
		[HttpGet("{airlineId}")]
		[ProducesResponseType(200, Type = typeof(Airline))]
		[ProducesResponseType(400)]
		public IActionResult GetAirline(int airlineId)
		{
			if (!_airlineRepository.AirlineExists(airlineId))
				return NotFound();
			var airline = _mapper.Map<AirlineDto>(_airlineRepository.GetAirline(airlineId));
			if (!ModelState.IsValid)
				return BadRequest(ModelState);
			return Ok(airline);
		}
		[HttpPost]
		[ProducesResponseType(204)]
		[ProducesResponseType(400)]
		public IActionResult CreateAirline([FromBody] AirlineDto aircreate)
		{
			if (aircreate == null)
				return BadRequest(ModelState);
			var city = _airlineRepository.GetAirlines()
				.Where(a => a.Name.Trim().ToUpper() == aircreate.Name.TrimEnd().ToUpper())
				.FirstOrDefault();
			if (city != null)
			{
				ModelState.AddModelError("", "airline already exists");
				return StatusCode(422, ModelState);
			}
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var airMap = _mapper.Map<Airline>(aircreate);
			if (!_airlineRepository.CreateAirline(airMap))
			{
				ModelState.AddModelError("", "something went wrong while saving");
				return StatusCode(500, ModelState);
			}
			return Ok("Successfully created");
		}
		[HttpPut("{AirlineId}")]
		[ProducesResponseType(400)]
		[ProducesResponseType(204)]
		[ProducesResponseType(404)]
		public IActionResult UpdateAirflight(int AirlineId, [FromBody] AirlineDto updatedAirline)
		{
			if (updatedAirline == null)
				return BadRequest(ModelState);

			if (AirlineId != updatedAirline.Id)
				return BadRequest(ModelState);

			if (!_airlineRepository.AirlineExists(AirlineId))
				return NotFound();

			if (!ModelState.IsValid)
				return BadRequest();

			var airlineMap = _mapper.Map<Airline>(updatedAirline);

			if (!_airlineRepository.UpdateAirline(airlineMap))
			{
				ModelState.AddModelError("", "something went wrong updating airline");
				return StatusCode(500, ModelState);
			}
			return NoContent();
		}
		[HttpDelete("{AirlineId}")]
		[ProducesResponseType(400)]
		[ProducesResponseType(204)]
		[ProducesResponseType(404)]
		public IActionResult DeleteAirflight(int AirlineId)
		{
			if (!_airlineRepository.AirlineExists(AirlineId))
				return NotFound();

			var FlightToDelete = _airflightRepository.GetAirflightFromAirline(AirlineId);
			var LineToDelete = _airlineRepository.GetAirline(AirlineId);

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			if (!_airflightRepository.DeleteAirflight(FlightToDelete))
			{
				ModelState.AddModelError("", "something went wrong when deleting users");
			}



			if (!_airlineRepository.DeleteAirline(LineToDelete))
			{
				ModelState.AddModelError("", "SomeThing went wrong deleting Airline");
			}
			return NoContent();

		}
	}
}
