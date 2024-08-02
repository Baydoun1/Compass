namespace Compass.Models
{
	public class Package_Resturant
	{
        public int PackageId { get; set; }
        public int ResturantId { get; set; }
        public Package Package { get; set; }
        public Resturant Resturant { get; set; }
    }
}
