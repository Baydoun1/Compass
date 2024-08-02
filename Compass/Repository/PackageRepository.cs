using AutoMapper;
using Compass.Data;
using Compass.Interfaces;
using Compass.Models;

namespace Compass.Repository
{
	public class PackageRepository : IPackageRepository
	{
		private readonly DataContext _context;
		private readonly IMapper _mapper;

		public PackageRepository(DataContext context,IMapper mapper)
        {
			_context = context;
			_mapper = mapper;
		}

		public bool CreatePackage(string resturantName, Package package)
		{
			var PackageRestEntity = _context.Resturants.Where(a => a.Name == resturantName).FirstOrDefault();
			var Package_Resturant = new Package_Resturant()
			{
				Resturant = PackageRestEntity,
				Package = package,
			};
			_context.Add(Package_Resturant);
			_context.Add(package);
			return Save();
		}

		public bool DeletePackage(Package package)
		{
			_context.Remove(package);
			return Save();
		}

		public bool FlightExists(int FlightId)
		{
			return _context.AirFlights.Any(p => p.Id == FlightId);
		}

		public Package GetPackage(int id)
		{
			return _context.Packages.Where(p => p.Id == id).FirstOrDefault();
		}


		public Package GetPackageOfAirflight(int airflightId)
		{
			return _context.AirFlights.Where(u => u.Id == airflightId).Select(c => c.Package).FirstOrDefault();
		}

		public ICollection<Package> GetPackages()
		{
			return _context.Packages.ToList();
		}

		public ICollection<Resturant> GetResturantOfAPacakage(int packageId)
		{
			return _context.Package_Resturants.Where(u => u.Package.Id == packageId).Select(c => c.Resturant).ToList();
		}

		public bool PackageExists(int packageId)
		{
			return _context.Packages.Any(p=>p.Id== packageId);
		}

		public bool Save()
		{
			var saved= _context.SaveChanges();
			return saved > 0 ? true : false;
		}

		public bool UpdatePackage( Package package)
		{
			_context.Update(package);
			return Save();
		}

		
	}
}
