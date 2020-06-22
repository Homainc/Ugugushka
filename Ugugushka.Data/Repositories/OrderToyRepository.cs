using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Ugugushka.Data.Code.Abstractions;
using Ugugushka.Data.Code.Interfaces;
using Ugugushka.Data.Models;

namespace Ugugushka.Data.Repositories
{
    public class OrderToyRepository : AbstractRepository<OrderToy>, IOrderToyRepository
    {
        public OrderToyRepository(ApplicationContext db, IHttpContextAccessor httpContextAccessor) : base(db, httpContextAccessor)
        {
        }

        public void AddToSet(ICollection<OrderToy> items) => Db.Set<OrderToy>().AddRange(items);
    }
}
