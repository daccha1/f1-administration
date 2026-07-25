using f1_administraiton_API.Models;

namespace f1_administraiton_API.Contracts
{
	public interface IRaceRepository
	{
		public Task<Race> CreateRace(Race newRace);
		public Task<Race> GetRaceById(int id);
		public Task<List<Race>> GetRaces();
		public Task<bool> DeleteRace(int id);
		public Task<Race> UpdateRace(Race updatedRace);
	}
}
