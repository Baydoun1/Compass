namespace Compass.Models
{
	public class User
	{
		public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Gender { get; set; }
        public City Nationality { get; set; }
        public int CityId { get; set; }
        public AppRate AppRate { get; set; }
        public ICollection<Car_User> Car_Users { get; set; }
    }
}
