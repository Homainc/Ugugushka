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
            CreateMap<ToyDto, ToyItemViewModel>()
                .ForMember(x => x.MainImageUrl, opt => opt.MapFrom(x => x.Images.SingleOrDefault(x => x.IsMain).Url));
            CreateMap<AddToyViewModel, ToyCreateDto>();
        }
    }
}
