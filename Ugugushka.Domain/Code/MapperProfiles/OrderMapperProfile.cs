using AutoMapper;
using Ugugushka.Data.Models;
using Ugugushka.Domain.DtoModels;

namespace Ugugushka.Domain.Code.MapperProfiles
{
    public class OrderMapperProfile : Profile
    {
        public OrderMapperProfile()
        {
            CreateMap<OrderDtoCreate, Order>();

            CreateMap<Order, OrderDto>();
            CreateMap<OrderToy, OrderToyDto>();
        }
    }
}
