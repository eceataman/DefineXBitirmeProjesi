using Microsoft.AspNetCore.Mvc;
using DefineX.Services.ProductAPI.Repository;
using Definex.Services.ProductAPI.Dto;
using Microsoft.AspNetCore.Authorization;

namespace DefineX.Services.ProductAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		private readonly IProductRepository _productRepo;

		public ProductController(IProductRepository productRepo)
		{
			_productRepo = productRepo;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllProducts()
		{
			var result = await _productRepo.GetProducts();
			return Ok(result);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetProductById(int id)
		{
			var result = await _productRepo.GetProductById(id);
			if (result == null)
				return NotFound();

			return Ok(result);
		}

		[Authorize(Roles = "Admin")] // 👈 sadece admin erişebilsin
		[HttpPost]
		public async Task<IActionResult> CreateOrUpdateProduct([FromBody] ProductDto productDto)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var result = await _productRepo.CreateUpdateProduct(productDto);
			return Ok(result);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteProduct(int id)
		{
			var isSuccess = await _productRepo.DeleteProduct(id);
			if (!isSuccess)
				return NotFound();

			return Ok(new { success = true, message = "Product deleted" });
		}
	}
}
