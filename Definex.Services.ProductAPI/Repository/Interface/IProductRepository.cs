﻿using Definex.Services.ProductAPI.Dto;

namespace DefineX.Services.ProductAPI.Repository
{
	public interface IProductRepository
	{

		//crud  create read update delete  
		Task<IEnumerable<ProductDto>> GetProducts();
		Task<ProductDto> GetProductById(int productId);
		Task<ProductDto> CreateUpdateProduct(ProductDto productDto);
		Task<bool> DeleteProduct(int productId);
	}
}
