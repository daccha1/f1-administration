using f1_administraiton_API.Contracts;
using f1_administraiton_API.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace f1_administraiton_API.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class CurrenciesController(ICurrencyRepository repository) : ControllerBase
	{
		[HttpGet("get-all")]
		public async Task<ActionResult<List<CurrencyDto>>> GetCurrencies()
		{
			var currencies = await repository.GetCurrencies();
			return currencies is null ? StatusCode(StatusCodes.Status500InternalServerError) : Ok(currencies.Select(x => x.ToDto()).ToList());
		}

		[HttpGet("id/{id}")]
		public async Task<ActionResult<CurrencyDto>> GetCurrencyById(int id)
		{
			var currency = await repository.GetCurrencyById(id);
			return currency is null ? NotFound() : Ok(currency.ToDto());
		}

		[HttpPut("update/{id}")]
		public async Task<ActionResult<CurrencyDto>> UpdateCurrency(int id, [FromBody] CurrencyRequestDto dto)
		{
			if (await repository.GetCurrencyById(id) is null) return NotFound();
			var currency = dto.ToEntity(); currency.Id = id;
			var updatedCurrency = await repository.UpdateCurrency(currency);
			return updatedCurrency is null ? BadRequest() : Ok(updatedCurrency.ToDto());
		}

		[HttpPost("new")]
		public async Task<ActionResult<CurrencyDto>> CreateCurrency([FromBody] CurrencyRequestDto dto)
		{
			var currency = await repository.CreateCurrency(dto.ToEntity());
			return currency is null ? BadRequest() : CreatedAtAction(nameof(GetCurrencyById), new { id = currency.Id }, currency.ToDto());
		}

		[HttpDelete("delete/{id}")]
		public async Task<ActionResult> DeleteCurrency(int id) => await repository.DeleteCurrency(id) ? NoContent() : NotFound();
	}
}
