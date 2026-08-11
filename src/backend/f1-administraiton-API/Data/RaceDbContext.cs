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

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			
			modelBuilder.Entity<RaceDay>(builder =>
			{
				// Kompozitni primarni ključ za dan trke
				builder.HasKey(rd => new { rd.RaceId, rd.Id });

				// Onemogućavamo auto-increment jer se Id unosi ručno (1, 2 ili 3)
				builder.Property(rd => rd.Id)
					   .ValueGeneratedNever();

				// Sprečavamo i da se RaceId automatski generiše
				builder.Property(rd => rd.RaceId)
					   .ValueGeneratedNever();
			});
			// 
			// 1. SeatingZone Konfiguracija
			// 
			modelBuilder.Entity<SeatingZone>(builder =>
			{

				// no-autoincr. jer se za svaki dan trke krece od 0
				builder.Property(sz => sz.Id)
				.ValueGeneratedNever();

				// Kompozitni primarni ključ za zonu
				builder.HasKey(sz => new { sz.RaceId, sz.RaceDayId, sz.Id });

				// Eksplicitno povezivanje sa RaceDay čiji je primarni ključ (RaceId, Id)
				builder.HasOne(sz => sz.RaceDay)
					   .WithMany() // ili .WithMany(rd => rd.SeatingZones) ako vraćate listu
					   .HasForeignKey(sz => new { sz.RaceId, sz.RaceDayId })
					   .HasPrincipalKey(rd => new { rd.RaceId, rd.Id }); // OVO JE KLJUČNO!

			});

	
			modelBuilder.Entity<Location>().HasData(
				new Location
				{
					Id = 1,
					Coutry = "Monaco",
					City = "Monte Carlo",
					CircuitName = "Circuit de Monaco"
				},
				new Location
				{
					Id = 2,
					Coutry = "Austria",
					City = "Spielberg",
					CircuitName = "Red Bull Ring"
				},
				new Location
				{
					Id = 3,
					Coutry = "United Arab Emirates",
					City = "Abu Dhabi",
					CircuitName = "Yas Marina Circuit"
				});

			modelBuilder.Entity<Race>().HasData(
				new Race
				{
					Id = 1,
					GrandPrixName = "Monaco Grand Prix",
					EuroBasePrice = 500.00m,
					AdditionalInfo = "Monaco street circuit race.",
					LocationId = 1
				},
				new Race
				{
					Id = 2,
					GrandPrixName = "Red Bull Ring",
					EuroBasePrice = 250.00m,
					AdditionalInfo = "Austrian Grand Prix race.",
					LocationId = 2
				},
				new Race
				{
					Id = 3,
					GrandPrixName = "Abu Dhabi GP",
					EuroBasePrice = 350.00m,
					AdditionalInfo = "Season finale at Yas Marina Circuit.",
					LocationId = 3
				});
		}
	}
}
