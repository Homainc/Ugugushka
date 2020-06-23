using System.Collections.Generic;
using System.Globalization;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
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
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json");
            _config = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Application configuration
            services.Configure<CredentialsConfig>(_config.GetSection("Credentials"));
            services.Configure<DeliveryConfig>(_config.GetSection("Delivery"));

            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddScoped<IUrlHelper>(x =>
            {
                var actionContext = x.GetRequiredService<IActionContextAccessor>().ActionContext;
                var factory = x.GetRequiredService<IUrlHelperFactory>();
                return factory.GetUrlHelper(actionContext);
            });

            // Adding domain services
            services.AddDomainServices();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(opt =>
                {
                    opt.LoginPath = new PathString("Account/Login");
                    opt.LogoutPath = new PathString("Account/Logout");
                    opt.Cookie.IsEssential = true;
                    opt.Cookie.HttpOnly = true;
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
            // Setting default app culture
            var defaultCulture = new CultureInfo("ru-RU")
            {
                NumberFormat = {NumberDecimalSeparator = ".", CurrencyDecimalSeparator = "."}
            };
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(defaultCulture),
                SupportedCultures = new List<CultureInfo>
                {
                    defaultCulture
                },
                SupportedUICultures = new List<CultureInfo>
                {
                    defaultCulture
                }
            });

            if (env.IsDevelopment())
            {
                Log.Information("Application has been started in the development mode");

                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                Log.Information("Application has been stared in the release mode");

                app.UseHsts();
                app.UseExceptionHandler("/error500");
            }

            app.UseStatusCodePagesWithReExecute("/error{0}");

            seedManager.SeedData();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseSerilogRequestLogging();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "error",
                    pattern: "error{code:int}",
                    defaults: new {controller = "Home", action = "Error", code = 500});

                endpoints.MapControllerRoute(
                    name: "sitemap",
                    pattern: "sitemap.xml",
                    defaults: new {controller = "Home", action = "SiteMap"});

                endpoints.MapControllerRoute(
                    name: "toyPage",
                    pattern: "Page{page:int}",
                    defaults: new {controller = "Home", action = "Index", page = 1});

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action}/{id?}",
                    defaults: new {controller = "Home", action = "Index"});
            });
        }
    }
}
