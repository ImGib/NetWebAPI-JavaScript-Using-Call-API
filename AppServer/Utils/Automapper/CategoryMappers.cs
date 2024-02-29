using AppServer.DTO.CategoryDTO;
using AppServer.Models;
using AutoMapper;

namespace AppServer.Utils.Automapper
{
    public class CategoryMappers : Profile
    {
        public CategoryMappers()
        {
            CreateMap<Category, CategoryResponse>();
            CreateMap<CategoryRequest, Category>();
        }
    }
}
