using System.Collections.Generic;
using System.Threading.Tasks;
using Ugugushka.Common.Interfaces;
using Ugugushka.Domain.DtoModels;

namespace Ugugushka.Domain.Code.Interfaces
{
    public interface IPartitionManager
    {
        Task<IPagedResult<PartitionDto>> GetPagedAsync(IPageInfo pageInfo);
        Task<IEnumerable<PartitionDto>> GetAllAsync();
        Task<PartitionDto> SaveAsync(PartitionDto partition);
        Task<PartitionDto> GetByIdAsync(int id);
        Task<PartitionDto> DeleteAsync(int id);
    }
}
