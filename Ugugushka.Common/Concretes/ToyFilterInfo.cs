using Ugugushka.Common.Interfaces;

namespace Ugugushka.Common.Concretes
{
    public class ToyFilterInfo : IToyFilterInfo
    {
        public string SearchString { get; set; }
        public int? CategoryId { get; set; }
        public int? PartitionId { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public bool IsOnStock { get; set; }
    }
}
