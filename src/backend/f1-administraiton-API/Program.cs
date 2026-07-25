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

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();

			app.MapControllers();

			app.Run();
		}
	}
}
