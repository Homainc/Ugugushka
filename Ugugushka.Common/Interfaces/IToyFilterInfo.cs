namespace Ugugushka.Common.Interfaces
{
    public interface IToyFilterInfo
    {
        string SearchString { get; }
        int? CategoryId { get; }
        int? PartitionId { get; }
        decimal? MinPrice { get; }
        decimal? MaxPrice { get; }
        bool IsOnStock { get; }
    }
}
