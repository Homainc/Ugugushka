using Ugugushka.Common.Interfaces;

namespace Ugugushka.Common.Concretes
{
    public class PageInfo : IPageInfo
    {
        public uint PageNumber { get; set; }
        public uint PageSize { get; set; }
    }
}
