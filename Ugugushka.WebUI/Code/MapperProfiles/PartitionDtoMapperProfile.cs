using AutoMapper;
using Ugugushka.Domain.DtoModels;
using Ugugushka.WebUI.ViewModels;

namespace Ugugushka.WebUI.Code.MapperProfiles
{
    public class PartitionDtoMapperProfile : Profile
    {
        public PartitionDtoMapperProfile()
        {
            CreateMap<PartitionDto, AdminAddPartitionViewModel>().ReverseMap();
        }
    }
}
