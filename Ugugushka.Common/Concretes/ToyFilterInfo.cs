using Ugugushka.Common.Interfaces;

namespace Ugugushka.Common.Concretes
{
    public class ToyFilterInfo : IToyFilterInfo
    {
        public string SearchString { get; set; }
        public uint? CategoryId { get; set; }
        public uint? PartitionId { get; set; }
        public uint? MinPrice { get; set; }
        public uint? MaxPrice { get; set; }
    }
}
