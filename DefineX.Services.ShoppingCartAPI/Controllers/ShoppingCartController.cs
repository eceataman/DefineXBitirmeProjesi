using DefineX.Services.ShoppingCartAPI.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class ShoppingCartController : ControllerBase
{
	private readonly ApplicationDbContext _context;

	public ShoppingCartController(ApplicationDbContext context)
	{
		_context = context;
	}

	[HttpPost("AddItem")]
	public async Task<IActionResult> AddItem([FromBody] CartItemDto itemDto)
	{
		var userId = "guest"; // login varsa token'dan al
		var cart = await _context.ShoppingCarts
			.Include(c => c.Items)
			.FirstOrDefaultAsync(c => c.UserId == userId);

		if (cart == null)
		{
			cart = new ShoppingCart
			{
				UserId = userId,
				Items = new List<CartItem>() // null hatası almamak için
			};
			_context.ShoppingCarts.Add(cart);
		}

		// Eğer yine de Items null gelebilecekse (DB'den gelen kayıt için) şu eklenebilir:
		if (cart.Items == null)
		{
			cart.Items = new List<CartItem>();
		}


		var existingItem = cart.Items.FirstOrDefault(i => i.ProductId == itemDto.ProductId && i.Size == itemDto.Size);
		if (existingItem != null)
			existingItem.Quantity += itemDto.Quantity;
		else
			cart.Items.Add(new CartItem
			{
				ProductId = itemDto.ProductId,
				Size = itemDto.Size,
				Quantity = itemDto.Quantity
			});

		await _context.SaveChangesAsync();
		return Ok();
	}

	[HttpGet("{userId}")]
	public async Task<IActionResult> GetCart(string userId)
	{
		var cart = await _context.ShoppingCarts
			.Include(c => c.Items)
			.FirstOrDefaultAsync(c => c.UserId == userId);

		if (cart == null)
			return NotFound();

		return Ok(cart);
	}

	[HttpDelete("Clear/{userId}")]
	public async Task<IActionResult> ClearCart(string userId)
	{
		var cart = await _context.ShoppingCarts
			.Include(c => c.Items)
			.FirstOrDefaultAsync(c => c.UserId == userId);

		if (cart == null) return NotFound();

		_context.CartItems.RemoveRange(cart.Items);
		await _context.SaveChangesAsync();
		return Ok();
	}
}
