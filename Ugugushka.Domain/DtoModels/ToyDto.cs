using Ugugushka.Data.Models;

namespace Ugugushka.Domain.DtoModels
{
    public class BaseToyDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool IsOnStock { get; set; }
    }

    public class ToyCreateDto : BaseToyDto
    {
        public uint? CategoryId { get; set; }
    }

    public class ToyUpdateDto : ToyCreateDto
    {
        public uint Id { get; set; }
    }

    public class ToyDto : ToyUpdateDto
    {
        public Category Category { get; set; }
    }
}
