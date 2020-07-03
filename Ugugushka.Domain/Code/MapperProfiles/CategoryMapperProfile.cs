using AutoMapper;
using Ugugushka.Data.Models;
using Ugugushka.Domain.DtoModels;

namespace Ugugushka.WebUI.Code.MapperProfiles
{
    public class CategoryMapperProfile : Profile
    {
        public CategoryMapperProfile()
        {
            CreateMap<Category, CategoryDto>()
                .ReverseMap();
        }
    }
}
