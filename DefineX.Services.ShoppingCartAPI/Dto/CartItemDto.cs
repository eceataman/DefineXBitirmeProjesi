namespace DefineX.Services.ShoppingCartAPI.Dto
{
	public class CartItemDto
	{
		public int ProductId { get; set; }
		public string Size { get; set; }
		public int Quantity { get; set; }

		// Product bilgisi frontend için opsiyonel olarak dönebilir
		public string? ProductName { get; set; }
		public string? ImageUrl { get; set; }
		public double? Price { get; set; }
		public int Stock { get; set; }
        public string  UserId { get; set; }

    }
}
