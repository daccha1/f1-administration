using f1_administraiton_API.Contracts;
using f1_administraiton_API.Data;
using f1_administraiton_API.Models;
using Microsoft.EntityFrameworkCore;

namespace f1_administraiton_API.Repositories
{
	public class RaceDaySQLRepository : IRaceDayRepository
	{
		RaceDbContext context;
		public RaceDaySQLRepository(RaceDbContext db)
		{
			context = db;
		}

		public async Task<RaceDay> CreateRaceDay(RaceDay newRaceDay)
		{
			try
			{
				if (newRaceDay.Agenda == RaceDayAgenda.None)
				{
					return null;
				}
				var race = await context.Races.Where(r => r.Id == newRaceDay.RaceId).FirstOrDefaultAsync();
				if (race == null)
				{
					return null;
				}
				var raceDayExists = await context.RaceDays.AnyAsync(r => r.RaceId == newRaceDay.RaceId && r.Agenda == newRaceDay.Agenda);
				if (raceDayExists)
				{
					return null;
				}
				newRaceDay.Race = race;
				await context.RaceDays.AddAsync(newRaceDay);
				await context.SaveChangesAsync();
				return newRaceDay;
			}
			catch (Exception ex)
			{
				return null;
			}
		}

		public async Task<bool> DeleteRaceDay(int id)
		{
			try
			{
				var raceDay = await context.RaceDays.Where(r => r.Id == id).FirstOrDefaultAsync();
				if (raceDay == null)
				{
					return false;
				}
				context.RaceDays.Remove(raceDay);
				await context.SaveChangesAsync();
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}

		public async Task<RaceDay> GetRaceDayById(int id)
		{
			try
			{
				var raceDay = await context.RaceDays.Include(r => r.Race).ThenInclude(r => r.Location).Where(r => r.Id == id).FirstOrDefaultAsync();
				return raceDay;
			}
			catch (Exception ex)
			{
				return null;
			}
		}

		public async Task<List<RaceDay>> GetRaceDays()
		{
			try
			{
				var raceDays = await context.RaceDays.Include(r => r.Race).ThenInclude(r => r.Location).ToListAsync();
				return raceDays;
			}
			catch (Exception ex)
			{
				return null;
			}
		}

		public async Task<RaceDay> UpdateRaceDay(RaceDay updatedRaceDay)
		{
			try
			{
				if (updatedRaceDay.Agenda == RaceDayAgenda.None)
				{
					return null;
				}
				var raceDay = await context.RaceDays.Where(r => r.Id == updatedRaceDay.Id).FirstOrDefaultAsync();
				if (raceDay == null)
				{
					return null;
				}
				var race = await context.Races.Where(r => r.Id == updatedRaceDay.RaceId).FirstOrDefaultAsync();
				if (race == null)
				{
					return null;
				}
				var raceDayExists = await context.RaceDays.AnyAsync(r => r.Id != updatedRaceDay.Id && r.RaceId == updatedRaceDay.RaceId && r.Agenda == updatedRaceDay.Agenda);
				if (raceDayExists)
				{
					return null;
				}
				raceDay.RaceId = updatedRaceDay.RaceId;
				raceDay.Race = race;
				raceDay.Agenda = updatedRaceDay.Agenda;

				await context.SaveChangesAsync();
				return raceDay;
			}
			catch (Exception ex)
			{
				return null;
			}
		}
	}
}
