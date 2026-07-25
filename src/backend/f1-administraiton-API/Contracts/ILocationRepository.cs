using f1_administraiton_API.Models;

namespace f1_administraiton_API.Contracts
{
	public interface ILocationRepository
	{
		public Task<Location> CreateLocation(Location newLocation);
		public Task<Location> GetLocationById(int id);
		public Task<List<Location>> GetLocations();
		public Task<bool> DeleteLocation(int id);
		public Task<Location> UpdateLocation(Location updatedLocation);
			
	}
}
