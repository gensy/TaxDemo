using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using TaxDemo.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Globalization;

namespace TaxDemo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddSingleton<ITaxApiService, TaxApiService>();

            //Using secret.json
            services.AddHttpClient("PublicTaxApi", c =>
            {
                c.BaseAddress = new Uri(Configuration["KeyVault:TaxjarApiBase"]);
                c.DefaultRequestHeaders.Authorization =  new AuthenticationHeaderValue("Token", $"token=\"{Configuration["KeyVault:TaxjarApiKey"]}\"");
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
           
            app.UseExceptionHandler("/Home/Error");

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}",
                    defaults: new { controller = "Home", action = "Index" });

                endpoints.MapControllerRoute(
                    name: "GetOrderRates",
                    pattern: "{controller=Home}/{action=GetOrderRates}",
                    defaults: new { controller = "Home", action = "GetOrderRates" });
            });
        }
    }
}
