using System.Net;
using BuildingBlock.Domain.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace BuildingBlock.Presentation.Middleware;

public static class CustomExceptionMiddleware
{
    public static void UseCustomerExceptionHandler(this IApplicationBuilder app, IHostEnvironment env)
    {
        app.UseExceptionHandler(exceptionHandlerApp =>
        {
            exceptionHandlerApp.Run(Handler);
            return;

            async Task Handler(HttpContext context)
            {
                var ex = context.Features.Get<IExceptionHandlerFeature>()?.Error;
                var message = ex?.Message;
                // LoggerService.LogError(ex, "An exception occurred while processing the request");

                var statusCode = GetStatusCode(ex);
                if(!env.IsDevelopment() && statusCode==(int)HttpStatusCode.InternalServerError)
                    message = "An error occurred from the system. Please try again";
                context.Response.StatusCode = statusCode;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsJsonAsync(new ExceptionResponse
                    { StatusCode = statusCode, Message = message });
            }
        });
    }

    private static int GetStatusCode(Exception? ex)
    {
        var statusCode = ex switch
        {
            EntityNotFoundException => (int)HttpStatusCode.NotFound,
            NotImplementedException => (int)HttpStatusCode.NotImplemented,
            EntityValidationException => (int)HttpStatusCode.BadRequest,
            InvalidUpdateException => (int)HttpStatusCode.BadRequest,
            InvalidPasswordException => (int)HttpStatusCode.BadRequest,
            AuthorizationPolicyException => (int)HttpStatusCode.Forbidden,
            _ => (int)HttpStatusCode.InternalServerError
        };

        return statusCode;
    }
}