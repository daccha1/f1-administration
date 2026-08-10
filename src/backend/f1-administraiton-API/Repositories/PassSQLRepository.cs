using f1_administraiton_API.Contracts;
using f1_administraiton_API.Data;
using f1_administraiton_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace f1_administraiton_API.Repositories
{
	public class PassSQLRepository : IPassRepository
	{
		RaceDbContext context;
		IPromoCodeRepository promoRepository;
		
		public PassSQLRepository(RaceDbContext db, IPromoCodeRepository promoCodeRepository)
		{
			context = db;
			promoRepository = promoCodeRepository;
		}

		public async Task<Pass> CreatePass(Pass newPass)
		{
			try
			{
				var currencyId = newPass.Currency?.Id ?? newPass.CurrenctId;
				var currency = await context.Currencies.Where(c => c.Id == currencyId).FirstOrDefaultAsync();

				if (currency == null)
				{
					return null;
				}

				var passExists = await context.Passes.AnyAsync(p => p.Email == newPass.Email && p.isActive);
				
				if (passExists)
				{
					return null;
				}

				newPass.Currency = currency;
				newPass.CurrenctId = currency.Id;

				var promoCode = new PromoCode();

				newPass.PromoCode = promoCode;

				await context.Passes.AddAsync(newPass);
				await context.SaveChangesAsync();
				return newPass;
			}
			catch (Exception ex)
			{
				Debug.WriteLine(">>>>>>>>> EXCEPTION: " + ex.Message);
				return null;
			}
		}

		public async Task<bool> DeletePass(int id)
		{
			try
			{
				var pass = await context.Passes.Where(p => p.Id == id).FirstOrDefaultAsync();
				if (pass == null)
				{
					return false;
				}
				context.Passes.Remove(pass);
				await context.SaveChangesAsync();
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}

		public async Task<Pass> GetPassById(int id)
		{
			try
			{
				var pass = await context.Passes.Include(p => p.Currency).Include(p => p.PromoCode).Where(p => p.Id == id).FirstOrDefaultAsync();
				return pass;
			}
			catch (Exception ex)
			{
				return null;
			}
		}

		public async Task<List<Pass>> GetPasses()
		{
			try
			{
				var passes = await context.Passes.Include(p => p.Currency).Include(p => p.PromoCode).ToListAsync();
				return passes;
			}
			catch (Exception ex)
			{
				return null;
			}
		}

		public async Task<Pass> UpdatePass(Pass updatedPass)
		{
			try
			{
				var pass = await context.Passes.Where(p => p.Id == updatedPass.Id).FirstOrDefaultAsync();
				if (pass == null)
				{
					return null;
				}
				var currencyId = updatedPass.Currency?.Id ?? updatedPass.CurrenctId;
				var currency = await context.Currencies.Where(c => c.Id == currencyId).FirstOrDefaultAsync();
				var promoCode = await context.PromoCodes.Where(p => p.Id == updatedPass.PromoCodeId).FirstOrDefaultAsync();
				if (currency == null || promoCode == null)
				{
					return null;
				}
				var passExists = await context.Passes.AnyAsync(p => p.Id != updatedPass.Id && p.Email == updatedPass.Email && p.isActive);
				if (passExists)
				{
					return null;
				}
				pass.FirstName = updatedPass.FirstName;
				pass.LastName = updatedPass.LastName;
				pass.Address = updatedPass.Address;
				pass.ZipCode = updatedPass.ZipCode;
				pass.Town = updatedPass.Town;
				pass.Country = updatedPass.Country;
				pass.Email = updatedPass.Email;
				pass.isActive = updatedPass.isActive;
				pass.Secret = updatedPass.Secret;
				pass.Total = updatedPass.Total;
				pass.DiscountDate = updatedPass.DiscountDate;
				pass.CurrenctId = currency.Id;
				pass.Currency = currency;
				pass.PromoCodeId = updatedPass.PromoCodeId;
				pass.PromoCode = promoCode;

				await context.SaveChangesAsync();
				return pass;
			}
			catch (Exception ex)
			{
				return null;
			}
		}
	}
}
