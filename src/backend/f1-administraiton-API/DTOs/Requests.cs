using f1_administraiton_API.Models;

namespace f1_administraiton_API.DTOs
{
	public class CurrencyRequestDto
	{
		public AvailableCurrencies SelectedCurrency { get; set; }
	}

	public class LocationRequestDto
	{
		public string Coutry { get; set; } = string.Empty;
		public string City { get; set; } = string.Empty;
		public string CircuitName { get; set; } = string.Empty;
	}

	public class RaceRequestDto
	{
		public string GrandPrixName { get; set; } = string.Empty;
		public decimal EuroBasePrice { get; set; }
		public string AdditionalInfo { get; set; } = string.Empty;
		public int LocationId { get; set; }
	}

	public class RaceDayRequestDto
	{
		public int Id { get; set; }
		public int RaceId { get; set; }
		public DateOnly Date { get; set; }
		public RaceDayAgenda Agenda { get; set; }
	}

	public class SeatingZoneRequestDto
	{
		public int RaceId { get; set; }
		public int RaceDayId { get; set; }
		public SeatingType SeatingType { get; set; }
		public int Capacity { get; set; }
		public List<string> Benefits { get; set; }
	}

	public class PromoCodeRequestDto
	{
		public string Code { get; set; } = string.Empty;
		public bool IsValid { get; set; } = true;
		public bool IsActivated { get; set; }
		public double DiscountPercent { get; set; }
	}

	public class PassRequestDto
	{
		public string FirstName { get; set; } = string.Empty;
		public string LastName { get; set; } = string.Empty;
		public string Address { get; set; } = string.Empty;
		public int ZipCode { get; set; }
		public string Town { get; set; } = string.Empty;
		public string Country { get; set; } = string.Empty;
		public string Email { get; set; } = string.Empty;
		public bool IsActive { get; set; } = true;
		public string Secret { get; set; } = string.Empty;
		public decimal Total { get; set; }
		public DateTime DiscountDate { get; set; }
		public int CurrencyId { get; set; }
		public int PromoCodeId { get; set; }
	}

	public class PaddockPassRequestDto
	{
		public int PassId { get; set; }
		public string Secret { get; set; } = string.Empty;
		public bool GarageAccess { get; set; }
		public decimal GaragePrice { get; set; }
		public bool PitLaneAccess { get; set; }
		public decimal PitLanePrice { get; set; }
		public bool FoodAccess { get; set; }
		public decimal FoodPrice { get; set; }
		public bool DrinkAccess { get; set; }
		public decimal DrinkPrice { get; set; }
	}

	public class PassZoneRequestDto
	{
		public int PassId { get; set; }
		public int SeatingZoneId { get; set; }
		public SeatingType SeatingType { get; set; }
		public int RaceDayId { get; set; }
		public int RaceId { get; set; }
		public string AdditionalInformation { get; set; } = string.Empty;
	}
}
