using Ugugushka.Common.Interfaces;

namespace Ugugushka.Common.Concretes
{
    public class PageInfo : IPageInfo
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
