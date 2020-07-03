using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Ugugushka.Common.Interfaces;
using Ugugushka.Data.Code.Abstractions;
using Ugugushka.Data.Code.Extensions;
using Ugugushka.Data.Code.Interfaces;
using Ugugushka.Data.Models;

namespace Ugugushka.Data.Repositories
{
    public class CategoryRepository : AbstractRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationContext db, IHttpContextAccessor httpContextAccessor) : base(db,
            httpContextAccessor)
        {
        }

        public async Task<IEnumerable<Category>> GetAllAsync() =>
            await (
                from c in Db.Categories
                join p in Db.Partitions on c.PartitionId equals p.Id into pGrouping
                from p in pGrouping.DefaultIfEmpty()
                select new Category
                {
                    Id = c.Id,
                    Name = c.Name,
                    PartitionId = c.PartitionId,
                    Partition = p != null ? new Partition
                    {
                        Id = p.Id,
                        Name = p.Name
                    }: null
                }).ToListAsync(CancellationToken);

        public async Task<Category> SingleByIdOrDefaultAsync(int id) =>
            await(
                from c in Db.Categories
                join p in Db.Partitions on c.PartitionId equals p.Id into pGrouping
                from p in pGrouping.DefaultIfEmpty()
                select new Category
                {
                    Id = c.Id,
                    Name = c.Name,
                    PartitionId = c.PartitionId,
                    Partition = p != null ? new Partition
                    {
                        Id = p.Id,
                        Name = p.Name
                    } : null
                }).SingleOrDefaultAsync(x => x.Id == id, CancellationToken);

        public async Task<bool> AnyByIdAsync(int id) => 
            await Db.Categories.AnyAsync(x => x.Id == id, CancellationToken);

        public async Task<IPagedResult<Category>> GetAllPagedAsync(IPageInfo pageInfo) =>
            await (
                from c in Db.Categories
                join p in Db.Partitions on c.PartitionId equals p.Id into pGrouping
                from p in pGrouping.DefaultIfEmpty()
                select new Category
                {
                    Id = c.Id,
                    Name = c.Name,
                    PartitionId = c.PartitionId,
                    Partition = p != null ? new Partition
                    {
                        Id = p.Id,
                        Name = p.Name
                    } : null
                }).ToPagedAsync(pageInfo, CancellationToken);
    }
}
