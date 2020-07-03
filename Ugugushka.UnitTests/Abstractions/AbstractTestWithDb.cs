using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Moq;
using Ugugushka.Data;
using Ugugushka.Data.Code.Interfaces;
using Ugugushka.Data.Models;
using Ugugushka.Data.Repositories;
using Ugugushka.Domain.Code.Config;
using Ugugushka.Domain.Code.Interfaces;
using Ugugushka.Domain.Code.MapperProfiles;
using Ugugushka.Domain.Managers;
using Ugugushka.UnitTests.FakeConcretes;
using Ugugushka.WebUI.Code.MapperProfiles;

namespace Ugugushka.UnitTests.Abstractions
{
    public class AbstractTestWithDb
    {
        private ApplicationContext _context;
        private readonly Mapper _mapper;

        protected AbstractTestWithDb()
        {
            var mapperCfg = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ToyMapperProfile>();
                cfg.AddProfile<OrderMapperProfile>();
                cfg.AddProfile<PartitionMapperProfile>();
                cfg.AddProfile<CategoryMapperProfile>();
            });
            _mapper = new Mapper(mapperCfg);
        }

        protected ApplicationContext CreateContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(dbName)
                .Options;
            _context = new ApplicationContext(options);
            return _context;
        }

        private static IHttpContextAccessor HttpContextAccessor
        {
            get
            {
                var mock = new Mock<IHttpContextAccessor>();
                mock.Setup(x => x.HttpContext.RequestAborted).Returns(CancellationToken.None);
                return mock.Object;
            }
        }

        private ISaveProvider SaveProvider => new SaveProvider(_context, HttpContextAccessor);

        protected async Task PopulateAsync<TITem>(IEnumerable<TITem> initialItems)
        {
            var iItems = initialItems as TITem[] ?? initialItems.ToArray();

            foreach (var item in iItems)
                await _context.AddAsync(item);

            await _context.SaveChangesAsync();

            foreach (var item in iItems)
                _context.Entry(item).State = EntityState.Detached;
        }

        protected void DetachOrderToys()
        {
            foreach (var entry in _context.ChangeTracker.Entries<OrderToy>().ToList())
                _context.Entry(entry.Entity).State = EntityState.Detached;
        }

        protected (IToyManager, FakePictureManager) CreateToyManager()
        {
            var pictureManager = new FakePictureManager();
            var toyManager = new ToyManager(new ToyRepository(_context, HttpContextAccessor), pictureManager,
                new ToyImageRepository(_context, HttpContextAccessor), SaveProvider,
                _mapper);
            return (toyManager, pictureManager);
        }

        protected IOrderManager CreateOrderManager()
        {
            var options = new Mock<IOptions<DeliveryConfig>>();
            options.SetupGet(x => x.Value).Returns(new DeliveryConfig {CourierPrice = 5});
            return new OrderManager(new OrderRepository(_context, HttpContextAccessor), options.Object, SaveProvider,
                _mapper);
        }

        protected IPartitionManager CreatePartitionManager() =>
            new PartitionManager(new PartitionRepository(_context, HttpContextAccessor), SaveProvider, _mapper);

        protected ICategoryManager CreateCategoryManager() =>
            new CategoryManager(new CategoryRepository(_context, HttpContextAccessor), SaveProvider, _mapper);
    }
}
