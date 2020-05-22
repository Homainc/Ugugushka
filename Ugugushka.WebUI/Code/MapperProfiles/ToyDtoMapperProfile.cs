using System.Linq;
using AutoMapper;
using Ugugushka.Domain.DtoModels;
using Ugugushka.WebUI.ViewModels;

namespace Ugugushka.WebUI.Code.MapperProfiles
{
    public class ToyDtoMapperProfile : Profile
    {
        public ToyDtoMapperProfile()
        {
            CreateMap<ToyDto, ToyItemViewModel>();
            CreateMap<AddToyViewModel, ToyCreateDto>();
        }
    }
}
