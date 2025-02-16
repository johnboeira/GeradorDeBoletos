using GeradorDeBoletos.Domain.Features.Shared;
using System.Net;
using System.Text.Json;

namespace GeradorDeBoletos.Web.API.Extensions;

public class GlobalErrorHandlingMiddleware
{
    private readonly RequestDelegate _requestDelegate;

    public GlobalErrorHandlingMiddleware(RequestDelegate requestDelegate)
    {
        _requestDelegate = requestDelegate;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _requestDelegate(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
    {
        HttpStatusCode status;
        string stackTrace;
        string message;
        var exceptionType = ex.GetType();

        if (exceptionType == typeof(NotFoundException))
        {
            message = ex.Message;
            status = HttpStatusCode.NotFound;
            stackTrace = ex.StackTrace;
        }
        else
        {
            message = ex.Message;
            status = HttpStatusCode.InternalServerError;
            stackTrace = ex.StackTrace;
        }

        var result = JsonSerializer.Serialize(new { status, message, stackTrace });
        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = (int)status;

        return httpContext.Response.WriteAsync(result);
    }
}