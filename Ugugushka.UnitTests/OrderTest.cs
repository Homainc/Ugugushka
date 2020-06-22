using System;
using System.Collections.Generic;
using System.Linq;
using Ugugushka.Common.Concretes;
using Ugugushka.Data.Models;
using Ugugushka.Domain.Code.Exceptions;
using Ugugushka.Domain.DtoModels;
using Ugugushka.UnitTests.Abstractions;
using Xunit;
using Xunit.Abstractions;

namespace Ugugushka.UnitTests
{
    public class OrderTest : AbstractTestWithDb
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public OrderTest(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        private static IEnumerable<Order> TestOrders
        {
            get
            {
                var oId = Guid.NewGuid();
                return new[]
                {
                    new Order
                    {
                        Id = oId,
                        FirstName = "Phil",
                        LastName = "Kh",
                        Email = "spritefok@gmail.com",
                        ApartmentNumber = "38",
                        Date = DateTime.UtcNow,
                        DeliveryType = DeliveryWay.Courier,
                        ExitNumber = "2",
                        FloorNumber = 2,
                        HouseNumber = "37",
                        PhoneNumber = "+375 (33) 636-99-73",
                        Street = "st.Gur",
                        TotalPrice = 104.3m,
                        OrderToys = new[]
                        {
                            new OrderToy
                            {
                                OrderId = oId,
                                ToyId = 1
                            },
                            new OrderToy
                            {
                                OrderId = oId,
                                ToyId = 2
                            }
                        }
                    }
                };
            }
        }

        [Fact]
        public async void Can_Get_By_Id()
        {
            // Assign
            await using var context = CreateContext("Can_Get_By_Id_Order");
            var testValues = TestOrders.ToList();
            await PopulateAsync(testValues);
            var orderManager = CreateOrderManager();
            var order = testValues[0];
            var id = order.Id;


            // Action
            var dbOrder = await orderManager.GetByIdEagerAsync(id);
            _testOutputHelper.WriteLine(dbOrder.FirstName);
            _testOutputHelper.WriteLine(dbOrder.OrderToys.First().ToyId.ToString());

            // Assert
            Assert.Equal(dbOrder.FirstName, order.FirstName);
            Assert.Equal(dbOrder.LastName, order.LastName);
            Assert.Equal(dbOrder.Email, order.Email);
            Assert.Equal(dbOrder.PhoneNumber, order.PhoneNumber);
            Assert.Equal(dbOrder.DeliveryType, order.DeliveryType);
            Assert.Equal(dbOrder.Street, order.Street);
            Assert.Equal(dbOrder.HouseNumber, order.HouseNumber);
            Assert.Equal(dbOrder.ApartmentNumber, order.ApartmentNumber);
            Assert.Equal(dbOrder.FloorNumber, order.FloorNumber);
            Assert.Equal(dbOrder.ExitNumber, order.ExitNumber);
            Assert.Equal(dbOrder.Date, order.Date);
            Assert.Equal(dbOrder.TotalPrice, order.TotalPrice);

            Assert.Contains(dbOrder.OrderToys,
                x => order.OrderToys.Any(ot => ot.ToyId == x.ToyId && ot.Quantity == x.Quantity));
        }

        [Fact]
        public async void Can_Create()
        {
            // Assign
            await using var context = CreateContext("Can_Create_Order");
            await PopulateAsync(ToyTest.TestToys);
            var orderManager = CreateOrderManager();
            var order = new OrderDtoCreate
            {
                FirstName = "Added",
                LastName = "Test",
                Email = "test@gmail.com",
                ApartmentNumber = "1",
                DeliveryType = DeliveryWay.Courier,
                ExitNumber = "1",
                FloorNumber = 1,
                HouseNumber = "1",
                PhoneNumber = "+375 (33) 111-11-11",
                Street = "st.Test"
            };
            var cartLines = new[]
            {
                new CartLine
                {
                    Quantity = 2,
                    Toy = new ToyDto {Id = 1}
                },
                new CartLine
                {
                    Quantity = 13,
                    Toy = new ToyDto {Id = 2}
                }
            }.ToList();
            var cart = new Cart(cartLines);

            // Action
            var createdOrder = await orderManager.CreateAsync(order, cart);
            var dbOrder = await orderManager.GetByIdEagerAsync(createdOrder.Id);

            // Assert
            Assert.Equal(dbOrder.FirstName, order.FirstName);
            Assert.Equal(dbOrder.LastName, order.LastName);
            Assert.Equal(dbOrder.Email, order.Email);
            Assert.Equal(dbOrder.PhoneNumber, order.PhoneNumber);
            Assert.Equal(dbOrder.DeliveryType, order.DeliveryType);
            Assert.Equal(dbOrder.Street, order.Street);
            Assert.Equal(dbOrder.HouseNumber, order.HouseNumber);
            Assert.Equal(dbOrder.ApartmentNumber, order.ApartmentNumber);
            Assert.Equal(dbOrder.FloorNumber, order.FloorNumber);
            Assert.Equal(dbOrder.ExitNumber, order.ExitNumber);
            Assert.Equal(dbOrder.Date, createdOrder.Date);

            Assert.Contains(dbOrder.OrderToys,
                x => cartLines.Any(cl => cl.Toy.Id == x.ToyId && cl.Quantity == x.Quantity));
            Assert.Equal(dbOrder.TotalPrice, cart.ComputeTotalValue() + 5);
        }

        [Fact]
        public async void Can_Delete()
        {
            // Assign
            await using var context = CreateContext("Can_Delete_Order");
            var testValues = TestOrders.ToList();
            await PopulateAsync(ToyTest.TestToys);
            await PopulateAsync(testValues);
            var orderManager = CreateOrderManager();
            var (toyManager, _) = CreateToyManager();
            var order = testValues[0];
            var id = order.Id;

            // Action
            await toyManager.DeleteAsync(1);
            await orderManager.DeleteAsync(id);


            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () => await orderManager.GetByIdEagerAsync(id));
        }
    }
}
