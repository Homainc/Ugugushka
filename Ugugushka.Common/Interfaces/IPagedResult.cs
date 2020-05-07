using System.Collections.Generic;

namespace Ugugushka.Common.Interfaces
{
    public interface IPagedResult<out TItem> where TItem : class
    {
        IEnumerable<TItem> Items { get; }
        uint PageNumber { get; }
        uint PageSize { get; }
        uint TotalItems { get; }
        uint TotalPages { get; }
    }
}
