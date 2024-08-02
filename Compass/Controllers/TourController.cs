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
	public class TourController:Controller
	{
		private readonly ITourRepository _tourRepository;
		private readonly ICityRepository _cityRepository;
		private readonly ITourCategoryRepository _tourCategoryRepository;
		private readonly IPackageRepository _packageRepository;
		private readonly IMapper _mapper;

		public TourController(ITourRepository tourRepository
			,ICityRepository cityRepository,ITourCategoryRepository tourCategoryRepository,
			IPackageRepository packageRepository,IMapper mapper)
        {
			_tourRepository = tourRepository;
			_cityRepository = cityRepository;
			_tourCategoryRepository = tourCategoryRepository;
			_packageRepository = packageRepository;
			_mapper = mapper;
		}
		[HttpGet]
		[ProducesResponseType(200, Type = typeof(IEnumerable<Tour>))]
		public IActionResult GetTours()
		{
			var tours = _mapper.Map<List<TourDto>>(_tourRepository.GetTours());
			if (!ModelState.IsValid)
				return BadRequest(ModelState);
			return Ok(tours);
		}
		[HttpGet("{tourId}")]
		[ProducesResponseType(200, Type = typeof(Tour))]
		[ProducesResponseType(400)]
		public IActionResult GetTour(int tourId)
		{
			if (!_tourRepository.TourExists(tourId))
				return NotFound();
			var tour = _mapper.Map<TourDto>(_tourRepository.GetTour(tourId));
			if (!ModelState.IsValid)
				return BadRequest(ModelState);
			return Ok(tour);
		}
		[HttpGet("package/{packageId}")]
		[ProducesResponseType(200, Type = typeof(Tour))]
		[ProducesResponseType(400)]
		public IActionResult GetTourOfAPackage(int packageId)
		{
			var tour = _mapper.Map<List<TourDto>>(_tourRepository.GetTourOfAPackage(packageId));
			if (!ModelState.IsValid)
				return BadRequest();
			return Ok(tour);
		}
		[HttpPost]
		[ProducesResponseType(204)]
		[ProducesResponseType(400)]
		public IActionResult CreateTour([FromQuery] string cityName, [FromQuery] int packageId, [FromBody] TourDto tourcreate)
		{
			if (tourcreate == null)
				return BadRequest(ModelState);
			var tour = _tourRepository.GetTours()
				.Where(a => a.Id == tourcreate.Id).FirstOrDefault();
			if (tour != null)
			{
				ModelState.AddModelError("", "tour already exists");
				return StatusCode(422, ModelState);
			}
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var tourMap = _mapper.Map<Tour>(tourcreate);
			tourMap.City = _cityRepository.GetCity(cityName);
			tourMap.Package = _packageRepository.GetPackage(packageId);
			if (!_tourRepository.CreateTour(tourMap))
			{
				ModelState.AddModelError("", "something went wrong while saving");
				return StatusCode(500, ModelState);
			}
			return Ok("Successfully created");
		}
		[HttpPut("{TourId}")]
		[ProducesResponseType(400)]
		[ProducesResponseType(204)]
		[ProducesResponseType(404)]
		public IActionResult UpdateAirflight([FromQuery]int PackageId,int TourId, [FromBody] TourDto updatedTour)
		{
			if (updatedTour == null)
				return BadRequest(ModelState);

			if (TourId != updatedTour.Id)
				return BadRequest(ModelState);

			if (!_tourRepository.TourExists(TourId))
				return NotFound();

			if (!ModelState.IsValid)
				return BadRequest();

			// تحقق من أن CityId موجود
			if (!_cityRepository.City1Exists(updatedTour.CityId))
			{
				ModelState.AddModelError("", "Invalid CityId.");
				return BadRequest(ModelState);
			}

			if (!_packageRepository.PackageExists(updatedTour.PackId))
			{
				ModelState.AddModelError("", "Invalid PackId.");
				return BadRequest(ModelState);
			}

			var TourMap = _mapper.Map<Tour>(updatedTour);

			if (!_tourRepository.UpdateTour(PackageId,TourMap))
			{
				ModelState.AddModelError("", "something went wrong updating Tour");
				return StatusCode(500, ModelState);
			}
			return NoContent();
		}
		[HttpDelete("{TourId}")]
		[ProducesResponseType(400)]
		[ProducesResponseType(204)]
		[ProducesResponseType(404)]
		public IActionResult DeleteTour(int TourId)
		{
			if (!_tourRepository.TourExists(TourId))
				return NotFound();
			var TourToDelete = _tourRepository.GetTour(TourId);
			var CategToDelete = _tourCategoryRepository.GetCategoryOfTour(TourId);
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			if (!_tourCategoryRepository.DeleteCategory(CategToDelete))
			{
				ModelState.AddModelError("", "SomeThing went wrong deleting Category");
			}
			if (!_tourRepository.DeleteTour(TourToDelete))
			{
				ModelState.AddModelError("", "SomeThing went wrong deleting tour");
			}
			return NoContent();
		}
	}
}
