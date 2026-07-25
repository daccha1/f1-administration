using f1_administraiton_API.Models;

namespace f1_administraiton_API.Contracts
{
	public interface IPassRepository
	{
		public Task<Pass> CreatePass(Pass newPass);
		public Task<Pass> GetPassById(int id);
		public Task<List<Pass>> GetPasses();
		public Task<bool> DeletePass(int id);
		public Task<Pass> UpdatePass(Pass updatedPass);
	}
}
