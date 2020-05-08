using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Ugugushka.Common.Concretes;
using Ugugushka.Common.Interfaces;

namespace Ugugushka.Data.Code.Extensions
{
    internal static class QueryablePagedExtension
    {
        public static async Task<IPagedResult<TITem>> ToPagedAsync<TITem>(this IQueryable<TITem> query, IPageInfo pageInfo, CancellationToken token = default) where TITem : class
        {
            var totalItems = await query.CountAsync(token);
            var items = await query.Skip((pageInfo.PageNumber - 1) * pageInfo.PageSize).Take(pageInfo.PageSize).ToListAsync(token);
            
            return new PagedResult<TITem>(items, pageInfo, totalItems);
        }
    }
}
