namespace Compass.Models
{
	public class Car
	{
        public int Id { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public int HourPrice { get; set; }
        public string Type { get; set; }
        public ICollection<Car_User> Car_Users { get; set; }
    }
}
