namespace Compass.Models
{
	public class AppRate
	{
        public int Id { get; set; }
        public float Rate { get; set; }
        public string FeedBack { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
    }
}
