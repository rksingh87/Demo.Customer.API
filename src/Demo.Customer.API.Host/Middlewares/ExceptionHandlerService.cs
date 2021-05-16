using Demo.Customer.API.Core.Entities.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Demo.Customer.API.Host.Middlewares
{
    public static class ExceptionHandlerService
    {
        /// <summary>
        /// Configure Exception Handler
        /// </summary>
        /// <param name="app"></param>
        public static void UseExceptionHandlerService(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

                    if (contextFeature != null)
                    {

                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                        await context.Response.WriteAsync(JsonConvert.SerializeObject( new ErrorResult
                        {
                            ErrorCode = context.Response.StatusCode.ToString(),
                            ErrorMessage = contextFeature.Error.Message
                        }));
                    }
                });
            });
        }
    }
}
