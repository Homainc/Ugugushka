using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ugugushka.Domain.Code.Extensions;
using Ugugushka.Domain.Managers;
using Ugugushka.WebUI.Code.Binders;

namespace Ugugushka.WebUI
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDomainServices();

            services.AddAutoMapper(typeof(Startup), typeof(ToyManager));

            services.AddControllersWithViews(options =>
                {
                    options.ModelBinderProviders.Insert(0, new CartModelBinderProvider());
                })
                .AddSessionStateTempDataProvider();
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }

            app.UseRouting();
            app.UseStaticFiles();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: null,
                    pattern: "Page{page:int}",
                    defaults: new { Controller="Home", Action="Index", page = 1 });
                endpoints.MapControllerRoute(
                    name: null,
                    pattern: "Toys/Page{page:int}",
                    defaults: new { Controller="Toy", Action="Toys", page = 1 });

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action}/{id:int?}",
                    defaults: new { Controller = "Home", Action = "Index" });
            });
        }
    }
}
