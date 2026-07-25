namespace f1_administraiton_API.Models
{
	public enum AvailableCurrencies
	{
		EUR,
		USD,
		CHF,
		RSD
	}
	public class Currency
	{
		public int Id { get; set; }
		public AvailableCurrencies SelectedCurrency { get; set; }
	}
}
