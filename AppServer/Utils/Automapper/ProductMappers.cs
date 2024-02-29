using AppServer.DTO.ProductDTO;
using AppServer.Models;
using AutoMapper;

namespace AppServer.Utils.Automapper
{
    public class ProductMappers : Profile
    {
        public ProductMappers()
        {
            CreateMap<ProductRequest, Product>();
            CreateMap<Product, ProductResponse>();
        }
    }
}
