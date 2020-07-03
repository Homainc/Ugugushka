using System.Collections.Generic;
using Ugugushka.Data.Models;
using Ugugushka.Domain.Code.Exceptions;
using Ugugushka.Domain.DtoModels;
using Ugugushka.UnitTests.Abstractions;
using Xunit;

namespace Ugugushka.UnitTests
{
    public class CategoryTest : AbstractTestWithDb
    {

        public static IEnumerable<Category> TestValues => new[]
        {
            new Category
            {
                Id = 1, Name = "test db cat", PartitionId = 1, Partition = new Partition
                {
                    Id = 1,
                    Name = "test part"
                }
            },
            new Category
            {
                Id = 2, Name = "test db cat2", PartitionId = 2, Partition = new Partition
                {
                    Id = 2,
                    Name = "test part2"
                }
            },
        };

        [Fact]
        public async void Can_Create_And_Update()
        {
            // Assign
            await using var context = CreateContext("Can_Create_And_Update_Category");
            await PopulateAsync(TestValues);
            var categoryManager = CreateCategoryManager();

            var category = new CategoryDto {Name = "created"};
            var categoryToUpdate = new CategoryDto {Id = 1, Name = "updated"};
            
            // Action
            var createdCategory = await categoryManager.SaveAsync(category);
            var updatedCategory = await categoryManager.SaveAsync(categoryToUpdate);

            // Assert
            Assert.Equal(category.Name, createdCategory.Name);
            Assert.Equal(categoryToUpdate.Id, updatedCategory.Id);
            Assert.Equal(categoryToUpdate.Name, updatedCategory.Name);
        }

        [Fact]
        public async void Can_Delete()
        {
            // Assign
            await using var context = CreateContext("Can_Delete_Category");
            await PopulateAsync(TestValues);
            var categoryManager = CreateCategoryManager();

            // Action
            var deletedCategory1 = await categoryManager.DeleteAsync(1);
            var deletedCategory2 = await categoryManager.DeleteAsync(2);

            // Assert
            Assert.Equal(1, deletedCategory1.Id);
            await Assert.ThrowsAsync<NotFoundException>(async () => await categoryManager.DeleteAsync(1));
            Assert.Equal(2, deletedCategory2.Id);
            await Assert.ThrowsAsync<NotFoundException>(async () => await categoryManager.DeleteAsync(2));
        }

        [Fact]
        public async void Can_Get_All_Grouped()
        {
            // Assign
            await using var context = CreateContext("Can_Get_All_Grouped_Category");

            // Action


            // Assert

        }
    }
}
