using AutoMapper;
using Definex.Services.ProductAPI.Models;
using Definex.Services.ProductAPI.Dto;

namespace DefineX.Services.ProductAPI.Mapping
{
	public class MappingConfig : Profile
	{
		public MappingConfig()
		{
			CreateMap<Product, ProductDto>().ReverseMap();
		}
	}
}
