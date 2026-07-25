using f1_administraiton_API.Contracts;
using f1_administraiton_API.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace f1_administraiton_API.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class RaceDaysController(IRaceDayRepository repository) : ControllerBase
	{
		[HttpGet("get-all")]
		public async Task<ActionResult<List<RaceDayDto>>> GetRaceDays()
		{
			var raceDays = await repository.GetRaceDays();
			return raceDays is null ? StatusCode(StatusCodes.Status500InternalServerError) : Ok(raceDays.Select(x => x.ToDto()).ToList());
		}

		[HttpGet("id/{id}")]
		public async Task<ActionResult<RaceDayDto>> GetRaceDayById(int id)
		{
			var raceDay = await repository.GetRaceDayById(id);
			return raceDay is null ? NotFound() : Ok(raceDay.ToDto());
		}

		[HttpPut("update/{id}")]
		public async Task<ActionResult<RaceDayDto>> UpdateRaceDay(int id, [FromBody] RaceDayRequestDto dto)
		{
			if (await repository.GetRaceDayById(id) is null) return NotFound();
			var raceDay = dto.ToEntity(); raceDay.Id = id;
			var updatedRaceDay = await repository.UpdateRaceDay(raceDay);
			return updatedRaceDay is null ? BadRequest() : Ok(updatedRaceDay.ToDto());
		}

		[HttpPost("new")]
		public async Task<ActionResult<RaceDayDto>> CreateRaceDay([FromBody] RaceDayRequestDto dto)
		{
			var raceDay = await repository.CreateRaceDay(dto.ToEntity());
			return raceDay is null ? BadRequest() : CreatedAtAction(nameof(GetRaceDayById), new { id = raceDay.Id }, raceDay.ToDto());
		}

		[HttpDelete("delete/{id}")]
		public async Task<ActionResult> DeleteRaceDay(int id) => await repository.DeleteRaceDay(id) ? NoContent() : NotFound();
	}
}
