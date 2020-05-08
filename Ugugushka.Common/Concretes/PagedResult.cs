using System.Collections.Generic;
using Ugugushka.Common.Interfaces;

namespace Ugugushka.Common.Concretes
{
    public class PagedResult<TItem> : IPagedResult<TItem> where TItem : class
    {
        public IEnumerable<TItem> Items { get; }
        public int PageNumber { get; }
        public int PageSize { get; }
        public int TotalItems { get; }
        public int TotalPages => TotalItems / PageSize + (TotalItems % PageSize == 0 ? 0 : 1);

        public PagedResult(IEnumerable<TItem> items, IPageInfo pageInfo, int totalItems)
        {
            Items = items;
            PageNumber = pageInfo.PageNumber;
            PageSize = pageInfo.PageSize;
            TotalItems = totalItems;
        }
    }
}
