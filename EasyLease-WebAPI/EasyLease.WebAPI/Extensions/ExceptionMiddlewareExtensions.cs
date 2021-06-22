using System.Net;
using EasyLease.Contracts;
using EasyLease.Entities.ErrorModel;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;

namespace EasyLease.WebAPI.Extensions {
    public static class ExceptionMiddlewareExtensions {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILoggerManager logger) {
            app.UseExceptionHandler(appError => {
                appError.Run(async context => {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

                    if (contextFeature != null) {
                        logger.LogError($"Something went wrong: {contextFeature.Error}");

                        var errDetails = new ErrorDetails() {
                            StatusCode = context.Response.StatusCode,
                            Message = "Internal Server Error."
                        }.ToUtf8Bytes();

                        await context.Response.Body.WriteAsync(errDetails, 0, errDetails.Length).ConfigureAwait(false);
                    }
                });
            });
        }
    }
}