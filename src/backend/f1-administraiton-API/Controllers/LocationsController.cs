using f1_administraiton_API.Contracts;
using f1_administraiton_API.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace f1_administraiton_API.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class LocationsController(ILocationRepository repository) : ControllerBase
	{
		[HttpGet("get-all")]
		public async Task<ActionResult<List<LocationDto>>> GetLocations()
		{
			var locations = await repository.GetLocations();
			return locations is null ? StatusCode(StatusCodes.Status500InternalServerError) : Ok(locations.Select(x => x.ToDto()).ToList());
		}

		[HttpGet("id/{id}")]
		public async Task<ActionResult<LocationDto>> GetLocationById(int id)
		{
			var location = await repository.GetLocationById(id);
			return location is null ? NotFound() : Ok(location.ToDto());
		}

		[HttpPut("update/{id}")]
		public async Task<ActionResult<LocationDto>> UpdateLocation(int id, [FromBody] LocationRequestDto dto)
		{
			if (await repository.GetLocationById(id) is null) return NotFound();
			var location = dto.ToEntity(); location.Id = id;
			var updatedLocation = await repository.UpdateLocation(location);
			return updatedLocation is null ? BadRequest() : Ok(updatedLocation.ToDto());
		}

		[HttpPost("new")]
		public async Task<ActionResult<LocationDto>> CreateLocation([FromBody] LocationRequestDto dto)
		{
			var location = await repository.CreateLocation(dto.ToEntity());
			return location is null ? BadRequest() : CreatedAtAction(nameof(GetLocationById), new { id = location.Id }, location.ToDto());
		}

		[HttpDelete("delete/{id}")]
		public async Task<ActionResult> DeleteLocation(int id) => await repository.DeleteLocation(id) ? NoContent() : NotFound();
	}
}
