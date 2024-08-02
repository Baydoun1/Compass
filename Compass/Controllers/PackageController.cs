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
	public class PackageController:Controller
	{
		private readonly IPackageRepository _packageRepository;
		private readonly IAirflightRepository _airflightRepository;
		private readonly IResturantRepository _resturantRepository;
		private readonly ITourRepository _tourRepository;
		private readonly IHotelRepository _hotelRepository;
		private readonly IMapper _mapper;

		public PackageController(IPackageRepository packageRepository,
			IAirflightRepository airflightRepository,IResturantRepository resturantRepository
			, ITourRepository tourRepository, IHotelRepository hotelRepository, IMapper mapper)
        {
			_packageRepository = packageRepository;
			_airflightRepository = airflightRepository;
			_resturantRepository = resturantRepository;
			_tourRepository = tourRepository;
			_hotelRepository = hotelRepository;
			_mapper = mapper;
		}
		[HttpGet]
		[ProducesResponseType(200, Type = typeof(IEnumerable<Package>))]
		public IActionResult GetPacakges()
		{
			var package = _mapper.Map<List<PackageDto>>(_packageRepository.GetPackages());
			if (!ModelState.IsValid)
				return BadRequest(ModelState);
			return Ok(package);
		}
		[HttpGet("{packageId}")]
		[ProducesResponseType(200, Type = typeof(Package))]
		[ProducesResponseType(400)]
		public IActionResult GetCar(int packageId)
		{
			if (!_packageRepository.PackageExists(packageId))
				return NotFound();
			var package = _mapper.Map<CarDto>(_packageRepository.GetPackage(packageId));
			if (!ModelState.IsValid)
				return BadRequest(ModelState);
			return Ok(package);
		}
		[HttpGet("{packageId}/resturant")]
		[ProducesResponseType(200, Type = typeof(Package))]
		[ProducesResponseType(400)]
		public IActionResult GetResturantOfAPacakage(int packageId)
		{
			if (!_packageRepository.PackageExists(packageId))
			{
				return NotFound();
			}
			//getting pokemon by owner so we're gonna be returning the pokemondto
			var package = _mapper.Map<List<PackageDto>>(_packageRepository.GetResturantOfAPacakage(packageId));

			if (!ModelState.IsValid)
				return BadRequest(ModelState);
			return Ok(package);
		}
		[HttpPut("{PacakgeId}")]
		[ProducesResponseType(400)]
		[ProducesResponseType(204)]
		[ProducesResponseType(404)]
		public IActionResult UpdateAirflight(int PacakgeId, [FromBody] PackageDto updatedPackage)
		{
			if (updatedPackage == null)
				return BadRequest(ModelState);

			if (PacakgeId != updatedPackage.Id)
				return BadRequest(ModelState);

			if (!_packageRepository.PackageExists(PacakgeId))
				return NotFound();

			if (!ModelState.IsValid)
				return BadRequest();

			// تحقق من أن AirlineId موجود
			if (!_airflightRepository.AirflightExists(updatedPackage.FlightId))
			{
				ModelState.AddModelError("", "Invalid FlightId.");
				return BadRequest(ModelState);
			}

			if (!_hotelRepository.HotelExists(updatedPackage.HotelName))
			{
				ModelState.AddModelError("", "Invalid HotelId.");
				return BadRequest(ModelState);
			}

			// تحقق من أن Tourism_PlaceId موجود
			if (!_tourRepository.TourExists(updatedPackage.TourId))
			{
				ModelState.AddModelError("", "Invalid TourId.");
				return BadRequest(ModelState);
			}

			// تحقق من أن ResturantId موجود
			if (!_resturantRepository.ResturantExists(updatedPackage.RestName))
			{
				ModelState.AddModelError("", "Invalid RestName.");
				return BadRequest(ModelState);
			}

			var packageMap = _mapper.Map<Package>(updatedPackage);

			if (!_packageRepository.UpdatePackage(packageMap))
			{
				ModelState.AddModelError("", "something went wrong updating package");
				return StatusCode(500, ModelState);
			}
			return NoContent();
		}
		[HttpPost]
		[ProducesResponseType(204)]
		[ProducesResponseType(400)]
		public IActionResult CreatePackage([FromQuery] int airflightId, [FromQuery] string restName, [FromBody] PackageDto packagecreate)
		{
			if (packagecreate == null)
				return BadRequest(ModelState);
			var package = _packageRepository.GetPackages()
				.Where(a => a.Id == packagecreate.Id).FirstOrDefault();
			if (package != null)
			{
				ModelState.AddModelError("", "package already exists");
				return StatusCode(422, ModelState);
			}
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var packageMap = _mapper.Map<Package>(packagecreate);
			packageMap.AirFlight = _airflightRepository.GetAirFlight(airflightId);
			if (!_packageRepository.CreatePackage(restName,packageMap))
			{
				ModelState.AddModelError("", "something went wrong while saving");
				return StatusCode(500, ModelState);
			}
			return Ok("Successfully created");
		}

		[HttpDelete("{PackId}")]
		[ProducesResponseType(400)]
		[ProducesResponseType(204)]
		[ProducesResponseType(404)]
		public IActionResult DeletePackage(int PackId)
		{
			if (!_packageRepository.PackageExists(PackId))
				return NotFound();
			var FlightToDelete=_airflightRepository.GetFlightFromPackage(PackId);
			var RestToDelete=_resturantRepository.GetResturantOfAPackage(PackId);
			var PackToDelete = _packageRepository.GetPackage(PackId);
			var HotToDelete = _hotelRepository.GetHotelOfPackage(PackId);
			var TourToDelete=_tourRepository.GetTourOfAPackage(PackId);
			if (!ModelState.IsValid)
				return BadRequest(ModelState);
			if (!_airflightRepository.DeleteAirflight(FlightToDelete))
			{
				ModelState.AddModelError("", "something went wrong when deleting airflight");
			}
			if (!_resturantRepository.DeleteRests(RestToDelete.ToList()))
			{
				ModelState.AddModelError("", "something went wrong when deleting resturant");
			}
			if (!_hotelRepository.DeleteHotel(HotToDelete))
			{
				ModelState.AddModelError("", "something went wrong when deleting Hotel");
			}
			if (!_tourRepository.DeleteTours(TourToDelete.ToList()))
			{
				ModelState.AddModelError("", "something went wrong when deleting tours");
			}
			if (!_packageRepository.DeletePackage(PackToDelete))
			{
				ModelState.AddModelError("", "SomeThing went wrong deleting resturant");
			}
			return NoContent();

		}
	}
}
