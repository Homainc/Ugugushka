using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Ugugushka.Common.Interfaces;
using Ugugushka.Data.Code.Abstractions;
using Ugugushka.Data.Code.Extensions;
using Ugugushka.Data.Code.Interfaces;
using Ugugushka.Data.Models;

namespace Ugugushka.Data.Repositories
{
    public class OrderRepository : AbstractRepository<Order>, IOrderRepository
    {
        public OrderRepository(ApplicationContext db, IHttpContextAccessor httpContextAccessor) : base(db, httpContextAccessor)
        {
        }

        public async Task<Order> SingleByIdEagerOrDefaultAsync(Guid id) =>
            await (
                from o in Db.Orders.Include(x => x.OrderToys).ThenInclude(x => x.Toy)
                select new Order
                {
                    Id = o.Id,
                    ApartmentNumber = o.ApartmentNumber,
                    DeliveryType = o.DeliveryType,
                    Email = o.Email,
                    ExitNumber = o.ExitNumber,
                    FirstName = o.FirstName,
                    FloorNumber = o.FloorNumber,
                    HouseNumber = o.HouseNumber,
                    LastName = o.LastName,
                    TotalPrice = o.TotalPrice,
                    PhoneNumber = o.PhoneNumber,
                    OrderToys = o.OrderToys,
                    Street = o.Street,
                    Date = o.Date,
                    Status = o.Status
                }).SingleOrDefaultAsync(x => x.Id == id, CancellationToken);

        public async Task<IPagedResult<Order>> GetFilteredPagedAsync(IOrderFilterInfo orderFilter, IPageInfo pageInfo) =>
            await (
                from o in Db.Orders
                orderby o.Status, o.Date descending 
                select new Order
                {
                    Id = o.Id,
                    ApartmentNumber = o.ApartmentNumber,
                    DeliveryType = o.DeliveryType,
                    Email = o.Email,
                    ExitNumber = o.ExitNumber,
                    FirstName = o.FirstName,
                    FloorNumber = o.FloorNumber,
                    HouseNumber = o.HouseNumber,
                    LastName = o.LastName,
                    TotalPrice = o.TotalPrice,
                    PhoneNumber = o.PhoneNumber,
                    OrderToys = o.OrderToys,
                    Street = o.Street,
                    Date = o.Date,
                    Status = o.Status
                }).ToPagedAsync(pageInfo, CancellationToken);
    }
}
