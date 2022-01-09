using Book_store.DTOs.Commons;
using Book_store.Services.Loggers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace Book_store.Extensions
{
    public static class ExceptionMiddliwareExtension
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILoggerManager logger)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        logger.LogError($"Something went wrong: {contextFeature.Error}");
                        await context.Response.WriteAsync(
                            new APIErrorResponse<string>(
                                contextFeature.Error.Message,
                                HttpStatusCode.InternalServerError
                            ).ToString());
                    }
                });
            });
        }
    }
}
