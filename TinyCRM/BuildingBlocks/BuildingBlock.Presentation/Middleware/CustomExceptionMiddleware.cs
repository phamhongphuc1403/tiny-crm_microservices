using System.Net;
using System.Text.Json;
using BuildingBlock.Domain.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
                var response = context.Response;

                var isDevelopment = env.IsDevelopment();

                var ex = context.Features.Get<IExceptionHandlerFeature>()?.Error;

                var message = ex is ValidationException ? "Validation error" : ex?.Message;

                var statusCode = GetStatusCode(ex);

                response.StatusCode = statusCode;
                response.ContentType = "application/json";

                var pd = new ProblemDetails
                {
                    Title = isDevelopment ? message : "An error occurred on the server.",
                    Status = statusCode,
                    Detail = isDevelopment ? ex?.StackTrace : null
                };

                if (ex is ValidationException validationException)
                    pd.Extensions.Add("errors",
                        validationException.Errors.Select(x => new { x.PropertyName, x.ErrorMessage }));

                pd.Extensions.Add("traceId", context.TraceIdentifier);

                await context.Response.WriteAsync(JsonSerializer.Serialize(pd));
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
            EntityDuplicatedException => (int)HttpStatusCode.Conflict,
            ValidationException => (int)HttpStatusCode.BadRequest,
            _ => (int)HttpStatusCode.InternalServerError
        };

        return statusCode;
    }
}