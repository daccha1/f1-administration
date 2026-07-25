using f1_administraiton_API.Models;

namespace f1_administraiton_API.Contracts
{
	public interface ICurrencyRepository
	{
		public Task<Currency> CreateCurrency(Currency newCurrency);
		public Task<Currency> GetCurrencyById(int id);
		public Task<List<Currency>> GetCurrencies();
		public Task<bool> DeleteCurrency(int id);
		public Task<Currency> UpdateCurrency(Currency updatedCurrency);
	}
}
