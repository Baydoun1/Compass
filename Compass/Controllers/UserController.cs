using System.Collections.Generic;
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
	public class UserController : Controller
	{
		private readonly IUserRepository _userRepository;
		private readonly ICityRepository _cityRepository;
		private readonly IMapper _mapper;

		public UserController(IUserRepository userRepository,ICityRepository cityRepository, IMapper mapper)
		{
			_userRepository = userRepository;
			_cityRepository = cityRepository;
			_mapper = mapper;
		}
		[HttpGet]
		[ProducesResponseType(200,Type=typeof(IEnumerable<User>))]
		public IActionResult GetUsers()
		{
			var users= _mapper.Map<List<UserDto>>(_userRepository.GetUsers());
			if (!ModelState.IsValid)
				return BadRequest(ModelState);
			return Ok(users);
		}
		
		[HttpGet("{UserId}/rating")]
		[ProducesResponseType(200, Type = typeof(decimal))]
		[ProducesResponseType(400)]
		public IActionResult GetUserRating(int UserId)
		{
			if(!_userRepository.UserExists(UserId))
				return NotFound();
			var rating=_userRepository.GetUserRating(UserId);
			if(!ModelState.IsValid)
				return BadRequest();
			return Ok(rating);
		}
		[HttpGet("/city/{cityName}")]
		[ProducesResponseType(400)]
		[ProducesResponseType(200, Type = typeof(User))]

		public IActionResult GetUserFromCity(string cityName)
		{
			var user = _mapper.Map<UserDto>(
				_userRepository.GetUserFromCity(cityName));
			if (!ModelState.IsValid)
				return BadRequest();
			return Ok(user);

		}
		[HttpPost]
		[ProducesResponseType(204)]
		[ProducesResponseType(400)]
		public IActionResult CreateUser([FromQuery] string cityName,[FromBody] UserDto usercreate)
		{
			if (usercreate == null)
				return BadRequest(ModelState);
			var user = _userRepository.GetUsers()
				.Where(a => a.FirstName.Trim().ToUpper() == usercreate.FirstName.TrimEnd().ToUpper())
				.FirstOrDefault();
			if (user != null)
			{
				ModelState.AddModelError("", "user already exists");
				return StatusCode(422, ModelState);
			}
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var userMap = _mapper.Map<User>(usercreate);
			userMap.Nationality = _cityRepository.GetCity(cityName);
			if (!_userRepository.CreateUser(userMap))
			{
				ModelState.AddModelError("", "something went wrong while saving");
				return StatusCode(500, ModelState);
			}
			return Ok("Successfully created");
		}
		[HttpPut("{UserId}")]
		[ProducesResponseType(400)]
		[ProducesResponseType(204)]
		[ProducesResponseType(404)]
		public IActionResult UpdateAirflight(int UserId, [FromBody] UserDto updatedUser)
		{
			if (updatedUser == null)
				return BadRequest(ModelState);

			if (UserId != updatedUser.Id)
				return BadRequest(ModelState);

			if (!_userRepository.UserExists(UserId))
				return NotFound();

			if (!ModelState.IsValid)
				return BadRequest();

			// تحقق من أن CityId موجود
			if (!_cityRepository.City1Exists(updatedUser.CityId))
			{
				ModelState.AddModelError("", "Invalid CityId.");
				return BadRequest(ModelState);
			}
			/*
			if (!_packageRepository.PackageExists(updatedUser.PackId))
			{
				ModelState.AddModelError("", "Invalid PackId.");
				return BadRequest(ModelState);
			}*/

			var UserMap = _mapper.Map<User>(updatedUser);

			if (!_userRepository.UpdateUser(UserMap))
			{
				ModelState.AddModelError("", "something went wrong updating User");
				return StatusCode(500, ModelState);
			}
			return NoContent();
		}
		[HttpDelete("{UserId}")]
		[ProducesResponseType(400)]
		[ProducesResponseType(204)]
		[ProducesResponseType(404)]
		public IActionResult DeleteUser(int UserId)
		{
			if (!_userRepository.UserExists(UserId))
				return NotFound();

			var UserToDelete = _userRepository.GetUser(UserId);

			if (!ModelState.IsValid)
				return BadRequest(ModelState);


			if (!_userRepository.DeleteUser(UserToDelete))
			{
				ModelState.AddModelError("", "SomeThing went wrong deleting city");
			}
			return NoContent();

		}
	}
}
