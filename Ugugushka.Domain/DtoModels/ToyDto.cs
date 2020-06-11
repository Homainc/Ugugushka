using System.Collections.Generic;
using System.Linq;
using Ugugushka.Data.Models;

namespace Ugugushka.Domain.DtoModels
{
    public class BaseToyDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }
        public bool IsOnStock => Count > 0;
    }

    public class ToyUpdateDto : BaseToyDto
    {
        public int Id { get; set; }
        public int? CategoryId { get; set; }
        public IList<ToyImageDto> Images { get; set; }
    }

    public class ToyDto : ToyUpdateDto
    {
        public Category Category { get; set; }
        public ToyImageDto MainImage => Images?.FirstOrDefault(x => x.IsMain);
        public IEnumerable<ToyImageDto> ExtraImages => Images?.Where(x => !x.IsMain).ToList();
    }
}
