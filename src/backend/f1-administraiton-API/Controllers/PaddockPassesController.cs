using f1_administraiton_API.Contracts;
using f1_administraiton_API.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace f1_administraiton_API.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class PaddockPassesController(IPaddockPassRepository repository) : ControllerBase
	{
		[HttpGet("get-all")]
		public async Task<ActionResult<List<PaddockPassDto>>> GetPaddockPasses()
		{
			var paddockPasses = await repository.GetPaddockPasses();
			return paddockPasses is null ? StatusCode(StatusCodes.Status500InternalServerError) : Ok(paddockPasses.Select(x => x.ToDto()).ToList());
		}

		[HttpGet("id/{id}")]
		public async Task<ActionResult<PaddockPassDto>> GetPaddockPassById(int id)
		{
			var paddockPass = await repository.GetPaddockPassById(id);
			return paddockPass is null ? NotFound() : Ok(paddockPass.ToDto());
		}

		[HttpPut("update/{id}")]
		public async Task<ActionResult<PaddockPassDto>> UpdatePaddockPass(int id, [FromBody] PaddockPassRequestDto dto)
		{
			if (await repository.GetPaddockPassById(id) is null) return NotFound();
			var paddockPass = dto.ToEntity(); paddockPass.Id = id;
			var updatedPaddockPass = await repository.UpdatePaddockPass(paddockPass);
			return updatedPaddockPass is null ? BadRequest() : Ok(updatedPaddockPass.ToDto());
		}

		[HttpPost("new")]
		public async Task<ActionResult<PaddockPassDto>> CreatePaddockPass([FromBody] PaddockPassRequestDto dto)
		{
			var paddockPass = await repository.CreatePaddockPass(dto.ToEntity());
			return paddockPass is null ? BadRequest() : CreatedAtAction(nameof(GetPaddockPassById), new { id = paddockPass.Id }, paddockPass.ToDto());
		}

		[HttpDelete("delete/{id}")]
		public async Task<ActionResult> DeletePaddockPass(int id) => await repository.DeletePaddockPass(id) ? NoContent() : NotFound();
	}
}
