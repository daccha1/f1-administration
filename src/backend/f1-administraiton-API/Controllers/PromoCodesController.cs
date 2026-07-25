using f1_administraiton_API.Contracts;
using f1_administraiton_API.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace f1_administraiton_API.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class PromoCodesController(IPromoCodeRepository repository) : ControllerBase
	{
		[HttpGet("get-all")]
		public async Task<ActionResult<List<PromoCodeDto>>> GetPromoCodes()
		{
			var promoCodes = await repository.GetPromoCodes();
			return promoCodes is null ? StatusCode(StatusCodes.Status500InternalServerError) : Ok(promoCodes.Select(x => x.ToDto()).ToList());
		}

		[HttpGet("id/{id}")]
		public async Task<ActionResult<PromoCodeDto>> GetPromoCodeById(int id)
		{
			var promoCode = await repository.GetPromoCodeById(id);
			return promoCode is null ? NotFound() : Ok(promoCode.ToDto());
		}

		[HttpPut("update/{id}")]
		public async Task<ActionResult<PromoCodeDto>> UpdatePromoCode(int id, [FromBody] PromoCodeRequestDto dto)
		{
			if (await repository.GetPromoCodeById(id) is null) return NotFound();
			var promoCode = dto.ToEntity(); promoCode.Id = id;
			var updatedPromoCode = await repository.UpdatePromoCode(promoCode);
			return updatedPromoCode is null ? BadRequest() : Ok(updatedPromoCode.ToDto());
		}

		[HttpPost("new")]
		public async Task<ActionResult<PromoCodeDto>> CreatePromoCode([FromBody] PromoCodeRequestDto dto)
		{
			var promoCode = await repository.CreatePromoCode(dto.ToEntity());
			return promoCode is null ? BadRequest() : CreatedAtAction(nameof(GetPromoCodeById), new { id = promoCode.Id }, promoCode.ToDto());
		}

		[HttpDelete("delete/{id}")]
		public async Task<ActionResult> DeletePromoCode(int id) => await repository.DeletePromoCode(id) ? NoContent() : NotFound();
	}
}
