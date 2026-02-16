using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;

namespace EventTicketSystem.Middleware;

public class GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            //Continues the request
            await next(context);
        }
        catch (Exception ex)
        {
            //Catches the exception and logs it
            logger.LogError(ex, "Unhandled exception occurred.");
            
            //awaits the HandleExceptionAsync method
            await HandleExceptionAsync(context, ex);
        }

    }


    public static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        //Defines response header
        context.Response.ContentType = "application/problem+json";
        ProblemDetails problemDetails;

        //Checks if the exception is a DomainException(custom exception)
        if (exception is DomainException domainException)
        {
            context.Response.StatusCode = domainException.StatusCode;

            problemDetails = new ProblemDetails()
            {
                Title = ReasonPhrases.GetReasonPhrase(domainException.StatusCode),
                Status = domainException.StatusCode,
                Detail = domainException.Message,
                Instance = context.Request.Path
            };
        }
        else
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            problemDetails = new ProblemDetails
            {
                Status = 500,
                Title = "Internal Server Error",
                Detail = "An unexpected error occurred.",
                Instance = context.Request.Path
            };
        }
        await context.Response.WriteAsJsonAsync(problemDetails);
    }
    
}