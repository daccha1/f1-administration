using f1_administraiton_API.Contracts;
using f1_administraiton_API.Data;
using f1_administraiton_API.Models;
using Microsoft.EntityFrameworkCore;

namespace f1_administraiton_API.Repositories
{
	public class SeatingZoneSQLRepository : ISeatingZoneRepository
	{
		RaceDbContext context;
		public SeatingZoneSQLRepository(RaceDbContext db)
		{
			context = db;
		}

		public async Task<SeatingZone> CreateSeatingZone(SeatingZone newSeatingZone)
		{
			try
			{
				var raceDayId = newSeatingZone.RaceDay.Id;
				var raceId = newSeatingZone.RaceDay.RaceId;

				var raceDay = await context.RaceDays.Where(r => r.Id == raceDayId && r.RaceId == raceId).FirstOrDefaultAsync();
				
				if (raceDay == null || newSeatingZone.Capacity <= 0)
				{
					return null;
				}

				var seatingZones = await context.SeatingZones.Where(sz => sz.RaceId == raceId && sz.RaceDayId == raceDayId).ToListAsync();
				
				if (seatingZones == null || seatingZones.Count == 0)
				{
					newSeatingZone.Id = 1;
				}
				else
				{
					var id = seatingZones.Select(sz => sz.Id).Max();
					newSeatingZone.Id = id + 1;
				}

				var seatingZoneExists = await context.SeatingZones.AnyAsync(s => s.RaceDay.Id == raceDayId && s.SeatingType == newSeatingZone.SeatingType && s.RaceId == raceId && s.Id == newSeatingZone.Id);

				if (seatingZoneExists)
				{
					return null;
				}

				newSeatingZone.RaceDay = raceDay;
				await context.SeatingZones.AddAsync(newSeatingZone);
				await context.SaveChangesAsync();
				return newSeatingZone;
			}
			catch (Exception ex)
			{
				return null;
			}
		}

		public async Task<bool> DeleteSeatingZone(int id)
		{
			try
			{
				var seatingZone = await context.SeatingZones.Where(s => s.Id == id).FirstOrDefaultAsync();
				if (seatingZone == null)
				{
					return false;
				}
				context.SeatingZones.Remove(seatingZone);
				await context.SaveChangesAsync();
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}

		public async Task<SeatingZone> GetSeatingZoneById(int id)
		{
			try
			{
				var seatingZone = await context.SeatingZones.Include(s => s.RaceDay).ThenInclude(r => r.Race).ThenInclude(r => r.Location).Where(s => s.Id == id).FirstOrDefaultAsync();
				return seatingZone;
			}
			catch (Exception ex)
			{
				return null;
			}
		}

		public async Task<List<SeatingZone>> GetSeatingZones()
		{
			try
			{
				var seatingZones = await context.SeatingZones.ToListAsync();
				return seatingZones;
			}
			catch (Exception ex)
			{
				return null;
			}
		}

		public async Task<SeatingZone> UpdateSeatingZone(SeatingZone updatedSeatingZone)
		{
			try
			{
				var seatingZone = await context.SeatingZones.Where(s => s.Id == updatedSeatingZone.Id).FirstOrDefaultAsync();
				if (seatingZone == null)
				{
					return null;
				}
				var raceDayId = updatedSeatingZone.RaceDay?.Id ?? 0;
				var raceDay = await context.RaceDays.Where(r => r.Id == raceDayId).FirstOrDefaultAsync();
				if (raceDay == null || updatedSeatingZone.Capacity <= 0)
				{
					return null;
				}
				var seatingZoneExists = await context.SeatingZones.AnyAsync(s => s.Id != updatedSeatingZone.Id && s.RaceDay.Id == raceDayId && s.SeatingType == updatedSeatingZone.SeatingType);
				if (seatingZoneExists)
				{
					return null;
				}
				seatingZone.RaceDay = raceDay;
				seatingZone.SeatingType = updatedSeatingZone.SeatingType;
				seatingZone.Capacity = updatedSeatingZone.Capacity;
				seatingZone.Benefits = updatedSeatingZone.Benefits;

				await context.SaveChangesAsync();
				return seatingZone;
			}
			catch (Exception ex)
			{
				return null;
			}
		}
	}
}
