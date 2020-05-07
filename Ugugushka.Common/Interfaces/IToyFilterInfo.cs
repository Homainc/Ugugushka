namespace Ugugushka.Common.Interfaces
{
    public interface IToyFilterInfo
    {
        string SearchString { get; }
        uint? CategoryId { get; }
        uint? PartitionId { get; }
        decimal? MinPrice { get; }
        decimal? MaxPrice { get; }
        bool IsOnStock { get; }
    }
}
