using System.Collections.Generic;
using System.Linq;

namespace Ugugushka.Domain.DtoModels
{
    public class Cart
    {
        private readonly List<CartLine> _lineCollection = new List<CartLine>();

        public void AddItem(ToyDto toy, int quantity)
        {
            var line = _lineCollection
                .FirstOrDefault(t => t.Toy.Id == toy.Id);

            if (line == null)
            {
                _lineCollection.Add(new CartLine { Toy = toy, Quantity = quantity });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public void RemoveLine(ToyDto toy) => _lineCollection.RemoveAll(l => l.Toy.Id == toy.Id);

        public decimal ComputeTotalValue() => _lineCollection.Sum(e => e.Toy.Price * e.Quantity);

        public void Clear() => _lineCollection.Clear();

        public IEnumerable<CartLine> Lines => _lineCollection.AsEnumerable();
    }

    public class CartLine
    {
        public ToyDto Toy { get; set; }
        public int Quantity { get; set; }
    }
}