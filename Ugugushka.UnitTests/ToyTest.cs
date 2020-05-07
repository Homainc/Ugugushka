using System.Collections.Generic;
using System.Linq;
using Ugugushka.Common.Concretes;
using Ugugushka.Data.Models;
using Ugugushka.UnitTests.Abstractions;
using Xunit;

namespace Ugugushka.UnitTests
{
    public class ToyTest : AbstractTestWithDb
    {
        private static IEnumerable<Toy> TestToys
        {
            get
            {
                var partition = new[]
                {
                    new Partition {Id = 1, Name = "For children"},
                    new Partition {Id = 2, Name = "For adults"}
                };
                var categories = new[]
                {
                    new Category {Id = 1, Name = "Plush", Partition = partition[0], PartitionId = partition[0].Id},
                    new Category {Id = 2, Name = "Logic", Partition = partition[0], PartitionId = partition[0].Id},
                    new Category {Id = 3, Name = "Plastic", Partition = partition[1], PartitionId = partition[1].Id},
                    new Category {Id = 4, Name = "Wood", Partition = partition[1], PartitionId = partition[1].Id}
                };

                var toys = new[]
                {
                    new Toy
                    {
                        Name = "Bear", Category = categories[0], CategoryId = categories[0].Id,
                        Description = "plush bear", IsOnStock = true, Price = 25.8m
                    },
                    new Toy
                    {
                        Name = "Rabbit", Category = categories[0], CategoryId = categories[0].Id,
                        Description = "plush rabbit", IsOnStock = true, Price = 26.4m
                    },
                    new Toy
                    {
                        Name = "Dog", Category = categories[0], CategoryId = categories[0].Id,
                        Description = "plush dog", IsOnStock = false, Price = 50.6m
                    },
                    new Toy
                    {
                        Name = "Mafia", Category = categories[1], CategoryId = categories[1].Id,
                        Description = "table mafia", IsOnStock = false, Price = 70.6m
                    },
                    new Toy
                    {
                        Name = "Car", Category = categories[2], CategoryId = categories[2].Id,
                        Description = "plastic car", IsOnStock = true, Price = 10.6m
                    },
                    new Toy
                    {
                        Name = "Pencil", Category = categories[3], CategoryId = categories[3].Id,
                        Description = "wooden pencil", IsOnStock = false, Price = 5.6m
                    }
                };
                return toys;
            }
        }

        [Fact]
        public async void Can_Paginate()
        {
            //Assign
            DbName = "Can_Paginate";
            await using var context = CreateContext();
            Context = context;
            var toyManager = await CreateToyManagerAsync(TestToys);

            var filter = new ToyFilterInfo();
            var firstPageWithTwoItems = new PageInfo {PageNumber = 1, PageSize = 2};
            var secondPageWithTwoItems = new PageInfo {PageNumber = 2, PageSize = 2};
            var firstPageWithFourItems = new PageInfo {PageNumber = 1, PageSize = 4};
            var secondPageWithFourItems = new PageInfo {PageNumber = 2, PageSize = 4};

            //Action
            var toysFirstPageWithTwoItems = await toyManager.GetPagedFilteredAsync(filter, firstPageWithTwoItems);
            var toysSecondPageWithTwoItems = await toyManager.GetPagedFilteredAsync(filter, secondPageWithTwoItems);
            var toysFirstPageWithFourItems = await toyManager.GetPagedFilteredAsync(filter, firstPageWithFourItems);
            var toysSecondPageWithFourItems = await toyManager.GetPagedFilteredAsync(filter, secondPageWithFourItems);

            //Assert
            Assert.Equal((uint) 2, toysFirstPageWithTwoItems.PageSize);
            Assert.Equal((uint) 1, toysFirstPageWithTwoItems.PageNumber);
            Assert.Equal((uint) 6, toysFirstPageWithTwoItems.TotalItems);
            Assert.Equal((uint) 3, toysFirstPageWithTwoItems.TotalPages);
            Assert.Equal(2, toysFirstPageWithTwoItems.Items.Count());

            Assert.Equal(2, toysSecondPageWithTwoItems.Items.Count());

            Assert.Equal(4, toysFirstPageWithFourItems.Items.Count());
            Assert.Equal((uint) 2, toysFirstPageWithFourItems.TotalPages);

            Assert.Equal(2, toysSecondPageWithFourItems.Items.Count());
            Assert.Equal((uint) 6, toysSecondPageWithFourItems.TotalItems);
        }

        [Fact]
        public async void Can_Filtering()
        {
            //Assign
            DbName = "Can_Filtering";
            await using var context = CreateContext();
            Context = context;
            var toyManager = await CreateToyManagerAsync(TestToys);

            var pageInfo = new PageInfo {PageSize = 6, PageNumber = 1};
            var plushCategory = new ToyFilterInfo {CategoryId = 0};
            var plushCategoryWithMinTwentySixAndMaxThirty =
                new ToyFilterInfo {CategoryId = 0, MinPrice = 26m, MaxPrice = 30m};
            var isOnStock = new ToyFilterInfo {IsOnStock = true};
            var maxThirty = new ToyFilterInfo {MaxPrice = 30m};
            var minThirty = new ToyFilterInfo {MinPrice = 30m};
            var searchCar = new ToyFilterInfo {SearchString = "Car"};

            //Action
            var plushToys = await toyManager.GetPagedFilteredAsync(plushCategory, pageInfo);
            var plushCategoryWithMinTwentySixAndMaxThirtyToys =
                await toyManager.GetPagedFilteredAsync(plushCategoryWithMinTwentySixAndMaxThirty, pageInfo);
            var isOnStockToys = await toyManager.GetPagedFilteredAsync(isOnStock, pageInfo);
            var maxThirtyToys = await toyManager.GetPagedFilteredAsync(maxThirty, pageInfo);
            var minThirtyToys = await toyManager.GetPagedFilteredAsync(minThirty, pageInfo);
            var searchCarToys = await toyManager.GetPagedFilteredAsync(searchCar, pageInfo);

            //Assert
            Assert.Collection(plushToys.Items, x => Assert.Equal("Plush", x.CategoryName));
            Assert.Equal((uint) 5, plushToys.TotalItems);

            Assert.Collection(plushCategoryWithMinTwentySixAndMaxThirtyToys.Items,
                x => Assert.Equal("Plush", x.CategoryName));
            Assert.Collection(plushCategoryWithMinTwentySixAndMaxThirtyToys.Items,
                x => Assert.InRange(x.Price, 26m, 30m));
            Assert.Equal((uint) 1, plushCategoryWithMinTwentySixAndMaxThirtyToys.TotalItems);

            Assert.Collection(isOnStockToys.Items, x => Assert.True(x.IsOnStock));
            Assert.Equal((uint) 3, isOnStockToys.TotalItems);

            Assert.Collection(maxThirtyToys.Items, x => Assert.True(x.Price <= 30m));
            Assert.Equal((uint) 4, maxThirtyToys.TotalItems);

            Assert.Collection(minThirtyToys.Items, x => Assert.True(x.Price >= 30m));
            Assert.Equal((uint) 2, minThirtyToys.TotalItems);

            Assert.Collection(searchCarToys.Items, x => Assert.Contains("Car", x.Name));
            Assert.Equal((uint) 1, searchCarToys.TotalItems);
        }
    }
}
