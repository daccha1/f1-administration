namespace f1_administraiton_API.Models
{
	public class Pass
	{
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Address { get; set; }
		public int ZipCode { get; set; }
		public string Town { get; set; }
		public string Country { get; set; }
		public string Email { get; set; }

		/// <summary>
		/// True by default, but switches to false when pass gets deactivated.
		/// </summary>
		public bool isActive { get; set; } = true;

		/// <summary>
		/// Random password for later access
		/// </summary>
		public string Secret { get; set; } = Guid.NewGuid().ToString("N");

		/// <summary>
		/// Total amount for the pass
		/// </summary>
		public decimal Total { get; set; }
		public DateTime DiscountDate { get; set; }

		/// <summary>
		/// Currency: each one is uniquely identified
		/// </summary>
		public int CurrenctId { get; set; }
		public Currency Currency { get; set; }
		
		/// <summary>
		/// Promo code for discount
		/// </summary>
		public int PromoCodeId { get; set; }
		public PromoCode PromoCode { get; set; }

	}
}
