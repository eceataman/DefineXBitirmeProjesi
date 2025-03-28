public class CartItem
{
	public int Id { get; set; }
	public int ProductId { get; set; }
	public string Size { get; set; }
	public int Quantity { get; set; }

	public int ShoppingCartId { get; set; }
	public ShoppingCart ShoppingCart { get; set; }
}
