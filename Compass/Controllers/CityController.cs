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
	public class CityController:Controller
	{
		private readonly ICityRepository _cityRepository;
		private readonly IResturantRepository _resturantRepository;
		private readonly IUserRepository _userRepository;
		private readonly IMapper _mapper;

		public CityController(ICityRepository cityRepository,IResturantRepository resturantRepository,IUserRepository userRepository,IMapper mapper)
        {
			_cityRepository = cityRepository;
			_resturantRepository = resturantRepository;
			_userRepository = userRepository;
			_mapper = mapper;
		}
		[HttpGet]
		[ProducesResponseType(200, Type = typeof(IEnumerable<City>))]
		public IActionResult GetCities()
		{
			var cities = _mapper.Map<List<CityDto>>(_cityRepository.GetCities());
			if (!ModelState.IsValid)
				return BadRequest(ModelState);
			return Ok(cities);
		}
		[HttpGet("{cityName}")]
		[ProducesResponseType(200, Type = typeof(City))]
		[ProducesResponseType(400)]
		public IActionResult GetCity(string cityName)
		{
			if (!_cityRepository.CityExists(cityName))
				return NotFound();
			var city = _mapper.Map<CityDto>(_cityRepository.GetCity(cityName));
			if (!ModelState.IsValid)
				return BadRequest(ModelState);
			return Ok(city);
		}

		[HttpGet("/users/{userId}")]
		[ProducesResponseType(400)]
		[ProducesResponseType(200,Type=typeof(City))]
		
		public IActionResult GetCityOfAnUser(int userId)
		{
			var city = _mapper.Map<CityDto>(
				_cityRepository.GetCityByUser(userId));
			if (!ModelState.IsValid)
				return BadRequest();
			return Ok(city);
		
		}

		[HttpPost]
		[ProducesResponseType(204)]
		[ProducesResponseType(400)]
		public IActionResult CreateCity([FromBody] CityDto citycreate)
		{
			if (citycreate == null)
				return BadRequest(ModelState);
			var city = _cityRepository.GetCities()
				.Where(a => a.Name.Trim().ToUpper() == citycreate.Name.TrimEnd().ToUpper())
				.FirstOrDefault();
			if (city != null)
			{
				ModelState.AddModelError("", "city already exists");
				return StatusCode(422, ModelState);
			}
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var cityMap = _mapper.Map<City>(citycreate);
			if (!_cityRepository.CreateCity(cityMap))
			{
				ModelState.AddModelError("", "something went wrong while saving");
				return StatusCode(500, ModelState);
			}
			return Ok("Successfully created");
		}
		
		[HttpPut("{CityId}")]
		[ProducesResponseType(400)]
		[ProducesResponseType(204)]
		[ProducesResponseType(404)]
		public IActionResult UpdateCity(int CityId, [FromBody] CityDto updatedCity)
		{
			if (updatedCity == null)
				return BadRequest(ModelState);

			if (CityId != updatedCity.Id)
				return BadRequest(ModelState);

			if (!_cityRepository.City1Exists(CityId))
				return NotFound();

			if (!ModelState.IsValid)
				return BadRequest();

			var CityMap = _mapper.Map<City>(updatedCity);

			if (!_cityRepository.UpdateCity(CityMap))
			{
				ModelState.AddModelError("", "something went wrong updating City");
				return StatusCode(500, ModelState);
			}
			return NoContent();
		}
		[HttpDelete("{cityId}")]
		[ProducesResponseType(400)]
		[ProducesResponseType(204)]
		[ProducesResponseType(404)]
		public IActionResult DeleteCity(int cityId)
		{
			if (!_cityRepository.City1Exists(cityId))
				return NotFound();
			var UserToDelete=_userRepository.Get1UserFromCity(cityId);
			var RestToDelete = _resturantRepository.GetResturantOfCity(cityId);
			var CityToDelete = _cityRepository.Get1City(cityId);
			
			 
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			if(!_userRepository.DeleteUser(UserToDelete))
			{
				ModelState.AddModelError("", "something went wrong when deleting users");
			}

			if (!_resturantRepository.DeleteRest(RestToDelete))
			{
				ModelState.AddModelError("", "SomeThing went wrong deleting resturant");
			}

			if (!_cityRepository.DeleteCity(CityToDelete))
			{ 
				ModelState.AddModelError("", "SomeThing went wrong deleting city");
		}
			return NoContent();

		}
	}
}
