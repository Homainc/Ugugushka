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
                    new Partition{Id = 1, Name = "For children"},
                    new Partition{Id = 2, Name = "For adults"}
                };
                var categories = new[]
                {
                    new Category{Id = 1, Name = "Plush", Partition = partition[0], PartitionId = partition[0].Id},
                    new Category{Id = 2, Name = "Logic", Partition = partition[0], PartitionId = partition[0].Id},
                    new Category{Id = 3, Name = "Plastic", Partition = partition[1], PartitionId = partition[1].Id},
                    new Category{Id = 4, Name = "Wood", Partition = partition[1], PartitionId = partition[1].Id}
                };

                var toys = new[]
                {
                    new Toy{Name = "Bear", Category = categories[0], CategoryId = categories[0].Id, Description = "plush bear", IsOnStock = true, Price = 25.8m},
                    new Toy{Name = "Rabbit", Category = categories[0], CategoryId = categories[0].Id, Description = "plush rabbit", IsOnStock = true, Price = 26.4m},
                    new Toy{Name = "Dog", Category = categories[0], CategoryId = categories[0].Id, Description = "plush dog", IsOnStock = false, Price = 50.6m},
                    new Toy{Name = "Mafia", Category = categories[1], CategoryId = categories[1].Id, Description = "table mafia", IsOnStock = false, Price = 70.6m},
                    new Toy{Name = "Car", Category = categories[2], CategoryId = categories[2].Id, Description = "plastic car", IsOnStock = true, Price = 10.6m},
                    new Toy{Name = "Pencil", Category = categories[3], CategoryId = categories[3].Id, Description = "wooden pencil", IsOnStock = false, Price = 5.6m}
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
            var firstPageWithTwoItems = new PageInfo{PageNumber = 1, PageSize = 2};
            var secondPageWithTwoItems = new PageInfo { PageNumber = 2, PageSize = 2 };
            var firstPageWithFourItems = new PageInfo { PageNumber = 1, PageSize = 4 };
            var secondPageWithFourItems = new PageInfo { PageNumber = 2, PageSize = 4 };
            //Action

            var toysFirstPageWithTwoItems = await toyManager.GetPagedFilteredAsync(filter, firstPageWithTwoItems);
            var toysSecondPageWithTwoItems = await toyManager.GetPagedFilteredAsync(filter, secondPageWithTwoItems);
            var toysFirstPageWithFourItems = await toyManager.GetPagedFilteredAsync(filter, firstPageWithFourItems);
            var toysSecondPageWithFourItems = await toyManager.GetPagedFilteredAsync(filter, secondPageWithFourItems);

            //Assert
            Assert.Equal((uint)2, toysFirstPageWithTwoItems.PageSize);
            Assert.Equal((uint)1, toysFirstPageWithTwoItems.PageNumber);
            Assert.Equal((uint)6, toysFirstPageWithTwoItems.TotalItems);
            Assert.Equal((uint)3, toysFirstPageWithTwoItems.TotalPages);
            Assert.Equal(2, toysFirstPageWithTwoItems.Items.Count());

            Assert.Equal(2, toysSecondPageWithTwoItems.Items.Count());

            Assert.Equal(4, toysFirstPageWithFourItems.Items.Count());
            Assert.Equal((uint)2, toysFirstPageWithFourItems.TotalPages);

            Assert.Equal(2, toysSecondPageWithFourItems.Items.Count());
            Assert.Equal((uint)6, toysSecondPageWithFourItems.TotalItems);
        }
    }
}
