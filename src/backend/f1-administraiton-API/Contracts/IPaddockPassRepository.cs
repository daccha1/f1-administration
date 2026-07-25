using f1_administraiton_API.Models;

namespace f1_administraiton_API.Contracts
{
	public interface IPaddockPassRepository
	{
		public Task<PaddockPass> CreatePaddockPass(PaddockPass newPaddockPass);
		public Task<PaddockPass> GetPaddockPassById(int id);
		public Task<List<PaddockPass>> GetPaddockPasses();
		public Task<bool> DeletePaddockPass(int id);
		public Task<PaddockPass> UpdatePaddockPass(PaddockPass updatedPaddockPass);
	}
}
