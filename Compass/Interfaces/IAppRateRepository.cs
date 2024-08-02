using Compass.Models;

namespace Compass.Interfaces
{
	public interface IAppRateRepository
	{
		ICollection<AppRate>GetAppRates();
		AppRate GetAppRate(int id);
		//many app rates to one user
		ICollection<AppRate> GetRatesOfAUser(int userId);
		bool RateExists(int RateId);
		bool UpdateRate(AppRate appRate);
		bool DeleteRate(AppRate appRate);
		bool CreateRate(AppRate apprate);
		bool Save();
	}
}
