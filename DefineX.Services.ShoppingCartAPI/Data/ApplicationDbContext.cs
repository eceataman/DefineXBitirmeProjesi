using DefineX.Services.ShoppingCartAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

public class ApplicationDbContext : DbContext
{
	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
	: base(options) { }

	public DbSet<ShoppingCart> ShoppingCarts { get; set; }
	public DbSet<CartItem> CartItems { get; set; }
	public DbSet<FavoriteItem> FavoriteItems { get; set; }

}
