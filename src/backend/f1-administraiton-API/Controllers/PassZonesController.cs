using f1_administraiton_API.Contracts;
using f1_administraiton_API.DTOs;
using f1_administraiton_API.Models;
using Microsoft.AspNetCore.Mvc;

namespace f1_administraiton_API.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class PassZonesController(IPassZoneRepository repository) : ControllerBase
	{
		[HttpGet("get-all")]
		public async Task<ActionResult<List<PassZoneDto>>> GetPassZones()
		{
			var passZones = await repository.GetPassZones();
			return passZones is null ? StatusCode(StatusCodes.Status500InternalServerError) : Ok(passZones.Select(x => x.ToDto()).ToList());
		}

		[HttpGet("id/{id}")]
		public async Task<ActionResult<PassZoneDto>> GetPassZoneById(int id)
		{
			var passZone = await repository.GetPassZoneById(id);
			return passZone is null ? NotFound() : Ok(passZone.ToDto());
		}

		[HttpPut("update/{id}")]
		public async Task<ActionResult<PassZoneDto>> UpdatePassZone(int id, [FromBody] PassZoneRequestDto dto)
		{
			if (await repository.GetPassZoneById(id) is null) return NotFound();
			var passZone = dto.ToEntity(); passZone.Id = id;
			var updatedPassZone = await repository.UpdatePassZone(passZone);
			return updatedPassZone is null ? BadRequest() : Ok(updatedPassZone.ToDto());
		}

		[HttpPost("new")]
		public async Task<ActionResult<PassZone>> CreatePassZone([FromBody] PassZoneRequestDto dto)
		{
			var passZone = await repository.CreatePassZone(dto.ToEntity());
			return passZone is null ? BadRequest() : passZone;
		}

		[HttpDelete("delete/{id}")]
		public async Task<ActionResult> DeletePassZone(int id) => await repository.DeletePassZone(id) ? NoContent() : NotFound();
	}
}
