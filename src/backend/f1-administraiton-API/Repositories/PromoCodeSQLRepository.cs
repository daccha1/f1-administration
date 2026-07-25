using f1_administraiton_API.Contracts;
using f1_administraiton_API.Data;
using f1_administraiton_API.Models;
using Microsoft.EntityFrameworkCore;

namespace f1_administraiton_API.Repositories
{
	public class PromoCodeSQLRepository : IPromoCodeRepository
	{
		RaceDbContext context;
		public PromoCodeSQLRepository(RaceDbContext db)
		{
			context = db;
		}

		public async Task<PromoCode> CreatePromoCode(PromoCode newPromoCode)
		{
			try
			{
				var promoCodeExists = await context.PromoCodes.AnyAsync(p => p.Code == newPromoCode.Code);
				if (promoCodeExists)
				{
					return null;
				}
				await context.PromoCodes.AddAsync(newPromoCode);
				await context.SaveChangesAsync();
				return newPromoCode;
			}
			catch (Exception ex)
			{
				return null;
			}
		}

		public async Task<bool> DeletePromoCode(int id)
		{
			try
			{
				var promoCode = await context.PromoCodes.Where(p => p.Id == id).FirstOrDefaultAsync();
				if (promoCode == null)
				{
					return false;
				}
				context.PromoCodes.Remove(promoCode);
				await context.SaveChangesAsync();
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}

		public async Task<PromoCode> GetPromoCodeById(int id)
		{
			try
			{
				var promoCode = await context.PromoCodes.Where(p => p.Id == id).FirstOrDefaultAsync();
				return promoCode;
			}
			catch (Exception ex)
			{
				return null;
			}
		}

		public async Task<List<PromoCode>> GetPromoCodes()
		{
			try
			{
				var promoCodes = await context.PromoCodes.ToListAsync();
				return promoCodes;
			}
			catch (Exception ex)
			{
				return null;
			}
		}

		public async Task<PromoCode> UpdatePromoCode(PromoCode updatedPromoCode)
		{
			try
			{
				var promoCode = await context.PromoCodes.Where(p => p.Id == updatedPromoCode.Id).FirstOrDefaultAsync();
				if (promoCode == null)
				{
					return null;
				}
				var promoCodeExists = await context.PromoCodes.AnyAsync(p => p.Id != updatedPromoCode.Id && p.Code == updatedPromoCode.Code);
				if (promoCodeExists)
				{
					return null;
				}
				promoCode.Code = updatedPromoCode.Code;
				promoCode.isValid = updatedPromoCode.isValid;
				promoCode.isActivated = updatedPromoCode.isActivated;
				promoCode.DiscountPercent = updatedPromoCode.DiscountPercent;

				await context.SaveChangesAsync();
				return promoCode;
			}
			catch (Exception ex)
			{
				return null;
			}
		}
	}
}
