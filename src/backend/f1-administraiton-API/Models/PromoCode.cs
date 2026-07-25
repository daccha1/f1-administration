namespace f1_administraiton_API.Models
{
	public class PromoCode
	{
		public int Id { get; set; }
		public string Code { get; set; } = Guid.NewGuid().ToString("N"); // kreira promo kod
		/// <summary>
		/// isValid & isActive are two different options for the promo code.
		/// isActive switches to true when activated on the purchase.
		/// isValid switches to false when the pass gets deactivated.
		/// </summary>
		public bool isValid { get; set; } = true;
		public bool isActivated { get; set; } = false;
		public double DiscountPercent { get; set; } = 0.05;
		
	}
}
