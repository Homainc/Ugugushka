using Microsoft.AspNetCore.Http;
using Ugugushka.Data.Code.Abstractions;
using Ugugushka.Data.Code.Interfaces;
using Ugugushka.Data.Models;

namespace Ugugushka.Data.Repositories
{
    public class ToyImageRepository : AbstractRepository<ToyImage>, IToyImageRepository
    {
        public ToyImageRepository(ApplicationContext db, IHttpContextAccessor httpContextAccessor) : base(db, httpContextAccessor)
        { }
    }
}
