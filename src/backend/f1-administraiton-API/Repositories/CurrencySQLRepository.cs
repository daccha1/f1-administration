using f1_administraiton_API.Contracts;
using f1_administraiton_API.Data;
using f1_administraiton_API.Models;
using Microsoft.EntityFrameworkCore;

namespace f1_administraiton_API.Repositories
{
	public class CurrencySQLRepository : ICurrencyRepository
	{
		RaceDbContext context;
		public CurrencySQLRepository(RaceDbContext db)
		{
			context = db;
		}

		public async Task<Currency> CreateCurrency(Currency newCurrency)
		{
			try
			{
				var currencyExists = await context.Currencies.AnyAsync(c => c.SelectedCurrency == newCurrency.SelectedCurrency);
				if (currencyExists)
				{
					return null;
				}
				await context.Currencies.AddAsync(newCurrency);
				await context.SaveChangesAsync();
				return newCurrency;
			}
			catch (Exception ex)
			{
				return null;
			}
		}

		public async Task<bool> DeleteCurrency(int id)
		{
			try
			{
				var currency = await context.Currencies.Where(c => c.Id == id).FirstOrDefaultAsync();
				if (currency == null)
				{
					return false;
				}
				context.Currencies.Remove(currency);
				await context.SaveChangesAsync();
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}

		public async Task<Currency> GetCurrencyById(int id)
		{
			try
			{
				var currency = await context.Currencies.Where(c => c.Id == id).FirstOrDefaultAsync();
				return currency;
			}
			catch (Exception ex)
			{
				return null;
			}
		}

		public async Task<List<Currency>> GetCurrencies()
		{
			try
			{
				var currencies = await context.Currencies.ToListAsync();
				return currencies;
			}
			catch (Exception ex)
			{
				return null;
			}
		}

		public async Task<Currency> UpdateCurrency(Currency updatedCurrency)
		{
			try
			{
				var currency = await context.Currencies.Where(c => c.Id == updatedCurrency.Id).FirstOrDefaultAsync();
				if (currency == null)
				{
					return null;
				}
				var currencyExists = await context.Currencies.AnyAsync(c => c.Id != updatedCurrency.Id && c.SelectedCurrency == updatedCurrency.SelectedCurrency);
				if (currencyExists)
				{
					return null;
				}
				currency.SelectedCurrency = updatedCurrency.SelectedCurrency;

				await context.SaveChangesAsync();
				return currency;
			}
			catch (Exception ex)
			{
				return null;
			}
		}
	}
}
