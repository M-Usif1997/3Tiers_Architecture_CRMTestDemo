using Microsoft.AspNetCore.Diagnostics;
using static System.Net.Mime.MediaTypeNames;
using System.Net.Sockets;
using System.Net;
using System.Text.Json;
using System;
using BusinessLogicLayer.Exceptions;


namespace WebApi.Extension
{
    public static class ErrorHandlerExtensions
    {
        public static void UseErrorHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature == null) return;

                    context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                    context.Response.ContentType = "application/json";

                    context.Response.StatusCode = contextFeature.Error switch
                    {
                        BadRequestException => (int)HttpStatusCode.BadRequest,
                        OperationCanceledException => (int)HttpStatusCode.ServiceUnavailable,
                        NoDataFoundException => (int)HttpStatusCode.NotFound,                   
                        EntityNotFoundException => (int)HttpStatusCode.NotFound,
                        ErrorMappingException => (int)HttpStatusCode.InternalServerError,
                        SaveChangesFailedException => (int)HttpStatusCode.BadRequest,
                        UnauthenticatedException => (int)HttpStatusCode.Forbidden,
                        BusinessLogicLayer.Exceptions.UnauthorizedAccessException => (int)HttpStatusCode.Unauthorized,
                        _ => (int)HttpStatusCode.InternalServerError
                    };

                    var errorResponse = new
                    {
                        statusCode = context.Response.StatusCode,
                        message = contextFeature.Error.GetBaseException().Message,
                        Errors = contextFeature.Error switch
                        {
                            BadRequestException badRequestError => badRequestError.Errors,
                            _ => null
                        }

                    };

                    await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
                });
            });
        }
    }
}
