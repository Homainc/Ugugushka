namespace Ugugushka.Common.Interfaces
{
    public interface IToyFilterInfo
    {
        string SearchString { get; }
        uint? CategoryId { get; }
        uint? PartitionId { get; }
        uint? MinPrice { get; }
        uint? MaxPrice { get; }
    }
}
