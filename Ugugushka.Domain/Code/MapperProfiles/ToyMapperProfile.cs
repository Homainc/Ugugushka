using AutoMapper;
using Ugugushka.Data.Models;
using Ugugushka.Domain.DtoModels;

namespace Ugugushka.Domain.Code.MapperProfiles
{
    public class ToyMapperProfile : Profile
    {
        public ToyMapperProfile()
        {
            CreateMap<Toy, ToyDto>();
        }
    }
}
