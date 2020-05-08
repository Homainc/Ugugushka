using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ugugushka.Data.Code.Interfaces;
using Ugugushka.Data.Repositories;

namespace Ugugushka.Data.Code.Extensions
{
    public static class DataExtensions
    {
        private const string ConfigFileName = "appsettings.json";
        private static string GetConnectionString()
        {
            var cfg = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(ConfigFileName)
                .Build();
            return cfg.GetConnectionString("DefaultConnection");
        }
        public static IServiceCollection AddDataServices(this IServiceCollection services)
        {
            //EF Core Context
            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(GetConnectionString()));

            //Repositories
            services.AddScoped<IToyRepository, ToyRepository>();
            
            //Save Provider
            services.AddScoped<ISaveProvider, SaveProvider>();

            //Other Dependencies
            services.AddHttpContextAccessor();

            return services;
        }
    }
}
