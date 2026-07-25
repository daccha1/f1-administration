namespace f1_administraiton_API.Models
{
	public class PaddockPass
	{
		public int Id { get; set; }
		public int PassId { get; set; }
		public Pass Pass { get; set; }
		public string Secret { get; set; }
		public bool GarageAccess { get; set; } = false;
		public decimal GaragePrice { get; set; }
		public bool PitLaneAccess { get; set; } = false;
		public decimal PitLanePrice { get; set; }
		public bool FoodAccess { get; set; } = false;
		public decimal FoodPrice { get; set; }
		public bool DrinkAccess { get; set; } = false;
		public decimal DrinkPrice { get; set; }

	}
}
