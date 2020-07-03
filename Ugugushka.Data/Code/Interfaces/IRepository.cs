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
        void Attach(TItem item);
        void Update(TItem item);
        Task AddRangeAsync(IEnumerable<TItem> items);
        void RemoveRange(IEnumerable<TItem> items);
        void UpdateRange(IEnumerable<TItem> items);
    }
}
