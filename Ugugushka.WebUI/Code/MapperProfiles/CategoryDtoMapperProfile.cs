using AutoMapper;
using Ugugushka.Domain.DtoModels;
using Ugugushka.WebUI.ViewModels;

namespace Ugugushka.WebUI.Code.MapperProfiles
{
    public class CategoryDtoMapperProfile : Profile
    {
        public CategoryDtoMapperProfile()
        {
            CreateMap<CategoryDto, AdminAddCategoryViewModel>().ReverseMap();
        }
    }
}
