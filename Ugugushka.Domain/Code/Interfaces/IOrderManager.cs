using System;
using System.Threading.Tasks;
using Ugugushka.Common.Concretes;
using Ugugushka.Common.Interfaces;
using Ugugushka.Domain.DtoModels;

namespace Ugugushka.Domain.Code.Interfaces
{
    public interface IOrderManager
    {
        Task<IPagedResult<OrderDto>> GetFilteredPagedAsync(IOrderFilterInfo orderFilter, IPageInfo pageInfo);
        Task<OrderDto> GetByIdEagerAsync(Guid id);
        Task<OrderDto> CreateAsync(OrderDtoCreate orderDto, Cart cart);
        Task<OrderDto> SetOrderStatusAsync(Guid id, OrderStatus status);
        Task<OrderDto> DeleteAsync(Guid id);
    }
}
