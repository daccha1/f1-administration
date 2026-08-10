using f1_administraiton_API.Contracts;
using f1_administraiton_API.Data;
using f1_administraiton_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace f1_administraiton_API.Repositories
{
	public class PassZoneSQLRepository : IPassZoneRepository
	{
		RaceDbContext context;
		public PassZoneSQLRepository(RaceDbContext db)
		{
			context = db;
		}

		public async Task<PassZone> CreatePassZone(PassZone newPassZone)
		{
			try
			{
				var passId = newPassZone.Pass?.Id ?? 0;
				var raceId = newPassZone.SeatingZone.RaceDay.RaceId;
				var raceDayId = newPassZone.SeatingZone.RaceDay.Id;

				var pass = await context.Passes.Where(p => p.Id == passId).FirstOrDefaultAsync();
				
				var seatingZone = await context.SeatingZones.Where(s => s.RaceDay.Id == raceId && s.Id ==  raceDayId).FirstOrDefaultAsync();
				
				if (pass == null || seatingZone == null)
				{
					return null;
				}
								
				newPassZone.Pass = pass;
				newPassZone.SeatingZone = seatingZone;
				
				await context.PassAndZones.AddAsync(newPassZone);
				await context.SaveChangesAsync();
				
				return newPassZone;
			}
			catch (Exception ex)
			{
				Debug.WriteLine(">>>>>> EX: " + ex.Message);	
				return null;
			}
		}

		public async Task<bool> DeletePassZone(int id)
		{
			try
			{
				var passZone = await context.PassAndZones.Where(p => p.Id == id).FirstOrDefaultAsync();
				if (passZone == null)
				{
					return false;
				}
				context.PassAndZones.Remove(passZone);
				await context.SaveChangesAsync();
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}

		public async Task<PassZone> GetPassZoneById(int id)
		{
			try
			{
				var passZone = await context.PassAndZones.Include(p => p.Pass).Include(p => p.SeatingZone).ThenInclude(s => s.RaceDay).ThenInclude(r => r.Race).ThenInclude(r => r.Location).Where(p => p.Id == id).FirstOrDefaultAsync();
				return passZone;
			}
			catch (Exception ex)
			{
				return null;
			}
		}

		public async Task<List<PassZone>> GetPassZones()
		{
			try
			{
				var passZones = await context.PassAndZones.Include(p => p.Pass).Include(p => p.SeatingZone).ThenInclude(s => s.RaceDay).ThenInclude(r => r.Race).ThenInclude(r => r.Location).ToListAsync();
				return passZones;
			}
			catch (Exception ex)
			{
				return null;
			}
		}

		public async Task<PassZone> UpdatePassZone(PassZone updatedPassZone)
		{
			try
			{
				var passZone = await context.PassAndZones.Where(p => p.Id == updatedPassZone.Id).FirstOrDefaultAsync();
				if (passZone == null)
				{
					return null;
				}
				var passId = updatedPassZone.Pass?.Id ?? 0;
				var seatingZoneId = updatedPassZone.SeatingZone?.Id ?? 0;
				var pass = await context.Passes.Where(p => p.Id == passId).FirstOrDefaultAsync();
				var seatingZone = await context.SeatingZones.Where(s => s.Id == seatingZoneId).FirstOrDefaultAsync();
				if (pass == null || seatingZone == null)
				{
					return null;
				}
				var passZoneExists = await context.PassAndZones.AnyAsync(p => p.Id != updatedPassZone.Id && p.Pass.Id == passId && p.SeatingZone.Id == seatingZoneId);
				if (passZoneExists)
				{
					return null;
				}
				passZone.Pass = pass;
				passZone.SeatingZone = seatingZone;
				passZone.AdditionalInformation = updatedPassZone.AdditionalInformation;

				await context.SaveChangesAsync();
				return passZone;
			}
			catch (Exception ex)
			{
				return null;
			}
		}
	}
}
