using System;
using System.Collections.Generic;
using Ugugushka.Common.Concretes;

namespace Ugugushka.Domain.DtoModels
{
    public class OrderDtoCreate
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DeliveryWay DeliveryType { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string ApartmentNumber { get; set; }
        public int? FloorNumber { get; set; }
        public string ExitNumber { get; set; }
    }

    public class OrderDto : OrderDtoCreate
    {
        public Guid Id { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalPrice { get; set; }
        public ICollection<OrderToyDto> OrderToys { get; set; } = new List<OrderToyDto>();
    }
}
