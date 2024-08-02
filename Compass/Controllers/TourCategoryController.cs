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
	public class TourCategoryController : Controller
	{
		private readonly ITourCategoryRepository _tourRepository;
		private readonly ITourRepository _tourRepository1;
		private readonly IMapper _mapper;

		public TourCategoryController(ITourCategoryRepository tourRepository,ITourRepository tourRepository1,IMapper mapper)
		{
			_tourRepository = tourRepository;
			_tourRepository1 = tourRepository1;
			_mapper = mapper;
		}
		[HttpGet]
		[ProducesResponseType(200, Type = typeof(IEnumerable<TourCategory>))]
		public IActionResult GetTourCategories()
		{
			var tourCategories = _mapper.Map<List<TourCategoryDto>>(_tourRepository.GetTourCategories());
			if (!ModelState.IsValid)
				return BadRequest(ModelState);
			return Ok(tourCategories);
		}

		[HttpGet("{CategoryId}")]
		[ProducesResponseType(200, Type = typeof(TourCategory))]
		[ProducesResponseType(400)]
		public IActionResult GetCategory(int Categoryid)
		{
			if (!_tourRepository.CategoryExists(Categoryid))
				return NotFound();
			var category = _mapper.Map<TourCategoryDto>(_tourRepository.GetTourCategory(Categoryid));
			if (!ModelState.IsValid)
				return BadRequest(ModelState);
			return Ok(category);
		}
		[HttpGet("tour/{categoryId}")]
		[ProducesResponseType(200, Type = typeof(IEnumerable<Tour>))]
		[ProducesResponseType(400)]
		public IActionResult GetTourByCategoryId(int categoryId)
		{
			var tours=_mapper.Map<List<TourCategoryDto>>(
				_tourRepository.GetTourByTourCategory(categoryId));
			if (!ModelState.IsValid)
				return BadRequest();
			return Ok(tours);
		}
		[HttpPost]
		[ProducesResponseType(204)]
		[ProducesResponseType(400)]
		public IActionResult CreateCategory([FromQuery] int tourId,[FromBody] TourCategoryDto categorycreate)
		{
			if (categorycreate == null)
				return BadRequest(ModelState);
			var city = _tourRepository.GetTourCategories()
				.Where(a => a.Name.Trim().ToUpper() == categorycreate.Name.TrimEnd().ToUpper())
				.FirstOrDefault();
			if (city != null)
			{
				ModelState.AddModelError("", "category already exists");
				return StatusCode(422, ModelState);
			}
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var categMap = _mapper.Map<TourCategory>(categorycreate);
			categMap.Tour = _tourRepository1.GetTour(tourId);
			if (!_tourRepository.CreateCategory(categMap))
			{
				ModelState.AddModelError("", "something went wrong while saving");
				return StatusCode(500, ModelState);
			}
			return Ok("Successfully created");
		}
		[HttpPut("{CategoryId}")]
		[ProducesResponseType(400)]
		[ProducesResponseType(204)]
		[ProducesResponseType(404)]
		public IActionResult UpdateAirflight(int CategoryId, [FromBody] TourCategoryDto updatedCategory)
		{
			if (updatedCategory == null)
				return BadRequest(ModelState);

			if (CategoryId != updatedCategory.Id)
				return BadRequest(ModelState);

			if (!_tourRepository.CategoryExists(CategoryId))
				return NotFound();

			if (!ModelState.IsValid)
				return BadRequest();

			if (!_tourRepository1.TourExists(updatedCategory.TourId))
			{
				ModelState.AddModelError("", "Invalid TourId.");
				return BadRequest(ModelState);
			}

			var CategoryMap = _mapper.Map<TourCategory>(updatedCategory);

			if (!_tourRepository.UpdateCategory(CategoryMap))
			{
				ModelState.AddModelError("", "something went wrong updating Category");
				return StatusCode(500, ModelState);
			}
			return NoContent();
		}
		[HttpDelete("{CategId}")]
		[ProducesResponseType(400)]
		[ProducesResponseType(204)]
		[ProducesResponseType(404)]
		public IActionResult DeleteCategory(int CategId)
		{
			if (!_tourRepository.CategoryExists(CategId))
				return NotFound();
			var CategToDelete = _tourRepository.GetTourCategory(CategId);
			//var PackageToDelete = _packageRepository.GetPackageFromResturant(RestId);
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			if (!_tourRepository.DeleteCategory(CategToDelete))
			{
				ModelState.AddModelError("", "SomeThing went wrong deleting category");
			}
			return NoContent();
		}
	}
}

