using System.Collections.Generic;
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
        public int? CategoryId { get; set; }
        public IList<string> ImageUrls { get; set; }
    }

    public class ToyUpdateDto : ToyCreateDto
    {
        public int Id { get; set; }
    }

    public class ToyDto : BaseToyDto
    {
        public int Id { get; set; }
        public int? CategoryId { get; set; }
        public ISet<Image> Images { get; set; }
        public Category Category { get; set; }
    }
}
