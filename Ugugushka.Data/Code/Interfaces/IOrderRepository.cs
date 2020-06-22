using System;
using System.Threading.Tasks;
using Ugugushka.Common.Interfaces;
using Ugugushka.Data.Models;

namespace Ugugushka.Data.Code.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<IPagedResult<Order>> GetFilteredPagedAsync(IOrderFilterInfo orderFilter, IPageInfo pageInfo);
        Task<Order> SingleByIdEagerOrDefaultAsync(Guid id);
    }
}
