namespace f1_administraiton_API.Models
{
	public class Race
	{
		public int Id { get; set; }
		public string GrandPrixName { get; set; }
		public decimal EuroBasePrice { get; set; }	
		public string AdditionalInfo { get; set; }
		public int LocationId { get; set; }			
		public Location? Location { get; set; }		
	}
}
