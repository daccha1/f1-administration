using f1_administraiton_API.Contracts;
using f1_administraiton_API.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace f1_administraiton_API.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class RacesController(IRaceRepository repository) : ControllerBase
	{
		[HttpGet("get-all")]
		public async Task<ActionResult<List<RaceDto>>> GetRaces()
		{
			var races = await repository.GetRaces();
			return races is null ? StatusCode(StatusCodes.Status500InternalServerError) : Ok(races.Select(x => x.ToDto()).ToList());
		}

		[HttpGet("id/{id}")]
		public async Task<ActionResult<RaceDto>> GetRaceById(int id)
		{
			var race = await repository.GetRaceById(id);
			return race is null ? NotFound() : Ok(race.ToDto());
		}

		[HttpPut("update/{id}")]
		public async Task<ActionResult<RaceDto>> UpdateRace(int id, [FromBody] RaceRequestDto dto)
		{
			if (await repository.GetRaceById(id) is null) return NotFound();
			var race = dto.ToEntity(); race.Id = id;
			var updatedRace = await repository.UpdateRace(race);
			return updatedRace is null ? BadRequest() : Ok(updatedRace.ToDto());
		}

		[HttpPost("new")]
		public async Task<ActionResult<RaceDto>> CreateRace([FromBody] RaceRequestDto dto)
		{
			var race = await repository.CreateRace(dto.ToEntity());
			return race is null ? BadRequest() : CreatedAtAction(nameof(GetRaceById), new { id = race.Id }, race.ToDto());
		}

		[HttpDelete("delete/{id}")]
		public async Task<ActionResult> DeleteRace(int id) => await repository.DeleteRace(id) ? NoContent() : NotFound();
	}
}
