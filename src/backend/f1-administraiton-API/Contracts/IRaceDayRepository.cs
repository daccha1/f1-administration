using f1_administraiton_API.Models;

namespace f1_administraiton_API.Contracts
{
	public interface IRaceDayRepository
	{
		public Task<RaceDay> CreateRaceDay(RaceDay newRaceDay);
		public Task<RaceDay> GetRaceDayById(int id);
		public Task<List<RaceDay>> GetRaceDays();
		public Task<bool> DeleteRaceDay(int id);
		public Task<RaceDay> UpdateRaceDay(RaceDay updatedRaceDay);
	}
}
