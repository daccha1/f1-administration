using f1_administraiton_API.Contracts;
using f1_administraiton_API.Data;
using f1_administraiton_API.Models;
using Microsoft.EntityFrameworkCore;

namespace f1_administraiton_API.Repositories
{
	public class LocationSQLRepository : ILocationRepository
	{
		RaceDbContext context;
		public LocationSQLRepository(RaceDbContext db)
		{
			context = db;
		}

		public async Task<Location> CreateLocation(Location newLocation)
		{
			try
			{
				await context.Locations.AddAsync(newLocation);
				await context.SaveChangesAsync();
				return newLocation;

			}
			catch (Exception ex)
			{
				return null;
			}

		}

		public async Task<bool> DeleteLocation(int id)
		{
			try
			{
				var location = await context.Locations.Where(l => l.Id == id).FirstOrDefaultAsync();
				if (location == null)
				{
					return false;
				}
				context.Locations.Remove(location);
				await context.SaveChangesAsync();
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}

		public async Task<Location> GetLocationById(int id)
		{
			try
			{
				var location = await context.Locations.Where(l => l.Id == id).FirstOrDefaultAsync();
				return location;
			}
			catch (Exception ex)
			{
				return null;
			}
		}

		public async Task<List<Location>> GetLocations()
		{
			try
			{
				var locations = await context.Locations.ToListAsync();
				return locations;
			}
			catch (Exception ex)
			{
				return null;
			}
		}

		public async Task<Location> UpdateLocation(Location updatedLocation)
		{
			var location = await context.Locations.Where(l => l.Id == updatedLocation.Id).FirstOrDefaultAsync();
			if(location == null)
			{
				return null;
			}
			location.Coutry = updatedLocation.Coutry;
			location.CircuitName = updatedLocation.CircuitName;
			location.City = updatedLocation.City;

			await context.SaveChangesAsync();
			return location;
		}
	}
}
