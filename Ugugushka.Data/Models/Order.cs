using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ugugushka.Common.Concretes;

namespace Ugugushka.Data.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime Date { get; set; }
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
        public decimal TotalPrice { get; set; }
        public ICollection<OrderToy> OrderToys { get; set; }

        public Order()
        {
            OrderToys = new List<OrderToy>();
        }
    }

    internal class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Status)
                .IsRequired();
            builder.Property(x => x.Date)
                .HasDefaultValueSql("GETUTCDATE()")
                .IsRequired();
            builder.Property(x => x.Id)
                .HasColumnType("nvarchar(450)")
                .ValueGeneratedOnAdd();
            builder.Property(x => x.FirstName)
                .HasMaxLength(256)
                .IsRequired();
            builder.Property(x => x.LastName)
                .HasMaxLength(256)
                .IsRequired();
            builder.Property(x => x.Email)
                .HasMaxLength(256)
                .IsRequired();
            builder.Property(x => x.PhoneNumber)
                .HasMaxLength(50)
                .IsRequired();
            builder.Property(x => x.DeliveryType)
                .IsRequired();
            builder.Property(x => x.Street)
                .HasMaxLength(256)
                .IsRequired(false);
            builder.Property(x => x.HouseNumber)
                .HasMaxLength(50)
                .IsRequired(false);
            builder.Property(x => x.ApartmentNumber)
                .HasMaxLength(50)
                .IsRequired(false);
            builder.Property(x => x.FloorNumber)
                .IsRequired(false);
            builder.Property(x => x.ExitNumber)
                .HasMaxLength(50)
                .IsRequired(false);
            builder.Property(x => x.TotalPrice)
                .HasColumnType("decimal(18,2)")
                .IsRequired();
        }
    }
}
