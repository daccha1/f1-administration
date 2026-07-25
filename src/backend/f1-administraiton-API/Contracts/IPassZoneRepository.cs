using f1_administraiton_API.Models;

namespace f1_administraiton_API.Contracts
{
	public interface IPassZoneRepository
	{
		public Task<PassZone> CreatePassZone(PassZone newPassZone);
		public Task<PassZone> GetPassZoneById(int id);
		public Task<List<PassZone>> GetPassZones();
		public Task<bool> DeletePassZone(int id);
		public Task<PassZone> UpdatePassZone(PassZone updatedPassZone);
	}
}
