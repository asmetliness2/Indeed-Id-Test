using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PurseApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PurseApi.Controllers.Filters
{
    public class ResultFilter : Attribute, IAsyncResultFilter
    {
        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            var result = context.Result as ObjectResult;
            if (result != null)
            {
                var resultObj = result.Value as Result;
                if (resultObj != null)
                {
                    if(!resultObj.Success)
                    {
                        if (!context.HttpContext.Response.HasStarted)
                        {
                            result.StatusCode = resultObj.Error.Code;
                        }
                    }
                }
            }
            await next();
        }
    }
}
