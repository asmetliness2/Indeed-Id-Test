using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PurseApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Logging;

namespace PurseApi.Controllers.Filters
{
    public class ExceptionFilter : Attribute, IAsyncExceptionFilter
    {
        private readonly ILogger<ExceptionFilter> _logger;

        public ExceptionFilter(ILogger<ExceptionFilter> logger)
        {
            _logger = logger;
        }

        public Task OnExceptionAsync(ExceptionContext context)
        {

            if(context.Exception.InnerException != null)
            {
                _logger.LogError(context.Exception.InnerException, context.Exception.InnerException.StackTrace);
            }

            _logger.LogError(context.Exception, context.Exception.StackTrace);

            Result<Object> result = new Result<object>()
                .SetServerError("Sorry, something went wrong, please try again later");

            string response = JsonSerializer.Serialize(result);
            if (!context.HttpContext.Response.HasStarted)
            {
                context.HttpContext.Response.StatusCode = 500;
                context.Result = new ContentResult
                {
                    Content = response
                };
            }

            context.ExceptionHandled = true;
            return Task.CompletedTask;
        }
    }
}

