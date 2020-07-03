using System.Collections.Generic;
using System.Threading.Tasks;
using Ugugushka.Common.Interfaces;
using Ugugushka.Data.Models;

namespace Ugugushka.Data.Code.Interfaces
{
    public interface IPartitionRepository : IRepository<Partition>
    {
        Task<IEnumerable<Partition>> GetAllAsync();
        Task<IPagedResult<Partition>> GetPagedAsync(IPageInfo pageInfo);
        Task<Partition> SingleByIdOrDefaultAsync(int id);
    }
}
