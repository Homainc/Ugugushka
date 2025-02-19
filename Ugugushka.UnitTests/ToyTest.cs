﻿using System.Collections.Generic;
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
    public class ToyTest : AbstractTestWithDb
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public ToyTest(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        public static IEnumerable<Toy> TestToys
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
                        Description = "plush bear", Count = 1, Price = 25.8m, Id = 1,
                        Images = new[]
                        {
                            new ToyImage{PublicId = "old_1", IsMain = true},
                            new ToyImage{PublicId = "old_2"} 
                        }.ToHashSet()
                    },
                    new Toy
                    {
                        Name = "Rabbit", Category = categories[0], CategoryId = categories[0].Id,
                        Description = "plush rabbit", Count = 1, Price = 26.4m, Id = 2,
                        Images = new[]
                        {
                            new ToyImage{PublicId = "old_3", IsMain = true},
                            new ToyImage{PublicId = "old_4"}
                        }.ToHashSet()
                    },
                    new Toy
                    {
                        Name = "Dog", Category = categories[0], CategoryId = categories[0].Id,
                        Description = "plush dog", Price = 50.6m, Id = 3
                    },
                    new Toy
                    {
                        Name = "Mafia", Category = categories[1], CategoryId = categories[1].Id,
                        Description = "table mafia", Price = 70.6m, Id = 4
                    },
                    new Toy
                    {
                        Name = "Car 1", Category = categories[2], CategoryId = categories[2].Id,
                        Description = "plastic car", Count = 1, Price = 10.6m, Id = 5
                    },
                    new Toy
                    {
                        Name = "Pencil", Category = categories[3], CategoryId = categories[3].Id,
                        Description = "wooden pencil", Price = 5.6m, Id = 6
                    }
                };
                return toys;
            }
        }

        [Fact]
        public async void Can_Paginate()
        {
            //Assign
            await using var context = CreateContext("Can_Paginate_Toy");
            await PopulateAsync(TestToys);
            var (toyManager, _) = CreateToyManager();
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
            Assert.Equal(2, toysFirstPageWithTwoItems.PageSize);
            Assert.Equal(1, toysFirstPageWithTwoItems.PageNumber);
            Assert.Equal(6, toysFirstPageWithTwoItems.TotalItems);
            Assert.Equal(3, toysFirstPageWithTwoItems.TotalPages);
            Assert.Equal(2, toysFirstPageWithTwoItems.Items.Count());

            Assert.Equal(2, toysSecondPageWithTwoItems.Items.Count());

            Assert.Equal(4, toysFirstPageWithFourItems.Items.Count());
            Assert.Equal(2, toysFirstPageWithFourItems.TotalPages);

            Assert.Equal(2, toysSecondPageWithFourItems.Items.Count());
            Assert.Equal(6, toysSecondPageWithFourItems.TotalItems);
        }

        [Fact]
        public async void Can_Filtering()
        {
            //Assign
            await using var context = CreateContext("Can_Filtering_Toy");
            await PopulateAsync(TestToys);
            var (toyManager, _) = CreateToyManager();

            var pageInfo = new PageInfo {PageSize = 6, PageNumber = 1};
            var plushCategory = new ToyFilterInfo {CategoryId = 1};
            var plushCategoryWithMinTwentySixAndMaxThirty =
                new ToyFilterInfo {CategoryId = 1, MinPrice = 26m, MaxPrice = 30m};
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
            Assert.All(plushToys.Items, x => Assert.Equal("Plush", x.Category.Name));
            Assert.Equal(3, plushToys.TotalItems);

            Assert.All(plushCategoryWithMinTwentySixAndMaxThirtyToys.Items,
                x => Assert.Equal("Plush", x.Category.Name));
            Assert.All(plushCategoryWithMinTwentySixAndMaxThirtyToys.Items,
                x => Assert.InRange(x.Price, 26m, 30m));
            Assert.Equal(1, plushCategoryWithMinTwentySixAndMaxThirtyToys.TotalItems);

            Assert.All(isOnStockToys.Items, x => Assert.True(x.IsOnStock));
            Assert.Equal(3, isOnStockToys.TotalItems);

            Assert.All(maxThirtyToys.Items, x => Assert.True(x.Price <= 30m));
            Assert.Equal(4, maxThirtyToys.TotalItems);

            Assert.All(minThirtyToys.Items, x => Assert.True(x.Price >= 30m));
            Assert.Equal(2, minThirtyToys.TotalItems);

            Assert.All(searchCarToys.Items, x => Assert.Contains("Car", x.Name));
            Assert.Equal(1, searchCarToys.TotalItems);
        }

        [Fact]
        public async void Can_Create()
        {
            //Assign
            await using var context = CreateContext("Can_Create_Toy");
            await PopulateAsync(TestToys);
            var (toyManager, pictureManager) = CreateToyManager();
            var newPlushToy = new ToyUpdateDto
            {
                CategoryId = 1, 
                Description = "New plush toy", 
                Count = 1, 
                Name = "New plush toy", 
                Price = 10m, 
                Images = new[]
                {
                    new ToyImageDto{ PublicId = "new_1", IsMain = true},
                    new ToyImageDto{ PublicId = "new_2" }
                }
            };
            var newPlasticToy = new ToyUpdateDto
            {
                CategoryId = 3, 
                Description = "New plastic toy", 
                Name = "New plastic toy", 
                Price = 50.54m,
                Images = new[]
                {
                    new ToyImageDto{ PublicId = "new_3", IsMain = true},
                    new ToyImageDto{ PublicId = "new_4"},
                }
            };
            var newPublicIds = newPlushToy.Images.Concat(newPlasticToy.Images).Select(x => x.PublicId).ToHashSet();

            //Action
            var createdPlushToy = await toyManager.SaveAsync(newPlushToy);
            var createdPlasticToy = await toyManager.SaveAsync(newPlasticToy);
            var toys = await toyManager.GetPagedFilteredAsync(new ToyFilterInfo(),
                new PageInfo {PageNumber = 1, PageSize = 1});

            //Assert
            Assert.Equal(newPlushToy.Name, createdPlushToy.Name);
            _testOutputHelper.WriteLine(createdPlushToy.Category?.ToString() ?? "null");
            Assert.Equal("Plush", createdPlushToy.Category.Name);
            Assert.Equal("For children", createdPlushToy.Category.Partition.Name);
            Assert.Equal(newPlushToy.IsOnStock, createdPlushToy.IsOnStock);
            Assert.Equal(newPlushToy.Description, createdPlushToy.Description);
            Assert.Equal(newPlushToy.Price, createdPlushToy.Price);

            Assert.Equal(newPlasticToy.Name, createdPlasticToy.Name);
            Assert.Equal("Plastic", createdPlasticToy.Category.Name);
            Assert.Equal("For adults", createdPlasticToy.Category.Partition.Name);
            Assert.Equal(newPlasticToy.IsOnStock, createdPlasticToy.IsOnStock);
            Assert.Equal(newPlasticToy.Description, createdPlasticToy.Description);
            Assert.Equal(newPlasticToy.Price, createdPlasticToy.Price);

            Assert.Equal(newPublicIds, pictureManager.PublicIdsWithToyTag);

            Assert.Equal(8, toys.TotalItems);
        }

        [Fact]
        public async void Can_Update()
        {
            //Assign
            await using var context = CreateContext("Can_Update_Toy");
            await PopulateAsync(TestToys);
            var (toyManager, pictureManager) = CreateToyManager();
            var newWoodenToy = new ToyUpdateDto
            {
                Id = 1, 
                CategoryId = 4, 
                Description = "Updated wooden toy", 
                Count = 1,
                Name = "updated first toy", Price = 10m,
                Images = new[]
                {
                    new ToyImageDto{PublicId = "old_1", IsMain = true},
                    new ToyImageDto{PublicId = "new_1"}
                }
            };
            var newSecondWoodenToy = new ToyUpdateDto
            {
                Id = 2, 
                CategoryId = 4, 
                Description = "Updated second wooden toy", 
                Name = "updated second toy",
                Price = 50.54m,
                Images = new[]
                {
                    new ToyImageDto{PublicId = "new_2", IsMain = true},
                    new ToyImageDto{PublicId = "new_3"}
                }
            };

            //Action
            await toyManager.SaveAsync(newWoodenToy);
            await toyManager.SaveAsync(newSecondWoodenToy);
            var toys = await toyManager.GetPagedFilteredAsync(new ToyFilterInfo {CategoryId = 4},
                new PageInfo {PageNumber = 1, PageSize = 1});

            var updatedWoodenToy = await toyManager.GetByIdAsync(newWoodenToy.Id);
            var updatedSecondWoodenToy = await toyManager.GetByIdAsync(newSecondWoodenToy.Id);

            //Assert
            Assert.Equal(newWoodenToy.Name, updatedWoodenToy.Name);
            Assert.Equal("Wood", updatedWoodenToy.Category.Name);
            Assert.Equal("For adults", updatedWoodenToy.Category.Partition.Name);
            Assert.Equal(newWoodenToy.IsOnStock, updatedWoodenToy.IsOnStock);
            Assert.Equal(newWoodenToy.Description, updatedWoodenToy.Description);
            Assert.Equal(newWoodenToy.Price, updatedWoodenToy.Price);

            Assert.Equal(newSecondWoodenToy.Name, updatedSecondWoodenToy.Name);
            Assert.Equal("Wood", updatedSecondWoodenToy.Category.Name);
            Assert.Equal("For adults", updatedSecondWoodenToy.Category.Partition.Name);
            Assert.Equal(newSecondWoodenToy.IsOnStock, updatedSecondWoodenToy.IsOnStock);
            Assert.Equal(newSecondWoodenToy.Description, updatedSecondWoodenToy.Description);
            Assert.Equal(newSecondWoodenToy.Price, updatedSecondWoodenToy.Price);

            Assert.Equal(new[]{ "new_1", "new_2", "new_3" }, pictureManager.PublicIdsWithToyTag);
            Assert.Equal(new[] { "old_2", "old_3", "old_4" }, pictureManager.PublicIdsWithTempTag);

            Assert.Equal(3, toys.TotalItems);
        }

        [Fact]
        public async void Can_Delete()
        {
            //Assign
            await using var context = CreateContext("Can_Delete_Toy");
            await PopulateAsync(TestToys);
            var (toyManager, _) = CreateToyManager();

            //Action
            await toyManager.DeleteAsync(1);
            await toyManager.DeleteAsync(2);
            var toys = await toyManager.GetPagedFilteredAsync(new ToyFilterInfo(),
                new PageInfo {PageNumber = 1, PageSize = 10});

            //Assert
            await Assert.ThrowsAsync<NotFoundException>(async () => await toyManager.GetByIdAsync(1));
            await Assert.ThrowsAsync<NotFoundException>(async () => await toyManager.GetByIdAsync(2));
            Assert.Equal(4, toys.TotalItems);
        }
    }
}
