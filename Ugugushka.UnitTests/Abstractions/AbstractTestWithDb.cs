using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moq;
using Ugugushka.Data;
using Ugugushka.Data.Code.Interfaces;
using Ugugushka.Data.Models;
using Ugugushka.Data.Repositories;
using Ugugushka.Domain.Code.Interfaces;
using Ugugushka.Domain.Code.MapperProfiles;
using Ugugushka.Domain.Managers;
using Ugugushka.UnitTests.FakeConcretes;

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

        private async Task<IToyRepository> CreateToyRepositoryAsync(IEnumerable<Toy> initialValues)
        {
            var toys = initialValues.ToArray();
            await _context.AddRangeAsync(toys);
            await _context.SaveChangesAsync();
            
            foreach (var toy in toys)
                _context.Entry(toy).State = EntityState.Detached;

            return new ToyRepository(_context, HttpContextAccessor);
        }

        private IToyImageRepository CreateToyImageRepository() => 
            new ToyImageRepository(_context, HttpContextAccessor);

        protected async Task<(IToyManager, FakePictureManager)> CreateToyManagerAsync(IEnumerable<Toy> initialValues = null)
        {
            var pictureManager = new FakePictureManager();
            var toyManager = new ToyManager(await CreateToyRepositoryAsync(initialValues),pictureManager, CreateToyImageRepository(), SaveProvider,
                _mapper);
            return (toyManager, pictureManager);
        }
    }
}
