using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using YT.Challenge.Api.Util;
using YT.Challenge.Auth.i18n;
using YT.Challenge.Domain.Models;

namespace YT.Challenge.Api.Middlewares
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILogger logger, IMessageRepo messageRepo)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        logger.LogError($"Something went wrong: {contextFeature.Error}");
                        Error err = new Error(contextFeature.Error.Message);
                        TypedResult<Error> errorBody = new TypedResult<Error>(false, messageRepo.Get(MessageKey.INTERNAL_SERVER_ERROR), err);

                        await context.Response.WriteAsync(errorBody.ToJsonString());
                    }
                });
            });
        }
    }
}