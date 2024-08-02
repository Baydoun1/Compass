using Compass.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<DataContext>
{
	public DataContext CreateDbContext(string[] args)
	{
		var optionsBuilder = new DbContextOptionsBuilder<DataContext>();

		// Load configuration from appsettings.json
		IConfigurationRoot configuration = new ConfigurationBuilder()
			.SetBasePath(Directory.GetCurrentDirectory())
			.AddJsonFile("appsettings.json")
			.Build();

		var connectionString = configuration.GetConnectionString("DefaultConnection");
		optionsBuilder.UseSqlServer(connectionString);

		return new DataContext(optionsBuilder.Options);
	}
}
