using Compass.Models;
using Microsoft.EntityFrameworkCore;

namespace Compass.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<AirFlight> AirFlights { get; set; }
        public DbSet<Airline> Airlines { get; set; }
        public DbSet<Airport> Airports { get; set; }
        public DbSet<AppRate> AppRates { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<TourCategory> TourCategories { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<FlightClass> FlightClasses { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<HotelRoom> HotelRooms { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<Resturant> Resturants { get; set; }
        public DbSet<Tour> Tourism_Places { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Hotel_Reservation> Hotel_Reservations { get; set; }
        public DbSet<Resturant_Reservation> Resturant_Reservations { get; set; }
        public DbSet<Car_User> Car_Users { get; set; }
        public DbSet<Package_Resturant> Package_Resturants { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			//one to many relationships
			modelBuilder.Entity<Airport>()
		   .HasOne(p => p.AirFlight)
		   .WithMany(af => af.Airports)
		   .HasForeignKey(p => p.AirFlightId)
		   .OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<FlightClass>()
		   .HasOne(p => p.AirFlight)
		   .WithMany(af => af.FlightClasses)
		   .HasForeignKey(p => p.AirFlightId)
		   .OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<HotelRoom>()
		   .HasOne(p => p.Hotel)
		   .WithMany(af => af.HotelRooms)
		   .HasForeignKey(p => p.HotelId)
		   .OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<Tour>()
		   .HasOne(p => p.Package)
		   .WithMany(af => af.Tours)
		   .HasForeignKey(p => p.PackageId)
		   .OnDelete(DeleteBehavior.Restrict);

		

			//one to one relationships

			modelBuilder.Entity<City>()
		   .HasOne(cto => cto.Tour)
		   .WithOne(tot => tot.City)
		   .HasForeignKey<Tour>(tot => tot.CityId)
		   .OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<City>()
		   .HasOne(ctu => ctu.User)
		   .WithOne(tct => tct.Nationality)
		   .HasForeignKey<User>(tct => tct.CityId)
		   .OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<Tour>()
		   .HasOne(ttc => ttc.TourCategory)
		   .WithOne(tt => tt.Tour)
		   .HasForeignKey<TourCategory>(tt => tt.TourId)
		   .OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<City>()
		   .HasOne(cre => cre.Resturant)
		   .WithOne(prp => prp.City)
		   .HasForeignKey<Resturant>(prp => prp.CityId)
		   .OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<City>()
		   .HasOne(cho => cho.Hotel)
		   .WithOne(pcp => pcp.City)
		   .HasForeignKey<Hotel>(pcp => pcp.CityId)
		   .OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<Package>()
		   .HasOne(pho => pho.Hotel)
		   .WithOne(php => php.package)
		   .HasForeignKey<Hotel>(php => php.PackageId)
		   .OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<AirFlight>()
		   .HasOne(afp => afp.Package)
		   .WithOne(pap => pap.AirFlight)
		   .HasForeignKey<Package>(pap => pap.AirFlightId)
		   .OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<Airline>()
		   .HasOne(aff => aff.AirFlight)
		   .WithOne(ppp => ppp.Airline)
		   .HasForeignKey<AirFlight>(ppp => ppp.AirlineId)
		   .OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<City>()
		   .HasOne(ca => ca.Airport)
		   .WithOne(cc => cc.City)
		   .HasForeignKey<Airport>(cc => cc.CityId)
			.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<User>()
		   .HasOne(ar => ar.AppRate)
		   .WithOne(us => us.User)
		   .HasForeignKey<AppRate>(us => us.UserId)
			.OnDelete(DeleteBehavior.Restrict);

			//many to many relationships

			modelBuilder.Entity<Hotel_Reservation>()
				.HasKey(hr => new { hr.HotelId, hr.ReservationId });
			modelBuilder.Entity<Hotel_Reservation>()
				.HasOne(h => h.Hotel)
				.WithMany(hr => hr.Hotel_Reservations)
				.HasForeignKey(r => r.ReservationId)
				.OnDelete(DeleteBehavior.Restrict);
			modelBuilder.Entity<Hotel_Reservation>()
			   .HasOne(h => h.Reservation)
			   .WithMany(hr => hr.Hotel_Reservations)
			   .HasForeignKey(r => r.HotelId)
			   .OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<Resturant_Reservation>()
				.HasKey(rr => new { rr.ResturantId, rr.ReservationId });
			modelBuilder.Entity<Resturant_Reservation>()
				.HasOne(re => re.Resturant)
				.WithMany(rr =>rr. Resturant_Reservations)
				.HasForeignKey(res => res.ReservationId)
				.OnDelete(DeleteBehavior.Restrict);
			modelBuilder.Entity<Resturant_Reservation>()
			   .HasOne(re => re.Reservation)
			   .WithMany(rr => rr.Resturant_Reservations)
			   .HasForeignKey(res => res.ResturantId)
			   .OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<Package_Resturant>()
				.HasKey(pr => new { pr.PackageId, pr.ResturantId });
			modelBuilder.Entity<Package_Resturant>()
				.HasOne(rest => rest.Resturant)
				.WithMany(pr => pr.Package_Resturants)
				.HasForeignKey(p => p.PackageId)
				.OnDelete(DeleteBehavior.Restrict);
			modelBuilder.Entity<Package_Resturant>()
			   .HasOne(rest => rest.Package)
			   .WithMany(pr => pr.Package_Resturants)
			   .HasForeignKey(p => p.ResturantId)
			   .OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<Car_User>()
				.HasKey(cu => new { cu.CarId, cu.UserId });
			modelBuilder.Entity<Car_User>()
				.HasOne(c => c.Car)
				.WithMany(cu => cu.Car_Users)
				.HasForeignKey(u => u.UserId)
				.OnDelete(DeleteBehavior.Restrict);
			modelBuilder.Entity<Car_User>()
			   .HasOne(c => c.User)
			   .WithMany(cu =>cu. Car_Users)
			   .HasForeignKey(u => u.CarId)
			   .OnDelete(DeleteBehavior.Restrict);


		}





		
	}
    
}    

