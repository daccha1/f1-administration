using f1_administraiton_API.Contracts;
using f1_administraiton_API.Data;
using f1_administraiton_API.Models;
using Microsoft.EntityFrameworkCore;

namespace f1_administraiton_API.Repositories
{
	public class PaddockPassSQLRepository : IPaddockPassRepository
	{
		RaceDbContext context;
		public PaddockPassSQLRepository(RaceDbContext db)
		{
			context = db;
		}

		public async Task<bool> CheckEligibility(string secret)
		{
			try
			{
				var passExist = await context.Passes.Where(p => p.Secret == secret).FirstOrDefaultAsync();

				if (passExist != null) return true;

				return false;

			}
			catch (Exception ex)
			{
				return false;
			}
		}

		public async Task<PaddockPass> CreatePaddockPass(PaddockPass newPaddockPass)
		{
			try
			{
				var pass = await context.Passes.Where(p => p.Id == newPaddockPass.PassId).FirstOrDefaultAsync();
				if (pass == null)
				{
					return null;
				}
				var paddockPassExists = await context.PaddockPasses.AnyAsync(p => p.PassId == newPaddockPass.PassId);
				if (paddockPassExists)
				{
					return null;
				}
				newPaddockPass.Pass = pass;
				await context.PaddockPasses.AddAsync(newPaddockPass);
				await context.SaveChangesAsync();
				return newPaddockPass;
			}
			catch (Exception ex)
			{
				return null;
			}
		}

		public async Task<bool> DeletePaddockPass(int id)
		{
			try
			{
				var paddockPass = await context.PaddockPasses.Where(p => p.Id == id).FirstOrDefaultAsync();
				if (paddockPass == null)
				{
					return false;
				}
				context.PaddockPasses.Remove(paddockPass);
				await context.SaveChangesAsync();
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}

		public async Task<PaddockPass> GetPaddockPassById(int id)
		{
			try
			{
				var paddockPass = await context.PaddockPasses.Include(p => p.Pass).ThenInclude(p => p.Currency).Include(p => p.Pass).ThenInclude(p => p.PromoCode).Where(p => p.Id == id).FirstOrDefaultAsync();
				return paddockPass;
			}
			catch (Exception ex)
			{
				return null;
			}
		}

		public async Task<List<PaddockPass>> GetPaddockPasses()
		{
			try
			{
				var paddockPasses = await context.PaddockPasses.Include(p => p.Pass).ThenInclude(p => p.Currency).Include(p => p.Pass).ThenInclude(p => p.PromoCode).ToListAsync();
				return paddockPasses;
			}
			catch (Exception ex)
			{
				return null;
			}
		}

		public async Task<PaddockPass> UpdatePaddockPass(PaddockPass updatedPaddockPass)
		{
			try
			{
				var paddockPass = await context.PaddockPasses.Where(p => p.Id == updatedPaddockPass.Id).FirstOrDefaultAsync();
				if (paddockPass == null)
				{
					return null;
				}
				var pass = await context.Passes.Where(p => p.Id == updatedPaddockPass.PassId).FirstOrDefaultAsync();
				if (pass == null)
				{
					return null;
				}
				var paddockPassExists = await context.PaddockPasses.AnyAsync(p => p.Id != updatedPaddockPass.Id && p.PassId == updatedPaddockPass.PassId);
				if (paddockPassExists)
				{
					return null;
				}
				paddockPass.PassId = updatedPaddockPass.PassId;
				paddockPass.Pass = pass;
				paddockPass.Secret = updatedPaddockPass.Secret;
				paddockPass.GarageAccess = updatedPaddockPass.GarageAccess;
				paddockPass.GaragePrice = updatedPaddockPass.GaragePrice;
				paddockPass.PitLaneAccess = updatedPaddockPass.PitLaneAccess;
				paddockPass.PitLanePrice = updatedPaddockPass.PitLanePrice;
				paddockPass.FoodAccess = updatedPaddockPass.FoodAccess;
				paddockPass.FoodPrice = updatedPaddockPass.FoodPrice;
				paddockPass.DrinkAccess = updatedPaddockPass.DrinkAccess;
				paddockPass.DrinkPrice = updatedPaddockPass.DrinkPrice;

				await context.SaveChangesAsync();
				return paddockPass;
			}
			catch (Exception ex)
			{
				return null;
			}
		}
	}
}
