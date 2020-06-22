using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ugugushka.Data.Models
{
    public class OrderToy
    {
        public int ToyId { get; set; }
        public Toy Toy { get; set; }

        public Guid OrderId { get; set; }
        public Order Order { get; set; }

        public int Quantity { get; set; }
    }

    internal class OrderToyConfiguration : IEntityTypeConfiguration<OrderToy>
    {
        public void Configure(EntityTypeBuilder<OrderToy> builder)
        {
            builder.HasKey(ot => new {ot.ToyId, ot.OrderId});
            builder
                .HasOne(ot => ot.Toy)
                .WithMany(t => t.OrderToys)
                .HasForeignKey(ot => ot.ToyId);
            builder
                .HasOne(ot => ot.Order)
                .WithMany(o => o.OrderToys)
                .HasForeignKey(ot => ot.OrderId);
        }
    }
}
