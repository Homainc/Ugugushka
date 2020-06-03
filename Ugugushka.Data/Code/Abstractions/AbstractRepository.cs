using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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

        public void SetAdded(TItem item) => Db.Entry(item).State = EntityState.Added;

        public void SetModified(TItem item) => Db.Entry(item).State = EntityState.Modified;

        public void SetDeleted(TItem item) => Db.Entry(item).State = EntityState.Deleted;

        public async Task AddAsync(TItem item) => await Db.AddAsync(item, CancellationToken);
        public void Update(TItem item) => Db.Update(item);
        public async Task AddRangeAsync(IEnumerable<TItem> items) => await Db.AddRangeAsync(items, CancellationToken);
        public void RemoveRange(IEnumerable<TItem> items) => Db.RemoveRange(items);
        public void UpdateRange(IEnumerable<TItem> items) => Db.UpdateRange(items);
    }
}
