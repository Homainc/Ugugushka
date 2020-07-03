using System.Collections.Generic;
using Ugugushka.Data.Models;

namespace Ugugushka.Data.Code.Interfaces
{
    public interface IOrderToyRepository : IRepository<OrderToy>
    {
        void AddToSet(ICollection<OrderToy> items);
    }
}
