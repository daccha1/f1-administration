using f1_administraiton_API.Contracts;
using f1_administraiton_API.Data;
using f1_administraiton_API.Models;
using Microsoft.EntityFrameworkCore;

namespace f1_administraiton_API.Repositories
{
	public class RaceSQLRepository : IRaceRepository
	{
		RaceDbContext context;
		public RaceSQLRepository(RaceDbContext db)
		{
			context = db;
		}

		public async Task<Race> CreateRace(Race newRace)
		{
			try
			{
				var location = await context.Locations.Where(l => l.Id == newRace.LocationId).FirstOrDefaultAsync();
				if (location == null)
				{
					return null;
				}
				var raceExists = await context.Races.AnyAsync(r => r.GrandPrixName == newRace.GrandPrixName && r.LocationId == newRace.LocationId);
				if (raceExists)
				{
					return null;
				}
				newRace.Location = location;
				await context.Races.AddAsync(newRace);
				await context.SaveChangesAsync();
				return newRace;
			}
			catch (Exception ex)
			{
				return null;
			}
		}

		public async Task<bool> DeleteRace(int id)
		{
			try
			{
				var race = await context.Races.Where(r => r.Id == id).FirstOrDefaultAsync();
				if (race == null)
				{
					return false;
				}
				context.Races.Remove(race);
				await context.SaveChangesAsync();
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}

		public async Task<Race> GetRaceById(int id)
		{
			try
			{
				var race = await context.Races.Include(r => r.Location).Where(r => r.Id == id).FirstOrDefaultAsync();
				return race;
			}
			catch (Exception ex)
			{
				return null;
			}
		}

		public async Task<List<Race>> GetRaces()
		{
			try
			{
				var races = await context.Races.Include(r => r.Location).ToListAsync();
				return races;
			}
			catch (Exception ex)
			{
				return null;
			}
		}

		public async Task<Race> UpdateRace(Race updatedRace)
		{
			try
			{
				var race = await context.Races.Where(r => r.Id == updatedRace.Id).FirstOrDefaultAsync();
				if (race == null)
				{
					return null;
				}
				var location = await context.Locations.Where(l => l.Id == updatedRace.LocationId).FirstOrDefaultAsync();
				if (location == null)
				{
					return null;
				}
				var raceExists = await context.Races.AnyAsync(r => r.Id != updatedRace.Id && r.GrandPrixName == updatedRace.GrandPrixName && r.LocationId == updatedRace.LocationId);
				if (raceExists)
				{
					return null;
				}
				race.GrandPrixName = updatedRace.GrandPrixName;
				race.EuroBasePrice = updatedRace.EuroBasePrice;
				race.AdditionalInfo = updatedRace.AdditionalInfo;
				race.LocationId = updatedRace.LocationId;
				race.Location = location;

				await context.SaveChangesAsync();
				return race;
			}
			catch (Exception ex)
			{
				return null;
			}
		}
	}
}
