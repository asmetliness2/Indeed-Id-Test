using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using PurseApi.Controllers.Filters;
using PurseApi.Models;
using PurseApi.Repository;
using PurseApi.Repository.Interfaces;
using PurseApi.Services;
using PurseApi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace PurseApi
{
    public static class ServiceExtensions
    {

        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        public static void ConfigureInterfaces(this IServiceCollection services)
        {

            services.AddTransient<IPurseRepository, PurseRepository>();
            services.AddTransient<IPurseService, PurseService>();
        }

        public static void ConfigureFilters(this IServiceCollection services)
        {
            services.AddControllers(opt =>
            {
                opt.Filters.Add(typeof(ExceptionFilter));
                opt.Filters.Add(typeof(ResultFilter));
                opt.Filters.Add(typeof(LoggingFilter));
            });
        }
        public static void ConfigureInvalidModelFactory(this IServiceCollection services)
        {
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
    }
}
