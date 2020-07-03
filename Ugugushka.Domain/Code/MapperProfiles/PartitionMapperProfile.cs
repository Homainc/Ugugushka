using AutoMapper;
using Ugugushka.Data.Models;
using Ugugushka.Domain.DtoModels;

namespace Ugugushka.WebUI.Code.MapperProfiles
{
    public class PartitionMapperProfile : Profile
    {
        public PartitionMapperProfile()
        {
            CreateMap<Partition, PartitionDto>()
                .ReverseMap();
        }
    }
}
