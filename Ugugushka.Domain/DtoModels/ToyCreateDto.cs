namespace Ugugushka.Domain.DtoModels
{
    public class ToyCreateDto
    {
        public uint Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool IsOnStock { get; set; }
        public uint? CategoryId { get; set; }
    }
}
