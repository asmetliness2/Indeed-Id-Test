using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using NLog.Fluent;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text;

namespace PurseApi.Controllers.Filters
{
    public class LoggingFilter : Attribute, IActionFilter
    {
        private readonly ILogger<LoggingFilter> _logger;

        public LoggingFilter(ILogger<LoggingFilter> logger)
        {
            _logger = logger;
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            var builder = new StringBuilder(100);

            builder.Append($"{context.HttpContext.TraceIdentifier} RESPONE {context.HttpContext.Request.Method} {context.HttpContext.Request.Path} result: ");
            if (context.Result is ObjectResult obj)
            {
                builder.Append(JsonSerializer.Serialize(obj.Value));
            }

            _logger.LogInformation(builder.ToString());
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var builder = new StringBuilder(100);

            builder.Append($"{context.HttpContext.TraceIdentifier} REQUEST {context.HttpContext.Request.Method} {context.HttpContext.Request.Path}, data: ");
            context.ActionArguments
                .Select(elem => builder.Append($"({elem.Key}: {JsonSerializer.Serialize(elem.Value)}) "))
                .ToList();

            _logger.LogInformation(builder.ToString());


        }
    }
}
