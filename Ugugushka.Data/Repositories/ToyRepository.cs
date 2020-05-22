﻿using System.Linq;
using System.Threading.Tasks;
using Binbin.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Ugugushka.Common.Interfaces;
using Ugugushka.Data.Code.Abstractions;
using Ugugushka.Data.Code.Extensions;
using Ugugushka.Data.Code.Interfaces;
using Ugugushka.Data.Models;

namespace Ugugushka.Data.Repositories
{
    public class ToyRepository : AbstractRepository<Toy>, IToyRepository
    {
        public ToyRepository(ApplicationContext db, IHttpContextAccessor httpContextAccessor) : base(db,
            httpContextAccessor)
        {
        }

        public async Task<IPagedResult<Toy>> GetFilteredPagedAsync(IToyFilterInfo filter, IPageInfo pageInfo)
        {
            var toyFilter = PredicateBuilder.True<Toy>();
            if (filter.IsOnStock)
                toyFilter = toyFilter.And(x => x.IsOnStock);
            if (filter.CategoryId.HasValue)
                toyFilter = toyFilter.And(x => x.CategoryId == filter.CategoryId.Value);
            if (filter.MinPrice.HasValue)
                toyFilter = toyFilter.And(x => x.Price >= filter.MinPrice.Value);
            if (filter.MaxPrice.HasValue)
                toyFilter = toyFilter.And(x => x.Price <= filter.MaxPrice.Value);
            if (!string.IsNullOrWhiteSpace(filter.SearchString))
                toyFilter = toyFilter.And(x => x.Name.Contains(filter.SearchString));

            var partitionFilter = PredicateBuilder.True<Partition>();
            if (filter.PartitionId.HasValue)
                partitionFilter = partitionFilter.And(x => x.Id == filter.PartitionId.Value);

            return await (
                from t in Db.Toys.Where(toyFilter).Include(x => x.Images)
                join c in Db.Categories on t.CategoryId equals c.Id into cGroup
                from c in cGroup.DefaultIfEmpty()
                join p in Db.Partitions.Where(partitionFilter) on c.PartitionId equals p.Id into pGroup
                from p in pGroup.DefaultIfEmpty()
                select new Toy
                {
                    Id = t.Id,
                    Name = t.Name,
                    CategoryId = c.Id,
                    Category = c != null? new Category
                    {
                        Id = c.Id,
                        PartitionId = p.Id,
                        Partition = p,
                        Name = c.Name
                    } : null,
                    Description = t.Description,
                    IsOnStock = t.IsOnStock,
                    Price = t.Price,
                    Images = t.Images
                })
                .ToPagedAsync(pageInfo, CancellationToken);
        }

        public async Task<Toy> SingleOrDefaultByIdEagerAsync(int id) =>
            await (from t in Db.Toys.Where(x => x.Id == id).Include(x => x.Images)
                join c in Db.Categories on t.CategoryId equals c.Id into cGroup
                from c in cGroup.DefaultIfEmpty()
                join p in Db.Partitions on c.PartitionId equals p.Id into pGroup
                from p in pGroup.DefaultIfEmpty()
                select new Toy
                {
                    Id = t.Id,
                    Name = t.Name,
                    CategoryId = c.Id,
                    Category = c != null
                        ? new Category
                        {
                            Id = c.Id,
                            PartitionId = p.Id,
                            Partition = p,
                            Name = c.Name
                        }
                        : null,
                    Description = t.Description,
                    IsOnStock = t.IsOnStock,
                    Price = t.Price,
                    Images = t.Images
                }).SingleOrDefaultAsync(CancellationToken);
    }
}
