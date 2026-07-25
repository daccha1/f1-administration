using f1_administraiton_API.Models;
using Microsoft.EntityFrameworkCore;

namespace f1_administraiton_API.Data
{
	public class RaceDbContext : DbContext
	{
		public RaceDbContext(DbContextOptions options) : base(options)
		{
		}

		protected RaceDbContext()
		{
		}

		public DbSet<Location> Locations { get; set; }
		public DbSet<Currency> Currencies { get; set; }
		public DbSet<PaddockPass> PaddockPasses { get; set; }
		public DbSet<Pass> Passes { get; set; }
		public DbSet<PassZone> PassAndZones { get; set; }
		public DbSet<PromoCode> PromoCodes { get; set; }
		public DbSet<Race> Races { get; set; }
		public DbSet<RaceDay> RaceDays { get; set; }
		public DbSet<SeatingZone> SeatingZones { get; set; }

	}
}
