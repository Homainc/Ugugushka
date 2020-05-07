using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Ugugushka.Common.Interfaces;
using Ugugushka.Data.Code.Abstractions;
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

        public Task<IPagedResult<Toy>> GetFilteredPagedAsync(IToyFilterInfo filter, IPageInfo pageInfo)
        {
            throw new System.NotImplementedException();
        }
    }
}
