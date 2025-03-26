using AutoMapper;
using Definex.Services.ProductAPI.DbContexts;
using Definex.Services.ProductAPI.Dto;
using Definex.Services.ProductAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DefineX.Services.ProductAPI.Repository
{
	public class ProductRepository : IProductRepository
	{
		private readonly ApplicationDbContext _context;
		private readonly IMapper _mapper;

		public ProductRepository(ApplicationDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<IEnumerable<ProductDto>> GetProducts()
		{
			var products = await _context.Products.ToListAsync();
			return _mapper.Map<List<ProductDto>>(products);
		}

		public async Task<ProductDto> GetProductById(int productId)
		{
			var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == productId);
			return _mapper.Map<ProductDto>(product);
		}

		public async Task<ProductDto> CreateUpdateProduct(ProductDto productDto)
		{
			var product = _mapper.Map<Product>(productDto);

			if (product.ProductId > 0)
			{
				_context.Products.Update(product);
			}
			else
			{
				await _context.Products.AddAsync(product);
			}

			await _context.SaveChangesAsync();
			return _mapper.Map<ProductDto>(product);
		}

		public async Task<bool> DeleteProduct(int productId)
		{
			var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == productId);
			if (product == null)
				return false;

			_context.Products.Remove(product);
			await _context.SaveChangesAsync();
			return true;
		}
	}
}
