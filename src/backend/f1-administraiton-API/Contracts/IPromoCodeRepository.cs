using f1_administraiton_API.Models;

namespace f1_administraiton_API.Contracts
{
	public interface IPromoCodeRepository
	{
		public Task<PromoCode> CreatePromoCode(PromoCode newPromoCode);
		public Task<PromoCode> GetPromoCodeById(int id);
		public Task<List<PromoCode>> GetPromoCodes();
		public Task<bool> DeletePromoCode(int id);
		public Task<PromoCode> UpdatePromoCode(PromoCode updatedPromoCode);
		public Task<PromoCode> UsePromoCode(int id);
	}
}
