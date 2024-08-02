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
	public class AirportController:Controller
	{
		private readonly IAirportRepository _airportRepository;
		private readonly IAirflightRepository _airflightRepository;
		private readonly ICityRepository _cityRepository;
		private readonly IMapper _mapper;

		public AirportController(IAirportRepository airportRepository,
			IAirflightRepository airflightRepository,ICityRepository cityRepository,IMapper mapper)
        {
			_airportRepository = airportRepository;
			_airflightRepository = airflightRepository;
			_cityRepository = cityRepository;
			_mapper = mapper;
		}
		[HttpGet]
		[ProducesResponseType(200, Type = typeof(IEnumerable<Airport>))]
		public IActionResult GetAirports()
		{
			var airports = _mapper.Map<List<AirportDto>>(_airportRepository.GetAirports());
			if (!ModelState.IsValid)
				return BadRequest(ModelState);
			return Ok(airports);
		}
		[HttpGet("{AirportName}")]
		[ProducesResponseType(200, Type = typeof(Airport))]
		[ProducesResponseType(400)]
		public IActionResult GetFlightClass(string AirportName)
		{
			if (!_airportRepository.AirportExists(AirportName))
				return NotFound();
			var airport = _mapper.Map<AirportDto>(_airportRepository.GetAirport(AirportName));
			if (!ModelState.IsValid)
				return BadRequest(ModelState);
			return Ok(airport);
		}
		[HttpGet("airflight/{airflightId}")]
		[ProducesResponseType(200, Type = typeof(Airport))]
		[ProducesResponseType(400)]
		public IActionResult GetAirportOfAirflight(int airflightId)
		{
			var port = _mapper.Map<List<AirportDto>>(_airportRepository.GetAirportOfAirflight(airflightId));
			if (!ModelState.IsValid)
				return BadRequest();
			return Ok(port);
		}
		[HttpPost]
		[ProducesResponseType(204)]
		[ProducesResponseType(400)]
		public IActionResult CreateAirport([FromQuery] int airflightId,[FromQuery] string cityName, [FromBody] AirportDto airportcreate)
		{
			if (airportcreate == null)
				return BadRequest(ModelState);
			var airport = _airportRepository.GetAirports()
				.Where(a => a.Id == airportcreate.Id).FirstOrDefault();
			if (airport != null)
			{
				ModelState.AddModelError("", "airport already exists");
				return StatusCode(422, ModelState);
			}
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var airportMap = _mapper.Map<Airport>(airportcreate);
			airportMap.City = _cityRepository.GetCity(cityName);
			airportMap.AirFlight = _airflightRepository.GetAirFlight(airflightId);
			if (!_airportRepository.CreateAirport(airportMap))
			{
				ModelState.AddModelError("", "something went wrong while saving");
				return StatusCode(500, ModelState);
			}
			return Ok("Successfully created");
		}
		[HttpPut("{AirportId}")]
		[ProducesResponseType(400)]
		[ProducesResponseType(204)]
		[ProducesResponseType(404)]
		public IActionResult UpdateAirflight(int AirportId, [FromBody] AirportDto updatedAirport)
		{
			if (updatedAirport == null)
				return BadRequest(ModelState);

			if (AirportId != updatedAirport.Id)
				return BadRequest(ModelState);

			if (!_airportRepository.Airport1Exists(AirportId))
				return NotFound();

			if (!ModelState.IsValid)
				return BadRequest();

			// تحقق من أن AirflightId موجود
			if (!_airportRepository.AirflightExists(updatedAirport.AirflightId))
			{
				ModelState.AddModelError("", "Invalid AirflightId.");
				return BadRequest(ModelState);
			}

			if (!_airportRepository.CityExists(updatedAirport.CityId))
			{
				ModelState.AddModelError("", "Invalid CityId.");
				return BadRequest(ModelState);
			}

			var airportMap = _mapper.Map<Airport>(updatedAirport);

			if (!_airportRepository.UpdateAirport(airportMap))
			{
				ModelState.AddModelError("", "something went wrong updating airport");
				return StatusCode(500, ModelState);
			}
			return NoContent();
		}
		[HttpDelete("{AirportName}")]
		[ProducesResponseType(400)]
		[ProducesResponseType(204)]
		[ProducesResponseType(404)]
		public IActionResult DeleteAirport(string AirportName)
		{
			if (!_airportRepository.AirportExists(AirportName))
				return NotFound();

			var PortToDelete = _airportRepository.GetAirport(AirportName);

			if (!ModelState.IsValid)
				return BadRequest(ModelState);


			if (!_airportRepository.DeleteAirport(PortToDelete))
			{
				ModelState.AddModelError("", "SomeThing went wrong deleting airport");
			}
			return NoContent();

		}
	}
}
