using Microsoft.AspNetCore.SignalR;

namespace Compass.Dto
{
	public class UserDto
	{
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public string Gender { get; set; }
		public int CityId {  get; set; }
	}
}
