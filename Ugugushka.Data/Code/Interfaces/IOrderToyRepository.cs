using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Ugugushka.Data.Models;

namespace Ugugushka.Data.Code.Interfaces
{
    public interface IOrderToyRepository : IRepository<OrderToy>
    {
        void AddToSet(ICollection<OrderToy> items);
    }
}
