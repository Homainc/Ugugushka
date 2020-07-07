using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
                toyFilter = toyFilter.And(x => x.Count > 0);
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

            var query = filter.PartitionId.HasValue ?
                AllQuery(toyFilter, partitionFilter) :
                AllQuery(toyFilter);

            return await query.ToPagedAsync(pageInfo, CancellationToken);
        }

        private IQueryable<Toy> AllQuery(Expression<Func<Toy, bool>> toyFilter, Expression<Func<Partition, bool>> partitionFilter)
            => from t in Db.Toys.Where(toyFilter).Include(x => x.Images)
               join c in Db.Categories on t.CategoryId equals c.Id
               join p in Db.Partitions.Where(partitionFilter) on c.PartitionId equals p.Id
               select new Toy
               {
                   Id = t.Id,
                   Name = t.Name,
                   CategoryId = c.Id,
                   Category = c != null ? new Category
                   {
                       Id = c.Id,
                       PartitionId = p.Id,
                       Partition = p != null ? new Partition
                       {
                           Id = p.Id,
                           Name = p.Name
                       } : null,
                       Name = c.Name
                   } : null,
                   Description = t.Description,
                   Count = t.Count,
                   Price = t.Price,
                   Images = t.Images
               };

        private IQueryable<Toy> AllQuery(Expression<Func<Toy, bool>> toyFilter = null)
        {
            toyFilter ??= PredicateBuilder.True<Toy>();

            return
                from t in Db.Toys.Where(toyFilter).Include(x => x.Images)
                join c in Db.Categories on t.CategoryId equals c.Id into cGroup
                from c in cGroup.DefaultIfEmpty()
                join p in Db.Partitions on c.PartitionId equals p.Id into pGroup
                from p in pGroup.DefaultIfEmpty()
                select new Toy
                {
                    Id = t.Id,
                    Name = t.Name,
                    CategoryId = c.Id,
                    Category = c != null ? new Category
                    {
                        Id = c.Id,
                        PartitionId = p.Id,
                        Partition = p != null ? new Partition
                        {
                            Id = p.Id,
                            Name = p.Name
                        } : null,
                        Name = c.Name
                    } : null,
                    Description = t.Description,
                    Count = t.Count,
                    Price = t.Price,
                    Images = t.Images
                };
        }

        public async Task<Toy> SingleOrDefaultByIdEagerAsync(int id) =>
            await AllQuery().AsNoTracking().SingleOrDefaultAsync(x => x.Id == id, CancellationToken);

        public async Task<int> GetPagesCountAsync(int pageSize)
        {
            var value = (double)(await Db.Toys.CountAsync(CancellationToken)) / pageSize;
            if ((int)value < value)
                value++;
            return (int)value;
        }

        public async Task<IEnumerable<int>> GetToyIdsAsync() =>
            await (from t in Db.Toys select t.Id).ToListAsync(CancellationToken);

        public async Task<IEnumerable<Toy>> GetSimilarToysAsync(int categoryId, int toyId, int count = 4) =>
            await (
                 from t in Db.Toys.Where(x => x.Id != toyId && x.CategoryId == categoryId).Include(x => x.Images)
                 join c in Db.Categories on t.CategoryId equals c.Id
                 join p in Db.Partitions on c.PartitionId equals p.Id
                 select new Toy
                 {
                     Id = t.Id,
                     Name = t.Name,
                     CategoryId = c.Id,
                     Category = c != null ? new Category
                     {
                         Id = c.Id,
                         PartitionId = p.Id,
                         Partition = p != null ? new Partition
                         {
                             Id = p.Id,
                             Name = p.Name
                         } : null,
                         Name = c.Name
                     } : null,
                     Description = t.Description,
                     Count = t.Count,
                     Price = t.Price,
                     Images = t.Images
                 }).Take(count).ToListAsync(CancellationToken);
    }
}
