using f1_administraiton_API.Models;

namespace f1_administraiton_API.DTOs
{
	public static class DtoMappings
	{
		public static Currency ToEntity(this CurrencyRequestDto dto) => new() { SelectedCurrency = dto.SelectedCurrency };
		public static Location ToEntity(this LocationRequestDto dto) => new() { Coutry = dto.Coutry, City = dto.City, CircuitName = dto.CircuitName };
		public static Race ToEntity(this RaceRequestDto dto) => new() { GrandPrixName = dto.GrandPrixName, EuroBasePrice = dto.EuroBasePrice, AdditionalInfo = dto.AdditionalInfo, LocationId = dto.LocationId };
		public static RaceDay ToEntity(this RaceDayRequestDto dto) => new() { RaceId = dto.RaceId, Id=dto.Id, Agenda = dto.Agenda, Race = null! };
		public static SeatingZone ToEntity(this SeatingZoneRequestDto dto) => new() { RaceDay = new RaceDay { RaceId = dto.RaceId, Id = dto.RaceDayId }, SeatingType = dto.SeatingType, Capacity = dto.Capacity, Benefits = dto.Benefits };
		public static PromoCode ToEntity(this PromoCodeRequestDto dto) => new() { Code = dto.Code, isValid = dto.IsValid, isActivated = dto.IsActivated, DiscountPercent = dto.DiscountPercent };
		public static Pass ToEntity(this PassRequestDto dto) => new() { FirstName = dto.FirstName, LastName = dto.LastName, Address = dto.Address, ZipCode = dto.ZipCode, Town = dto.Town, Country = dto.Country, Email = dto.Email, isActive = dto.IsActive, Secret = dto.Secret, Total = dto.Total, DiscountDate = dto.DiscountDate, CurrenctId = dto.CurrencyId, PromoCodeId = dto.PromoCodeId, Currency = null!, PromoCode = null! };
		public static PaddockPass ToEntity(this PaddockPassRequestDto dto) => new() { PassId = dto.PassId, Secret = dto.Secret, GarageAccess = dto.GarageAccess, GaragePrice = dto.GaragePrice, PitLaneAccess = dto.PitLaneAccess, PitLanePrice = dto.PitLanePrice, FoodAccess = dto.FoodAccess, FoodPrice = dto.FoodPrice, DrinkAccess = dto.DrinkAccess, DrinkPrice = dto.DrinkPrice, Pass = null! };
		public static PassZone ToEntity(this PassZoneRequestDto dto) => new() { Pass = new Pass { Id = dto.PassId }, PassId=dto.PassId, SeatingZone = new SeatingZone { Id = dto.SeatingZoneId, RaceDayId = dto.RaceDayId, SeatingType = dto.SeatingType ,RaceId=dto.RaceId, RaceDay = new() { Id = dto.RaceDayId, RaceId = dto.RaceId } }, AdditionalInformation = dto.AdditionalInformation };

		public static CurrencyDto ToDto(this Currency entity) => new() { Id = entity.Id, SelectedCurrency = entity.SelectedCurrency };
		public static LocationDto ToDto(this Location entity) => new() { Id = entity.Id, Coutry = entity.Coutry, City = entity.City, CircuitName = entity.CircuitName };
		public static RaceDto ToDto(this Race entity) => new() { Id = entity.Id, GrandPrixName = entity.GrandPrixName, EuroBasePrice = entity.EuroBasePrice, AdditionalInfo = entity.AdditionalInfo, LocationId = entity.LocationId };
		public static RaceDayDto ToDto(this RaceDay entity) => new() { Id = entity.Id, RaceId = entity.RaceId, Agenda = entity.Agenda };
		public static SeatingZoneDto ToDto(this SeatingZone entity) => new() { RaceDayId = entity.Id, RaceId = entity.RaceDay?.Id ?? 0, SeatingType = entity.SeatingType, Capacity = entity.Capacity, Benefits = entity.Benefits };
		public static PromoCodeDto ToDto(this PromoCode entity) => new() { Id = entity.Id, Code = entity.Code, IsValid = entity.isValid, IsActivated = entity.isActivated, DiscountPercent = entity.DiscountPercent };
		public static PassDto ToDto(this Pass entity) => new() { Id = entity.Id, FirstName = entity.FirstName, LastName = entity.LastName, Address = entity.Address, ZipCode = entity.ZipCode, Town = entity.Town, Country = entity.Country, Email = entity.Email, IsActive = entity.isActive, Secret = entity.Secret, Total = entity.Total, DiscountDate = entity.DiscountDate, CurrencyId = entity.CurrenctId, PromoCodeId = entity.PromoCodeId };
		public static PaddockPassDto ToDto(this PaddockPass entity) => new() { Id = entity.Id, PassId = entity.PassId, Secret = entity.Secret, GarageAccess = entity.GarageAccess, GaragePrice = entity.GaragePrice, PitLaneAccess = entity.PitLaneAccess, PitLanePrice = entity.PitLanePrice, FoodAccess = entity.FoodAccess, FoodPrice = entity.FoodPrice, DrinkAccess = entity.DrinkAccess, DrinkPrice = entity.DrinkPrice };
		public static PassZoneDto ToDto(this PassZone entity) => new() { Id = entity.Id, PassId = entity.Pass.Id, RaceId = entity.SeatingZone.RaceId, RaceDayId = entity.SeatingZone.RaceDayId, SeatingZoneNumber = (int) entity.SeatingZone.SeatingType };
	}
}
