using f1_administraiton_API.Contracts;
using f1_administraiton_API.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace f1_administraiton_API.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class SeatingZonesController(ISeatingZoneRepository repository) : ControllerBase
	{
		[HttpGet("get-all")]
		public async Task<ActionResult<List<SeatingZoneDto>>> GetSeatingZones()
		{
			var seatingZones = await repository.GetSeatingZones();
			return seatingZones is null ? StatusCode(StatusCodes.Status500InternalServerError) : Ok(seatingZones.Select(x => x.ToDto()).ToList());
		}

		[HttpGet("id/{id}")]
		public async Task<ActionResult<SeatingZoneDto>> GetSeatingZoneById(int id)
		{
			var seatingZone = await repository.GetSeatingZoneById(id);
			return seatingZone is null ? NotFound() : Ok(seatingZone.ToDto());
		}

		[HttpPut("update/{id}")]
		public async Task<ActionResult<SeatingZoneDto>> UpdateSeatingZone(int id, [FromBody] SeatingZoneRequestDto dto)
		{
			if (await repository.GetSeatingZoneById(id) is null) return NotFound();
			var seatingZone = dto.ToEntity(); seatingZone.Id = id;
			var updatedSeatingZone = await repository.UpdateSeatingZone(seatingZone);
			return updatedSeatingZone is null ? BadRequest() : Ok(updatedSeatingZone.ToDto());
		}

		[HttpPost("new")]
		public async Task<ActionResult<SeatingZoneDto>> CreateSeatingZone([FromBody] SeatingZoneRequestDto dto)
		{
			var seatingZone = await repository.CreateSeatingZone(dto.ToEntity());
			return seatingZone is null ? BadRequest() : CreatedAtAction(nameof(GetSeatingZoneById), new { id = seatingZone.Id }, seatingZone.ToDto());
		}

		[HttpDelete("delete/{id}")]
		public async Task<ActionResult> DeleteSeatingZone(int id) => await repository.DeleteSeatingZone(id) ? NoContent() : NotFound();
	}
}
