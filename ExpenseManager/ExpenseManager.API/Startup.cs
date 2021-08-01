using Autofac;
using ExcelHandler.Providers;
using ExcelHandler.Service;
using ExpenseManager.Data.Context;
using ExpenseManager.Service.Setting;
using Lookif.Layers.WebFramework.Configuration;
using Lookif.Layers.WebFramework.CustomMapping;
using Lookif.Layers.WebFramework.Middlewares;
using Lookif.Layers.WebFramework.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseManager.API
{
    public class Startup
    {
        private readonly ExpenseManagerSettings _siteSetting;
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            _siteSetting = configuration.GetSection(nameof(ExpenseManagerSettings)).Get<ExpenseManagerSettings>();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<ExpenseManagerSettings>(Configuration.GetSection(nameof(ExpenseManagerSettings)));

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.AllowAnyOrigin();
                    });
            });

            services.InitializeAutoMapper();

            services.AddDbContext<ExpenseManagerDbContext>(Configuration);


            //Add capability to use excel
            services.AddExcelHandler(ExcelProviders.EPPlus);


            services.AddCustomIdentity(_siteSetting.IdentitySettings);

            services.AddMinimalMvc();

            // services.AddElmahCore(Configuration, _siteSetting);

            services.AddJwtAuthentication(_siteSetting.JwtSettings);

            services.AddCustomApiVersioning();

            services.AddSwagger();

            services.AddSingleton<PersianCalendar>();


        }


        public void ConfigureContainer(ContainerBuilder builder)
        {
            //Register Services to Autofac ContainerBuilder
            builder.AddServices();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(config => config.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

            app.IntializeDatabase<ExpenseManagerDbContext>();

            app.UseCustomExceptionHandler();

            app.UseHsts(env);

            //app.UseHttpsRedirection();

            // app.UseElmahCore(_siteSetting);

            app.UseSwaggerAndUI();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCustomHeaders();
            //Use this config just in Develoment (not in Production)

            app.UseEndpoints(config =>
            {
                config.MapControllers(); // Map attribute routing
                //    .RequireAuthorization(); Apply AuthorizeFilter as global filter to all endpoints
                //config.MapDefaultControllerRoute(); // Map default route {controller=Home}/{action=Index}/{id?}
            });

            //Using 'UseMvc' to configure MVC is not supported while using Endpoint Routing.
            //To continue using 'UseMvc', please set 'MvcOptions.EnableEndpointRouting = false' inside 'ConfigureServices'.
            //app.UseMvc();
        }
    }
}
