using System.Collections.Generic;
using System.Threading.Tasks;
using Ugugushka.Common.Interfaces;
using Ugugushka.Data.Models;

namespace Ugugushka.Data.Code.Interfaces
{
    public interface IToyRepository : IRepository<Toy>
    {
        Task<IPagedResult<Toy>> GetFilteredPagedAsync(IToyFilterInfo filter, IPageInfo pageInfo);
        Task<Toy> SingleOrDefaultByIdEagerAsync(int id);
        Task<int> GetPagesCountAsync(int pageSize);
        Task<IEnumerable<int>> GetToyIdsAsync();
        Task<IEnumerable<Toy>> GetSimilarToysAsync(int categoryId, int toyId, int count = 4);
    }
}
