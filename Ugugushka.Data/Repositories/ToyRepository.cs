using Microsoft.AspNetCore.Http;
using Ugugushka.Data.Code.Abstractions;
using Ugugushka.Data.Code.Interfaces;
using Ugugushka.Data.Models;

namespace Ugugushka.Data.Repositories
{
    internal class ToyRepository : AbstractRepository<Toy>, IToyRepository
    {
        public ToyRepository(ApplicationContext db, IHttpContextAccessor httpContextAccessor) : base(db,
            httpContextAccessor)
        {
        }


    }
}
