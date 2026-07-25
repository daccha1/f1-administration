using System.ComponentModel.DataAnnotations;

namespace f1_administraiton_API.Models
{
	public enum RaceDayAgenda
	{
		None, // for testing: agenda property must not take None (0 enum value) when inserted into database
		Practice,
		Qualifications,
		Sprint,
		Race
	}
	public class RaceDay
	{
		[Range(1, 3, ErrorMessage = "Vrednost mora biti 1, 2 ili 3.")]
		public int Id { get; set; }
		public int RaceId { get; set; }
		public Race Race { get; set; }
		public RaceDayAgenda Agenda { get; set; }
	}
}
