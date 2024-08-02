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
	public class AirflightController : Controller
	{
		private readonly IAirflightRepository _airflightRepository;
		private readonly IAirportRepository _airportRepository;
		private readonly IAirlineRepository _airlineRepository;
		private readonly IPackageRepository _packageRepository;
		private readonly IFlightClassRepository _flightClassRepository;
		private readonly IMapper _mapper;

		public AirflightController(IAirflightRepository airflightRepository, IAirportRepository airportRepository
			, IAirlineRepository airlineRepository, IPackageRepository packageRepository, IFlightClassRepository flightClassRepository
			, IMapper mapper)
		{
			_airflightRepository = airflightRepository;
			_airportRepository = airportRepository;
			_airlineRepository = airlineRepository;
			_packageRepository = packageRepository;
			_flightClassRepository = flightClassRepository;
			_mapper = mapper;
		}
		[HttpGet]
		[ProducesResponseType(200, Type = typeof(IEnumerable<AirFlight>))]
		public IActionResult GetCities()
		{
			var flights = _mapper.Map<List<AirflightDto>>(_airflightRepository.GetAirFlights());
			if (!ModelState.IsValid)
				return BadRequest(ModelState);
			return Ok(flights);
		}
		[HttpGet("/airline/{lineId}")]
		[ProducesResponseType(400)]
		[ProducesResponseType(200, Type = typeof(AirFlight))]

		public IActionResult GetAirflightFromAirline(int lineId)
		{
			var flight = _mapper.Map<AirflightDto>(
				_airflightRepository.GetAirflightFromAirline(lineId));
			if (!ModelState.IsValid)
				return BadRequest();
			return Ok(flight);

		}
		[HttpGet("{flightId}")]
		[ProducesResponseType(200, Type = typeof(AirFlight))]
		[ProducesResponseType(400)]
		public IActionResult GetCity(int flightid)
		{
			if (!_airflightRepository.AirflightExists(flightid))
				return NotFound();
			var flight = _mapper.Map<CityDto>(_airflightRepository.AirflightExists(flightid));
			if (!ModelState.IsValid)
				return BadRequest(ModelState);
			return Ok(flight);
		}
		[HttpPost]
		[ProducesResponseType(204)]
		[ProducesResponseType(400)]
		public IActionResult CreateAirflight([FromQuery] int airlineId, [FromBody] AirflightDto airflightcreate)
		{
			if (airflightcreate == null)
				return BadRequest(ModelState);
			var airflight = _airflightRepository.GetAirFlights()
				.Where(a => a.Id == airflightcreate.Id).FirstOrDefault();
			if (airflight != null)
			{
				ModelState.AddModelError("", "airflight already exists");
				return StatusCode(422, ModelState);
			}
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var airflightMap = _mapper.Map<AirFlight>(airflightcreate);
			airflightMap.Airline = _airlineRepository.GetAirline(airlineId);
			if (!_airflightRepository.CreateAirflight(airflightMap))
			{
				ModelState.AddModelError("", "something went wrong while saving");
				return StatusCode(500, ModelState);
			}
			return Ok("Successfully created");
		}
		[HttpPut("{AirflightId}")]
		[ProducesResponseType(400)]
		[ProducesResponseType(204)]
		[ProducesResponseType(404)]
		public IActionResult UpdateAirflight(int AirflightId, [FromBody] AirflightDto updatedAirflight)
		{
			if (updatedAirflight == null)
				return BadRequest(ModelState);

			if (AirflightId != updatedAirflight.Id)
				return BadRequest(ModelState);

			if (!_airflightRepository.AirflightExists(AirflightId))
				return NotFound();

			if (!ModelState.IsValid)
				return BadRequest();

			// تحقق من أن AirlineId موجود
			if (!_airflightRepository.AirlineExists(updatedAirflight.AirlineId))
			{
				ModelState.AddModelError("", "Invalid AirlineId.");
				return BadRequest(ModelState);
			}

			var airflightMap = _mapper.Map<AirFlight>(updatedAirflight);

			if (!_airflightRepository.UpdateAirflight(airflightMap))
			{
				ModelState.AddModelError("", "something went wrong updating airflight");
				return StatusCode(500, ModelState);
			}
			return NoContent();
		}
		[HttpDelete("{AirflightId}")]
		[ProducesResponseType(400)]
		[ProducesResponseType(204)]
		[ProducesResponseType(404)]
			public IActionResult DeleteAirflight(int AirflightId)
			{
				if (!_airflightRepository.AirflightExists(AirflightId))
					return NotFound();
				var ClassToDelete=_flightClassRepository.GetClassesOfAFlight(AirflightId);
				var PortToDelete = _airportRepository.GetAirportOfAirflight(AirflightId);
				var PackageToDelete=_packageRepository.GetPackageOfAirflight(AirflightId);
				var FlightToDelete = _airflightRepository.GetAirFlight(AirflightId);

			if (!ModelState.IsValid)
					return BadRequest(ModelState);


			if (!_airportRepository.DeleteAirports(PortToDelete.ToList()))
			{
				ModelState.AddModelError("", "SomeThing went wrong deleting airport");
			}


			if (!_flightClassRepository.DeleteFlightClasses(ClassToDelete.ToList()))
			{
				ModelState.AddModelError("", "SomeThing went wrong deleting flightclasss");
			}


			if (!_packageRepository.DeletePackage(PackageToDelete))
			{
				ModelState.AddModelError("", "SomeThing went wrong deleting package");
			}

			if (!_airflightRepository.DeleteAirflight(FlightToDelete))
				{
					ModelState.AddModelError("", "SomeThing went wrong deleting airflight");
				}
				return NoContent();

			}
	} 
}
