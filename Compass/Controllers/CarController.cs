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
	public class CarController : Controller
	{
		private readonly ICarRepository _carRepository;
		private readonly IMapper _mapper;

		public CarController(ICarRepository carRepository, IMapper mapper)
		{
			_carRepository = carRepository;
			_mapper = mapper;
		}
		[HttpGet]
		[ProducesResponseType(200, Type = typeof(IEnumerable<Car>))]
		public IActionResult GetCars()
		{
			var cars = _mapper.Map<List<CarDto>>(_carRepository.GetCars());
			if (!ModelState.IsValid)
				return BadRequest(ModelState);
			return Ok(cars);
		}
		[HttpGet("{CarId}")]
		[ProducesResponseType(200, Type = typeof(Car))]
		[ProducesResponseType(400)]
		public IActionResult GetCar(int CarId)
		{
			if (!_carRepository.CarExists(CarId))
				return NotFound();
			var car = _mapper.Map<CarDto>(_carRepository.GetCar(CarId));
			if (!ModelState.IsValid)
				return BadRequest(ModelState);
			return Ok(car);
		}
		[HttpGet("{CarId}/User")]
		[ProducesResponseType(200, Type = typeof(Car))]
		[ProducesResponseType(400)]
		public IActionResult GetUsersBycar(int CarId)
		{
			if (!_carRepository.CarExists(CarId))
			{
				return NotFound();
			}
			//getting pokemon by owner so we're gonna be returning the pokemondto
			var car = _mapper.Map<List<UserDto>>(_carRepository.GetUserByCar(CarId));

			if (!ModelState.IsValid)
				return BadRequest(ModelState);
			return Ok(car);
		}
		[HttpPut("{CarId}")]
		[ProducesResponseType(400)]
		[ProducesResponseType(204)]
		[ProducesResponseType(404)]
		public IActionResult UpdateAirflight(int CarId, [FromQuery] int UserId, [FromBody] CarDto updatedCar)
		{
			if (updatedCar == null)
				return BadRequest(ModelState);

			if (CarId != updatedCar.Id)
				return BadRequest(ModelState);

			if (!_carRepository.CarExists(CarId))
				return NotFound();

			if (!ModelState.IsValid)
				return BadRequest();

			var CarMap = _mapper.Map<Car>(updatedCar);

			if (!_carRepository.UpdateCar(UserId, CarMap))
			{
				ModelState.AddModelError("", "something went wrong updating airflight");
				return StatusCode(500, ModelState);
			}
			return NoContent();
		}
		[HttpPost]
		[ProducesResponseType(204)]
		[ProducesResponseType(400)]
		public IActionResult CreateCar([FromBody] CarDto carcreate)
		{
			if (carcreate == null)
				return BadRequest(ModelState);
			var city = _carRepository.GetCars()
				.Where(a => a.Name.Trim().ToUpper() == carcreate.Name.TrimEnd().ToUpper())
				.FirstOrDefault();
			if (city != null)
			{
				ModelState.AddModelError("", "car already exists");
				return StatusCode(422, ModelState);
			}
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var carMap = _mapper.Map<Car>(carcreate);
			if (!_carRepository.CreateCar(carMap))
			{
				ModelState.AddModelError("", "something went wrong while saving");
				return StatusCode(500, ModelState);
			}
			return Ok("Successfully created");
		}
		[HttpDelete("{CarId}")]
		[ProducesResponseType(400)]
		[ProducesResponseType(204)]
		[ProducesResponseType(404)]
		public IActionResult DeleteCar(int CarId)
		{
			if (!_carRepository.CarExists(CarId))
				return NotFound();
			var CarToDelete = _carRepository.GetCar(CarId);
			//var PackageToDelete = _packageRepository.GetPackageFromResturant(RestId);
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			if (!_carRepository.DeleteCar(CarToDelete))
			{
				ModelState.AddModelError("", "SomeThing went wrong deleting car");
			}
			return NoContent();
		}
	}
};
