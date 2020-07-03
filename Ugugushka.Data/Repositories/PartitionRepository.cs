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
    public class PartitionRepository : AbstractRepository<Partition>, IPartitionRepository
    {
        public PartitionRepository(ApplicationContext db, IHttpContextAccessor httpContextAccessor) : base(db,
            httpContextAccessor)
        {
        }

        public async Task<IPagedResult<Partition>> GetPagedAsync(IPageInfo pageInfo) =>
            await (
                    from p in Db.Partitions
                    select new Partition
                    {
                        Id = p.Id,
                        Name = p.Name
                    })
                .ToPagedAsync(pageInfo, CancellationToken);

        public async Task<IEnumerable<Partition>> GetAllAsync() =>
            await (
                from p in Db.Partitions
                select new Partition
                {
                    Id = p.Id,
                    Name = p.Name
                }).ToListAsync(CancellationToken);

        public async Task<Partition> SingleByIdOrDefaultAsync(int id) =>
                await (
                        from p in Db.Partitions
                        select new Partition { Id = p.Id, Name = p.Name })
                    .SingleOrDefaultAsync(x => x.Id == id, CancellationToken);
    }
}
