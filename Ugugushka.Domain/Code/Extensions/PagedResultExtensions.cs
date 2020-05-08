using System.Collections.Generic;
using AutoMapper;
using Ugugushka.Common.Concretes;
using Ugugushka.Common.Interfaces;

namespace Ugugushka.Domain.Code.Extensions
{
    internal static class PagedResultExtensions
    {
        public static IPagedResult<TDestination> Map<TSource, TDestination>(this IPagedResult<TSource> paged,
            IMapper mapper)
            where TDestination : class where TSource : class
        {
            var mappedItems = mapper.Map<IEnumerable<TSource>, IEnumerable<TDestination>>(paged.Items);
            return new PagedResult<TDestination>(mappedItems,
                new PageInfo {PageSize = paged.PageSize, PageNumber = paged.PageNumber}, paged.TotalItems);
        }
    }
}
