using System;
using System.Collections.Generic;
using System.Text;
using Ugugushka.Common.Interfaces;

namespace Ugugushka.Common.Concretes
{
    public class PagedResult<TItem> : IPagedResult<TItem> where TItem : class
    {
        public IEnumerable<TItem> Items { get; }
        public uint PageNumber { get; }
        public uint PageSize { get; }
        public uint TotalItems { get; }
        public uint TotalPages => (uint)(TotalItems / PageSize + (TotalItems % PageSize == 0 ? 0 : 1));

        public PagedResult(IEnumerable<TItem> items, uint pageNumber, uint pageSize, uint totalItems)
        {
            Items = items;
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalItems = totalItems;
        }
    }
}
