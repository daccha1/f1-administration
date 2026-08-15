using System.Text.RegularExpressions;

namespace f1_administraiton_API.Models
{
	public enum SeatingType
	{
		GeneralAdmission, // low budget
		Grandstand,		  // mid budget
		VIP				  // high budget
	}
	public class SeatingZone
	{
		public int Id { get; set; }
		public int RaceDayId { get; set; }
		public int RaceId { get; set; }
		public RaceDay RaceDay { get; set; }
		public SeatingType SeatingType { get; set; }
		public decimal Price { get; set; } // it is calculated by the Race's base price + additional fee based on seating zone	
		public int Capacity { get; set; }
		public List<string> Benefits { get; set; } // benefits for that specific seating zone
	}
}
