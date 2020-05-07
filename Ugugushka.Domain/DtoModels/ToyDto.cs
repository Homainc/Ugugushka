namespace Ugugushka.Domain.DtoModels
{
    public class BaseToyDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool IsOnStock { get; set; }
    }

    public class ToyDto : BaseToyDto
    {
        public uint Id { get; set; }
        public string CategoryName { get; set; }
        public string PartitionName { get; set; }
    }

    public class ToyCreateDto : BaseToyDto
    {
        public uint? CategoryId { get; set; }
    }

    public class ToyUpdateDto : ToyCreateDto
    {
        public uint Id { get; set; }
    }
}
