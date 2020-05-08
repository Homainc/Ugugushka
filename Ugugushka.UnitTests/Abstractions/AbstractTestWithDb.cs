using System.Collections.Generic;
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

        protected string DbName { get; set; }

        protected ApplicationContext Context
        {
            set => _context = value;
        }

        protected ApplicationContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(DbName)
                .Options;
            return new ApplicationContext(options);
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
            await _context.AddRangeAsync(initialValues);
            await _context.SaveChangesAsync();
            return new ToyRepository(_context, HttpContextAccessor);
        }

        protected async Task<IToyManager> CreateToyManagerAsync(IEnumerable<Toy> initialValues = null) =>
            new ToyManager(await CreateToyRepositoryAsync(initialValues), SaveProvider, _mapper);
    }
}
