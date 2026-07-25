using f1_administraiton_API.Contracts;
using f1_administraiton_API.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace f1_administraiton_API.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class PassesController(IPassRepository repository) : ControllerBase
	{
		[HttpGet("get-all")]
		public async Task<ActionResult<List<PassDto>>> GetPasses()
		{
			var passes = await repository.GetPasses();
			return passes is null ? StatusCode(StatusCodes.Status500InternalServerError) : Ok(passes.Select(x => x.ToDto()).ToList());
		}

		[HttpGet("id/{id}")]
		public async Task<ActionResult<PassDto>> GetPassById(int id)
		{
			var pass = await repository.GetPassById(id);
			return pass is null ? NotFound() : Ok(pass.ToDto());
		}

		[HttpPut("update/{id}")]
		public async Task<ActionResult<PassDto>> UpdatePass(int id, [FromBody] PassRequestDto dto)
		{
			if (await repository.GetPassById(id) is null) return NotFound();
			var pass = dto.ToEntity(); pass.Id = id;
			var updatedPass = await repository.UpdatePass(pass);
			return updatedPass is null ? BadRequest() : Ok(updatedPass.ToDto());
		}

		[HttpPost("new")]
		public async Task<ActionResult<PassDto>> CreatePass([FromBody] PassRequestDto dto)
		{
			var pass = await repository.CreatePass(dto.ToEntity());
			return pass is null ? BadRequest() : CreatedAtAction(nameof(GetPassById), new { id = pass.Id }, pass.ToDto());
		}

		[HttpDelete("delete/{id}")]
		public async Task<ActionResult> DeletePass(int id) => await repository.DeletePass(id) ? NoContent() : NotFound();
	}
}
