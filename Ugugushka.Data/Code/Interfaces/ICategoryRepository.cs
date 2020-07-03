using System.Collections.Generic;
using System.Threading.Tasks;
using Ugugushka.Common.Interfaces;
using Ugugushka.Data.Models;

namespace Ugugushka.Data.Code.Interfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<IPagedResult<Category>> GetAllPagedAsync(IPageInfo pageInfo);
        Task<Category> SingleByIdOrDefaultAsync(int id);
        Task<bool> AnyByIdAsync(int id);
    }
}
