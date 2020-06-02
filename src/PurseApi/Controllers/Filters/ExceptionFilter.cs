using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PurseApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PurseApi.Controllers.Filters
{
    public class ExceptionFilter : Attribute, IAsyncExceptionFilter
    {
        public Task OnExceptionAsync(ExceptionContext context)
        {

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

