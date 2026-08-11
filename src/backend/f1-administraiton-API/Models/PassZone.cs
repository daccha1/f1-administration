namespace f1_administraiton_API.Models
{
	public class PassZone
	{
		public int Id { get; set; }
		public int SeatingZoneId { get; set; }
		public SeatingZone SeatingZone { get; set; }
		public int PassId { get; set; }
		public Pass Pass { get; set; }
		public string AdditionalInformation { get; set; }
	}
}
