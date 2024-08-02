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
	public class AppRateController:Controller
	{
		private readonly IAppRateRepository _appRateRepository;
		private readonly IUserRepository _userRepository;
		private readonly IMapper _mapper;

		public AppRateController(IAppRateRepository appRateRepository,IUserRepository userRepository,IMapper mapper)
        {
			_appRateRepository = appRateRepository;
			_userRepository = userRepository;
			_mapper = mapper;
		}
		[HttpGet]
		[ProducesResponseType(200, Type = typeof(IEnumerable<AppRate>))]
		public IActionResult GetRates()
		{
			var rates = _mapper.Map<List<AppRateDto>>(_appRateRepository.GetAppRates());
			if (!ModelState.IsValid)
				return BadRequest(ModelState);
			return Ok(rates);
		}
		[HttpGet("{RateId}")]
		[ProducesResponseType(200, Type = typeof(AppRate))]
		[ProducesResponseType(400)]
		public IActionResult GetAppRate(int RateId)
		{
			if (!_appRateRepository.RateExists(RateId))
				return NotFound();
			var rate = _mapper.Map<AppRateDto>(_appRateRepository.GetAppRate(RateId));
			if (!ModelState.IsValid)
				return BadRequest(ModelState);
			return Ok(rate);
		}
		[HttpGet("user/{userId}")]
		[ProducesResponseType(200, Type = typeof(AppRate))]
		[ProducesResponseType(400)]
		public IActionResult GetRatesForAUser(int userId)
		{
			var rate=_mapper.Map<List<AppRateDto>>(_appRateRepository.GetRatesOfAUser(userId));
			if (!ModelState.IsValid)
				return BadRequest();
			return Ok(rate);
		}
		[HttpPut("{AppRateId}")]
		[ProducesResponseType(400)]
		[ProducesResponseType(204)]
		[ProducesResponseType(404)]
		public IActionResult UpdateAirflight(int AppRateId, [FromBody] AppRateDto updatedApprate)
		{
			if (updatedApprate == null)
				return BadRequest(ModelState);

			if (AppRateId != updatedApprate.Id)
				return BadRequest(ModelState);

			if (!_appRateRepository.RateExists(AppRateId))
				return NotFound();

			if (!ModelState.IsValid)
				return BadRequest();

			var apprateMap = _mapper.Map<AppRate>(updatedApprate);

			if (!_appRateRepository.UpdateRate(apprateMap))
			{
				ModelState.AddModelError("", "something went wrong updating App Rate");
				return StatusCode(500, ModelState);
			}
			return NoContent();
		}
		[HttpPost]
		[ProducesResponseType(204)]
		[ProducesResponseType(400)]
		public IActionResult CreateRate([FromQuery] int userId,[FromBody] AppRateDto ratecreate)
		{
			if (ratecreate == null)
				return BadRequest(ModelState);
			var rate = _appRateRepository.GetAppRates()
				.Where(a => a.Id == ratecreate.Id)
				.FirstOrDefault();
			if (rate != null)
			{
				ModelState.AddModelError("", "rate already exists");
				return StatusCode(422, ModelState);
			}
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var rateMap = _mapper.Map<AppRate>(ratecreate);
			rateMap.User = _userRepository.GetUser(userId);
			if (!_appRateRepository.CreateRate(rateMap))
			{
				ModelState.AddModelError("", "something went wrong while saving");
				return StatusCode(500, ModelState);
			}
			return Ok("Successfully created");
		}
		[HttpDelete("{ApprateId}")]
		[ProducesResponseType(400)]
		[ProducesResponseType(204)]
		[ProducesResponseType(404)]
		public IActionResult DeleteRate(int ApprateId)
		{
			if (!_appRateRepository.RateExists(ApprateId))
				return NotFound();
			var RateToDelete = _appRateRepository.GetAppRate(ApprateId);

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			if (!_appRateRepository.DeleteRate(RateToDelete))
			{
				ModelState.AddModelError("", "SomeThing went wrong deleting city");
			}
			return NoContent();

		}
	}
}
