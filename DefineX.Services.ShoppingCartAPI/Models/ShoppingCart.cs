﻿public class ShoppingCart
{
	public int Id { get; set; }
	public string UserId { get; set; }
	public List<CartItem> Items { get; set; } = new List<CartItem>();
}
