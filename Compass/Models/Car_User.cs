namespace Compass.Models
{
	public class Car_User
	{
        public int CarId { get; set; }
        public int UserId { get; set; }
        public Car Car { get; set; }
        public User User { get; set; }
    }
}
