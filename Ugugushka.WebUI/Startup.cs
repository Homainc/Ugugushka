using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Ugugushka.Domain.Code.Config;
using Ugugushka.Domain.Code.Extensions;
using Ugugushka.Domain.Code.Interfaces;
using Ugugushka.Domain.Managers;
using Ugugushka.WebUI.Code.Binders;
using Ugugushka.WebUI.Code.Filters;

namespace Ugugushka.WebUI
{
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup()
        {
            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
            _config = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Application configuration
            services.Configure<CredentialsConfig>(_config.GetSection("Credentials"));

            services.AddDomainServices();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(opt =>
                {
                    opt.LoginPath = new PathString("Account/Login");
                    opt.LogoutPath = new PathString("Account/Logout");
                });
            services.AddAuthorization();

            services.AddAutoMapper(typeof(Startup), typeof(ToyManager));

            services.AddControllersWithViews(options =>
                {
                    // Global filters
                    options.Filters.Add<GlobalExceptionFilter>();

                    // Custom model binder providers
                    options.ModelBinderProviders.Insert(0, new CartModelBinderProvider());
                })
                .AddSessionStateTempDataProvider();
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ISeedManager seedManager)
        {

            if (env.IsDevelopment())
            {
                Log.Information("Application has been started in the development mode");

                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                Log.Information("Application has been stared in the release mode");

                app.UseExceptionHandler("/Home/Error");
            }

            seedManager.SeedData();

            app.UseStaticFiles();

            app.UseSerilogRequestLogging();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: null,
                    pattern: "Page{page:int}",
                    defaults: new {Controller = "Home", Action = "Index", page = 1});

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action}/{id:int?}",
                    defaults: new {Controller = "Home", Action = "Index"});
            });
        }
    }
}
