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
				// sta proveravamo?
				// bitno: da li postoji vec taj passzone
				// bitno: da li postoji passzone za taj DAN te TRKE tog ID-a

				var raceDayId = newPassZone.SeatingZone.RaceDayId;
				var raceId = newPassZone.SeatingZone.RaceId;
				var seatingZoneId = newPassZone.SeatingZone.Id;
				var passId = newPassZone.PassId;

				// U pass zone mora da se dodaju konkretni objekti pass i seatingzone

				var pass = await context.Passes.Where(p => p.Id == passId).FirstOrDefaultAsync();

				if (pass == null) throw new Exception("Ne postoji taj PASS");

				var zone = await context.SeatingZones.Where(s => s.RaceDayId == raceDayId && s.RaceId == raceId && s.Id == seatingZoneId).FirstOrDefaultAsync();

				if (zone == null) throw new Exception("Ne postoji taj SEATING ZONE");

				newPassZone.SeatingZone = zone;
				newPassZone.Pass = pass;

				var existsExact = await context.PassAndZones.Where(pz => pz.SeatingZone.RaceDayId == raceDayId && pz.SeatingZone.RaceId == raceId && pz.PassId == passId).FirstOrDefaultAsync();

				if(existsExact != null)
				{
					throw new Exception("POSTOJI VEC TAJ UNOS!");
				}

				newPassZone.SeatingZone.Benefits = new();
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
