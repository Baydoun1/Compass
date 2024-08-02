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
	public class ResturantController:Controller
	{
		private readonly IResturantRepository _resturantRepository;
		private readonly IReservationRepository _reservationRepository;
		private readonly IPackageRepository _packageRepository;
		private readonly ICityRepository _cityRepository;
		private readonly IMapper _mapper;

		public ResturantController(IResturantRepository resturantRepository,IReservationRepository reservationRepository,IPackageRepository packageRepository,ICityRepository cityRepository,IMapper mapper)
        {
			_resturantRepository = resturantRepository;
			_reservationRepository = reservationRepository;
			_packageRepository = packageRepository;
			_cityRepository = cityRepository;
			_mapper = mapper;
		}
		[HttpGet]
		[ProducesResponseType(200, Type = typeof(IEnumerable<Resturant>))]
		public IActionResult GetResturants()
		{
			var rests = _mapper.Map<List<ResturantDto>>(_resturantRepository.GetResturants());
			if (!ModelState.IsValid)
				return BadRequest(ModelState);
			return Ok(rests);
		}
		[HttpGet("{Resturantname}")]
		[ProducesResponseType(200, Type = typeof(Resturant))]
		[ProducesResponseType(400)]
		public IActionResult GetCar(string Resturantname)
		{
			if (!_resturantRepository.ResturantExists(Resturantname))
				return NotFound();
			var Rest = _mapper.Map<ResturantDto>(_resturantRepository.GetResturant(Resturantname));
			if (!ModelState.IsValid)
				return BadRequest(ModelState);
			return Ok(Rest);
		}
		/*[HttpGet("/city/{cityName}")]
		[ProducesResponseType(400)]
		[ProducesResponseType(200, Type = typeof(Resturant))]

		public IActionResult GetResturantOfCity(int cityName)
		{
			var restu = _mapper.Map<ResturantDto>(
				_resturantRepository.GetResturantOfCity(cityName));
			if (!ModelState.IsValid)
				return BadRequest();
			return Ok(restu);

		}*/
		[HttpGet("{restName}/reservation")]
		[ProducesResponseType(200, Type = typeof(Resturant))]
		[ProducesResponseType(400)]
		public IActionResult GetReservationOfAResturant(string restName)
		{
			if (!_resturantRepository.ResturantExists(restName))
			{
				return NotFound();
			}
			//getting pokemon by owner so we're gonna be returning the pokemondto
			var rest = _mapper.Map<List<ResturantDto>>(_resturantRepository.GetReservationOfAResturant(restName));

			if (!ModelState.IsValid)
				return BadRequest(ModelState);
			return Ok(rest);
		}
		[HttpPut("{RestId}")]
		[ProducesResponseType(400)]
		[ProducesResponseType(204)]
		[ProducesResponseType(404)]
		public IActionResult UpdateRest(int RestId, [FromBody] ResturantDto updatedResturant)
		{
			if (updatedResturant == null)
				return BadRequest(ModelState);

			if (RestId != updatedResturant.Id)
				return BadRequest(ModelState);

			if (!_resturantRepository.Resturant1Exists(RestId))
				return NotFound();

			if (!ModelState.IsValid)
				return BadRequest();

			// تحقق من أن AirlineId موجود
			if (!_cityRepository.City1Exists(updatedResturant.CityId))
			{
				ModelState.AddModelError("", "Invalid CityId.");
				return BadRequest(ModelState);
			}

			var restMap = _mapper.Map<Resturant>(updatedResturant);

			if (!_resturantRepository.UpdateResturant(restMap))
			{
				ModelState.AddModelError("", "something went wrong updating Resturant");
				return StatusCode(500, ModelState);
			}
			return NoContent();
		}
		[HttpPost]
		[ProducesResponseType(204)]
		[ProducesResponseType(400)]
		public IActionResult CreateResturnat([FromQuery] string cityName, [FromBody] ResturantDto restucreate)
		{
			if (restucreate == null)
				return BadRequest(ModelState);
			var restur = _resturantRepository.GetResturants()
				.Where(a => a.Name.Trim().ToUpper() == restucreate.Name.TrimEnd().ToUpper())
				.FirstOrDefault();
			if (restur != null)
			{
				ModelState.AddModelError("", "resturnat already exists");
				return StatusCode(422, ModelState);
			}
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var restuMap = _mapper.Map<Resturant>(restucreate);
			restuMap.City = _cityRepository.GetCity(cityName);
			if (!_resturantRepository.CreateResturant(restuMap))
			{
				ModelState.AddModelError("", "something went wrong while saving");
				return StatusCode(500, ModelState);
			}
			return Ok("Successfully created");
		}
		[HttpDelete("{RestId}")]
		[ProducesResponseType(400)]
		[ProducesResponseType(204)]
		[ProducesResponseType(404)]
		public IActionResult DeleteRest(int RestId)
		{
			if (!_resturantRepository.Resturant1Exists(RestId))
				return NotFound();
			var ReservToDelete = _reservationRepository.GetReservationOfAResturant(RestId);
			var RestToDelete = _resturantRepository.Get1Resturant(RestId);
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			if (!_reservationRepository.DeleteReservations(ReservToDelete.ToList()))
			{
				ModelState.AddModelError("", "SomeThing went wrong deleting reservation");
			}
			if (!_resturantRepository.DeleteRest(RestToDelete))
			{
				ModelState.AddModelError("", "SomeThing went wrong deleting resturant");
			}
			return NoContent();

		}
	}
	
}//hotel @ this must be edited
