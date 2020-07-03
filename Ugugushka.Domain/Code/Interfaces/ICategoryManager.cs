using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ugugushka.Common.Interfaces;
using Ugugushka.Domain.DtoModels;

namespace Ugugushka.Domain.Code.Interfaces
{
    public interface ICategoryManager
    {
        Task<CategoryDto> SaveAsync(CategoryDto category);
        Task<IEnumerable<IGrouping<PartitionDto, CategoryDto>>> GetAllGroupedByPartitionAsync();
        Task<IEnumerable<CategoryDto>> GetAllAsync();
        Task<IPagedResult<CategoryDto>> GetAllPagedAsync(IPageInfo pageInfo);
        Task<CategoryDto> GetByIdAsync(int id);
        Task<CategoryDto> DeleteAsync(int id);
    }
}
