using AutoMapper;
using Compass.Data;
using Compass.Dto;
using Compass.Interfaces;
using Compass.Models;
using Compass.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Compass.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class FlightClassController:Controller
	{
		private readonly IFlightClassRepository _flightclassRepository;
		private readonly IAirflightRepository _airflightRepository;
		private readonly IMapper _mapper;

		public FlightClassController(IFlightClassRepository flightClassRepository,
			IAirflightRepository airflightRepository,IMapper mapper)
        {
			_flightclassRepository = flightClassRepository;
			_airflightRepository = airflightRepository;
			_mapper = mapper;
		}
		[HttpGet]
		[ProducesResponseType(200, Type = typeof(IEnumerable<FlightClass>))]
		public IActionResult GetClasses()
		{
			var classes = _mapper.Map<List<FlightClassDto>>(_flightclassRepository.GetFlightClasses());
			if (!ModelState.IsValid)
				return BadRequest(ModelState);
			return Ok(classes);
		}
		[HttpGet("{ClassId}")]
		[ProducesResponseType(200, Type = typeof(FlightClass))]
		[ProducesResponseType(400)]
		public IActionResult GetFlightClass(int ClassId)
		{
			if (!_flightclassRepository.ClassExists(ClassId))
				return NotFound();
			var classes = _mapper.Map<FlightClassDto>(_flightclassRepository.GetFlightClass(ClassId));
			if (!ModelState.IsValid)
				return BadRequest(ModelState);
			return Ok(classes);
		}
		[HttpGet("airflight/{AirflightId}")]
		[ProducesResponseType(200, Type = typeof(FlightClass))]
		[ProducesResponseType(400)]
		public IActionResult GetClassesOfAFlight(int flightId)
		{
			var classes = _mapper.Map<List<FlightClassDto>>(_flightclassRepository.GetClassesOfAFlight(flightId));
			if (!ModelState.IsValid)
				return BadRequest();
			return Ok(classes);
		}
		[HttpPost]
		[ProducesResponseType(204)]
		[ProducesResponseType(400)]
		public IActionResult CreateClass([FromQuery] int airflightId, [FromBody] FlightClassDto classcreate)
		{
			if (classcreate == null)
				return BadRequest(ModelState);
			var airflight = _flightclassRepository.GetFlightClasses()
				.Where(a => a.Id == classcreate.Id).FirstOrDefault();
			if (airflight != null)
			{
				ModelState.AddModelError("", "flightclass already exists");
				return StatusCode(422, ModelState);
			}
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var classMap = _mapper.Map<FlightClass>(classcreate);
			classMap.AirFlight = _airflightRepository.GetAirFlight(airflightId);
			if (!_flightclassRepository.CreateClass(classMap))
			{
				ModelState.AddModelError("", "something went wrong while saving");
				return StatusCode(500, ModelState);
			}
			return Ok("Successfully created");
		}
		[HttpPut("{ClassId}")]
		[ProducesResponseType(400)]
		[ProducesResponseType(204)]
		[ProducesResponseType(404)]
		public IActionResult UpdateAirflight(int ClassId, [FromBody] FlightClassDto updatedClass)
		{
			if (updatedClass == null)
				return BadRequest(ModelState);

			if (ClassId != updatedClass.Id)
				return BadRequest(ModelState);

			if (!_flightclassRepository.ClassExists(ClassId))
				return NotFound();

			if (!ModelState.IsValid)
				return BadRequest();

			// تحقق من أن AirlineId موجود
			if (!_airflightRepository.AirflightExists(updatedClass.FlightId))
			{
				ModelState.AddModelError("", "Invalid FlightId.");
				return BadRequest(ModelState);
			}

			var flightclassMap = _mapper.Map<FlightClass>(updatedClass);

			if (!_flightclassRepository.UpdateClass(flightclassMap))
			{
				ModelState.AddModelError("", "something went wrong updating Class");
				return StatusCode(500, ModelState);
			}
			return NoContent();
		}
		[HttpDelete("{FlightClassId}")]
		[ProducesResponseType(400)]
		[ProducesResponseType(204)]
		[ProducesResponseType(404)]
		public IActionResult DeleteFlightclass(int FlightClassId)
		{
			if (!_flightclassRepository.ClassExists(FlightClassId))
				return NotFound();

			var ClassToDelete = _flightclassRepository.GetFlightClass(FlightClassId);

			if (!ModelState.IsValid)
				return BadRequest(ModelState);


			if (!_flightclassRepository.DeleteFlightclass(ClassToDelete))
			{
				ModelState.AddModelError("", "SomeThing went wrong deleting city");
			}
			return NoContent();

		}
	}
}
