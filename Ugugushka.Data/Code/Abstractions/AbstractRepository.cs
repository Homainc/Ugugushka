using System.Threading;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Ugugushka.Data.Code.Interfaces;

namespace Ugugushka.Data.Code.Abstractions
{
    public abstract class AbstractRepository<TItem> : IRepository<TItem> where TItem : class
    {
        protected readonly ApplicationContext Db;
        protected readonly CancellationToken CancellationToken;

        public AbstractRepository(ApplicationContext db, IHttpContextAccessor httpContextAccessor)
        {
            Db = db;
            CancellationToken = httpContextAccessor.HttpContext.RequestAborted;
        }

        public void Create(TItem item) => Db.Entry(item).State = EntityState.Added;

        public void Update(TItem item) => Db.Entry(item).State = EntityState.Modified;

        public void Delete(TItem item) => Db.Entry(item).State = EntityState.Deleted;
    }
}
