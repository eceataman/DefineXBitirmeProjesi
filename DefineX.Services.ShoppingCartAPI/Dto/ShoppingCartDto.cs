namespace DefineX.Services.ShoppingCartAPI.Dto
{
	public class ShoppingCartDto
	{
		public int CartId { get; set; }
		public string UserId { get; set; } // Kullanıcı girişi varsa
		public List<CartItemDto> Items { get; set; } = new();

	}
}
