namespace Ugugushka.Domain.DtoModels
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PartitionId { get; set; }
        public PartitionDto Partition { get; set; }
    }
}
