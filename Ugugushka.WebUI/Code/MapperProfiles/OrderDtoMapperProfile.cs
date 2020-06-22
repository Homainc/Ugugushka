using AutoMapper;
using Ugugushka.Domain.DtoModels;
using Ugugushka.WebUI.ViewModels;

namespace Ugugushka.WebUI.Code.MapperProfiles
{
    public class OrderDtoMapperProfile : Profile
    {
        public OrderDtoMapperProfile()
        {
            CreateMap<CartCheckoutViewModel, OrderDtoCreate>();
        }
    }
}
