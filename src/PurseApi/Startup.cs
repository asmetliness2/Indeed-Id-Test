using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PurseApi.Controllers.Filters;
using PurseApi.Models;
using PurseApi.Providers;
using PurseApi.Providers.Interfaces;
using PurseApi.Repository;
using PurseApi.Repository.Context;
using PurseApi.Repository.Interfaces;
using PurseApi.Services;
using PurseApi.Services.Interfaces;

namespace PurseApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddMemoryCache();

            services.AddHttpClient<ICurrencyProvider, CentralBankCurrencyProvider>(client =>
            {
                client.BaseAddress = new Uri(Configuration["CurrencyProvider:BaseUrl"]);
            });

            services.AddTransient<IPurseRepository, PurseRepository>();
            services.AddTransient<IPurseService, PurseService>();

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetSection("ConnectionStrings")["DefaultConnection"]);
                options.EnableDetailedErrors(true);
            });


            services.AddControllers(opt =>
            {
                opt.Filters.Add(typeof(ExceptionFilter));
                opt.Filters.Add(typeof(ResultFilter));
                opt.Filters.Add(typeof(LoggingFilter));
            });

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (context) =>
                {
                    Result result = new Result();

                    result.SetErrorCodeAndMessage(422, "Incorrect data");

                    var keys = context.ModelState.Keys;
                    
                    foreach (var key in keys)
                    {
                        if (context.ModelState[key].Errors.Any())
                        {
                            result.SetValidationError(key, context.ModelState[key].Errors.First().ErrorMessage);
                        }    
                    }
                    return new UnprocessableEntityObjectResult(result);

                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
