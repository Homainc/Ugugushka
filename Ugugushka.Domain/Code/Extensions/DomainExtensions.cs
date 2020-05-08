using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Ugugushka.Data.Code.Extensions;
using Ugugushka.Domain.Code.Interfaces;
using Ugugushka.Domain.Code.MapperProfiles;
using Ugugushka.Domain.Managers;

namespace Ugugushka.Domain.Code.Extensions
{
    public static class DomainExtensions
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            //Data services
            services.AddDataServices();

            //Managers
            services.AddScoped<IToyManager, ToyManager>();

            return services;
        }
    }
}
