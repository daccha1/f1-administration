using f1_administraiton_API.Contracts;
using f1_administraiton_API.Data;
using f1_administraiton_API.Repositories;

namespace f1_administraiton_API
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddSqlServer<RaceDbContext>(builder.Configuration.GetConnectionString("DefaultConnection"));
			
			builder.Services.AddScoped<ILocationRepository, LocationSQLRepository>();
			builder.Services.AddScoped<ICurrencyRepository, CurrencySQLRepository>();
			builder.Services.AddScoped<IPaddockPassRepository, PaddockPassSQLRepository>();
			builder.Services.AddScoped<IPassRepository, PassSQLRepository>();
			builder.Services.AddScoped<IPassZoneRepository, PassZoneSQLRepository>();
			builder.Services.AddScoped<IPromoCodeRepository, PromoCodeSQLRepository>();
			builder.Services.AddScoped<IRaceRepository, RaceSQLRepository>();
			builder.Services.AddScoped<IRaceDayRepository, RaceDaySQLRepository>();
			builder.Services.AddScoped<ISeatingZoneRepository, SeatingZoneSQLRepository>();

			builder.Services.AddControllers();
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			builder.Services.AddCors(options =>
			{
				// Opcija A: Dozvoljava tvoj konkretni frontend (Preporučeno)
				options.AddPolicy("AllowFrontend", policy =>
				{
					policy.WithOrigins("http://localhost:5173") // URL tvog Vite/React app-a
						  .AllowAnyHeader()
						  .AllowAnyMethod()
						  .AllowCredentials(); // Dodaj ako šalješ cookie-je ili Auth header-e
				});

				// Opcija B: Dozvoljava doslovno SVE (Samo za lokalni razvoj/testiranje)
				options.AddPolicy("AllowAll", policy =>
				{
					policy.AllowAnyOrigin()
						  .AllowAnyHeader()
						  .AllowAnyMethod();
				});
			});

			var app = builder.Build();

			app.UseCors("AllowAll");

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}
			else
			{
				app.UseHttpsRedirection();
			}

			app.UseAuthorization();

			app.MapControllers();

			app.Run();
		}
	}
}
