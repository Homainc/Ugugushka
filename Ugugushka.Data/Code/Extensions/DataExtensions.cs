using System;
using System.IO;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ugugushka.Data.Code.Interfaces;
using Ugugushka.Data.Models;
using Ugugushka.Data.Repositories;

namespace Ugugushka.Data.Code.Extensions
{
    public static class DataExtensions
    {
        private const string ConfigFileName = "appsettings.json";

        private static string ConnectionString
        {
            get
            {
                var cfg = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile(ConfigFileName)
                    .Build();
                return cfg.GetConnectionString("DefaultConnection");
            }
        }

        public static IServiceCollection AddDataServices(this IServiceCollection services)
        {
            // EF Core Context
            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(ConnectionString));

            // Data Protection
            services.AddDataProtection()
                .SetApplicationName("ugugushka")
                .SetDefaultKeyLifetime(TimeSpan.FromDays(14))
                .PersistKeysToDbContext<ApplicationContext>();

            // Identity
            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationContext>()
                .AddDefaultTokenProviders();

            // Repositories
            services.AddScoped<IToyRepository, ToyRepository>();
            services.AddScoped<IToyImageRepository, ToyImageRepository>();
            services.AddScoped<IOrderToyRepository, OrderToyRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IPartitionRepository, PartitionRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();            
            // Save Provider
            services.AddScoped<ISaveProvider, SaveProvider>();

            // Other Dependencies
            services.AddHttpContextAccessor();

            return services;
        }
    }
}
