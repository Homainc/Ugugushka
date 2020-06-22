using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Options;
using Ugugushka.Common.Concretes;
using Ugugushka.Common.Interfaces;
using Ugugushka.Data.Code.Interfaces;
using Ugugushka.Data.Models;
using Ugugushka.Domain.Code.Abstractions;
using Ugugushka.Domain.Code.Config;
using Ugugushka.Domain.Code.Exceptions;
using Ugugushka.Domain.Code.Extensions;
using Ugugushka.Domain.Code.Interfaces;
using Ugugushka.Domain.DtoModels;

namespace Ugugushka.Domain.Managers
{
    public class OrderManager : AbstractManager, IOrderManager
    {
        private readonly IOrderRepository _orderRepository;
        private readonly DeliveryConfig _deliveryConfig;

        public OrderManager(IOrderRepository orderRepository, IOptions<DeliveryConfig> deliveryOptions,
            ISaveProvider saveProvider, IMapper mapper) : base(saveProvider, mapper)
        {
            _orderRepository = orderRepository;
            _deliveryConfig = deliveryOptions.Value;
        }

        public async Task<IPagedResult<OrderDto>> GetFilteredPagedAsync(IOrderFilterInfo orderFilter,
            IPageInfo pageInfo)
            => (await _orderRepository.GetFilteredPagedAsync(orderFilter, pageInfo)).Map<Order, OrderDto>(Mapper);

        public async Task<OrderDto> GetByIdEagerAsync(Guid id) =>
            Mapper.Map<OrderDto>(await SingleByIdAsync(id));

        public async Task<OrderDto> CreateAsync(OrderDtoCreate orderDto, Cart cart)
        {
            var order = Mapper.Map<Order>(orderDto);
            order.OrderToys = cart.Lines.Select(x => new OrderToy {ToyId = x.Toy.Id, Quantity = x.Quantity}).ToList();
            order.TotalPrice = cart.ComputeTotalValue();
            if (order.DeliveryType == DeliveryWay.Courier)
                order.TotalPrice += _deliveryConfig.CourierPrice;

            await _orderRepository.AddAsync(order);

            await SaveChangesAsync();

            order = await SingleByIdAsync(order.Id);

            return Mapper.Map<OrderDto>(order);
        }

        public async Task<OrderDto> SetOrderStatusAsync(Guid id, OrderStatus status)
        {
            var order = await SingleByIdAsync(id);

            order.Status = status;
            _orderRepository.SetModified(order);
            await SaveChangesAsync();

            return Mapper.Map<OrderDto>(order);
        }

        public async Task<OrderDto> DeleteAsync(Guid id)
        {
            var order = await SingleByIdAsync(id);

            _orderRepository.SetDeleted(order);
            await SaveChangesAsync();

            return Mapper.Map<OrderDto>(order);
        }

        private async Task<Order> SingleByIdAsync(Guid id) =>
            await _orderRepository.SingleByIdEagerOrDefaultAsync(id) ??
            throw new NotFoundException($"Order with id {id} not found!");

    }
}
