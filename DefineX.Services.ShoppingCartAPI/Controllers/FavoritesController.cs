using DefineX.Services.ShoppingCartAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class FavoritesController : ControllerBase
{
	private readonly ApplicationDbContext _context;

	public FavoritesController(ApplicationDbContext context)
	{
		_context = context;
	}

	[HttpPost("Add")]
	public async Task<IActionResult> AddFavorite([FromBody] FavoriteItem item)
	{
		try
		{
			var exists = await _context.FavoriteItems
				.AnyAsync(f => f.UserId == item.UserId && f.ProductId == item.ProductId);

			if (!exists)
			{
				_context.FavoriteItems.Add(item);
				await _context.SaveChangesAsync();
			}

			return Ok();
		}
		catch (Exception ex)
		{
			// 🔥 Hataları logla
			Console.WriteLine("Favori ekleme hatası: " + ex.Message);
			return StatusCode(500, "Favori eklenemedi: " + ex.Message);
		}
	}

	[HttpDelete("Remove")]
	public async Task<IActionResult> RemoveFavorite([FromBody] FavoriteItem item)
	{
		var favorite = await _context.FavoriteItems
			.FirstOrDefaultAsync(f => f.UserId == item.UserId && f.ProductId == item.ProductId);

		if (favorite != null)
		{
			_context.FavoriteItems.Remove(favorite);
			await _context.SaveChangesAsync();
		}

		return Ok();
	}

	[HttpGet("{userId}")]
	public async Task<IActionResult> GetFavorites(string userId)
	{
		var favorites = await _context.FavoriteItems
			.Where(f => f.UserId == userId)
			.ToListAsync();

		return Ok(favorites);
	}

}
