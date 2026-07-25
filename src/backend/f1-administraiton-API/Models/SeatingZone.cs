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
		public RaceDay RaceDay { get; set; }
		public SeatingType SeatingType { get; set; }
		public int Capacity { get; set; }
		public string Benefits { get; set; } // sta se dobija sve u toj zoni sedenja (ne toliko bitna stvar)
	}
}
