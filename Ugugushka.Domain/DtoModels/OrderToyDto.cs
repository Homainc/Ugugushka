using System;
using Ugugushka.Data.Models;

namespace Ugugushka.Domain.DtoModels
{
    public class OrderToyDto
    {
        public int ToyId { get; set; }
        public Toy Toy { get; set; }

        public Guid OrderId { get; set; }
        public Order Order { get; set; }

        public int Quantity { get; set; }
    }
}
