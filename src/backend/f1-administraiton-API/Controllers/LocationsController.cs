using f1_administraiton_API.Contracts;
using f1_administraiton_API.Models;
using f1_administraiton_API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query;

namespace f1_administraiton_API.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class LocationsController : ControllerBase
	{
		ILocationRepository repository;
		public LocationsController(ILocationRepository repository)
		{
			this.repository = repository;
		}

		[HttpGet("/get-all")]
		public async Task<List<Location>> GetLocations()
		{
			return await repository.GetLocations() ?? null;
		}

		[HttpGet("/id/{id}")]
		public async Task<Location> GetLocationById(int id)
		{
			return await repository.GetLocationById(id) ?? null;
		}

		[HttpPut("/update")]
		public async Task<Location> UpdateLocation([FromBody] Location newLocation)
		{
			return await repository.UpdateLocation(newLocation) ?? null;
		}

		[HttpPost("/new")]
		public async Task<Location> CreateLocation([FromBody] Location newLocation)
		{
			return await repository.CreateLocation(newLocation) ?? null;
		}

		[HttpDelete("/delete/{id}")]
		public async Task<bool> DeleteLocation(int id)
		{
			return await repository.DeleteLocation(id) ? true : false;
		}

	}
}
