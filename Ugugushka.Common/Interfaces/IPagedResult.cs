using System.Collections.Generic;

namespace Ugugushka.Common.Interfaces
{
    public interface IPagedResult<out TItem> where TItem : class
    {
        IEnumerable<TItem> Items { get; }
        int PageNumber { get; }
        int PageSize { get; }
        int TotalItems { get; }
        int TotalPages { get; }
    }
}
