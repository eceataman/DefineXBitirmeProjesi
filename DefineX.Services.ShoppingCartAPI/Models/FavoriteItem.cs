

namespace DefineX.Services.ShoppingCartAPI.Models
{
	public class FavoriteItem
	{
		public int Id { get; set; }
		public string UserId { get; set; }  // Giriş yapan veya guest
		public int ProductId { get; set; }
	}
}
