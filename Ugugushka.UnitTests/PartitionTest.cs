using System.Collections.Generic;
using System.Linq;
using Ugugushka.Common.Concretes;
using Ugugushka.Data.Models;
using Ugugushka.Domain.Code.Exceptions;
using Ugugushka.Domain.DtoModels;
using Ugugushka.UnitTests.Abstractions;
using Xunit;

namespace Ugugushka.UnitTests
{
    public class PartitionTest : AbstractTestWithDb
    {
        public static IEnumerable<object> TestValues => new object[]
        {
            new Partition{Id = 1, Name = "test db part"},
            new Partition{Id = 2, Name = "second db partition"},
            new Category{Id = 1, Name = "test cat", PartitionId = 1}
        };

        [Fact]
        public async void Can_Create_And_Update()
        {
            // Assign
            await using var context = CreateContext("Can_Create_And_Partition");
            await PopulateAsync(TestValues);
            var partitionManager = CreatePartitionManager();
            
            var partition = new PartitionDto {Name = "Test partition"};
            
            var partitionToUpdate = new PartitionDto {Id = 1, Name = "new name"};

            // Action
            var createdPartition = await partitionManager.SaveAsync(partition);

            var updatedPartition = await partitionManager.SaveAsync(partitionToUpdate);

            // Assert
            Assert.NotEqual(0, createdPartition.Id);
            Assert.Equal(partition.Name, createdPartition.Name);

            Assert.Equal(partitionToUpdate.Id, updatedPartition.Id);
            Assert.Equal(partitionToUpdate.Name, updatedPartition.Name);
        }

        [Fact]
        public async void Can_Delete()
        {
            // Assign
            await using var context = CreateContext("Can_Delete_Partition");
            await PopulateAsync(TestValues);
            var partitionManager = CreatePartitionManager();

            // Action
            var deletedPartition1 = await partitionManager.DeleteAsync(1);
            var deletedPartition2 = await partitionManager.DeleteAsync(2);

            // Assert
            Assert.Equal(1, deletedPartition1.Id);
            Assert.Equal(2, deletedPartition2.Id);
            await Assert.ThrowsAsync<NotFoundException>(async () => await partitionManager.DeleteAsync(1));
            await Assert.ThrowsAsync<NotFoundException>(async () => await partitionManager.DeleteAsync(2));
        }

        [Fact]
        public async void Can_Get_All()
        {
            // Assign
            await using var context = CreateContext("Can_Get_All_Partitions");
            await PopulateAsync(TestValues);
            var partitionManager = CreatePartitionManager();

            // Action
            var list = await partitionManager.GetAllAsync(new PageInfo { PageSize = 10, PageNumber = 1});

            // Assert
            Assert.Equal(2, list.TotalItems);
            Assert.Equal(2, list.Items.Count());
        }
    }
}
