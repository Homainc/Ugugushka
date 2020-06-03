using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ugugushka.Data.Code.Interfaces
{
    public interface IRepository<in TItem> where TItem : class
    {
        void SetAdded(TItem item);
        void SetModified(TItem item);
        void SetDeleted(TItem item);
        Task AddAsync(TItem item);
        void Update(TItem item);
        public Task AddRangeAsync(IEnumerable<TItem> items);
        public void RemoveRange(IEnumerable<TItem> items);
        public void UpdateRange(IEnumerable<TItem> items);
    }
}
