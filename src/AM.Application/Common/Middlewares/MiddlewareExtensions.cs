using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Builder;

namespace AM.Application.Common.Middlewares;

[ExcludeFromCodeCoverage]
public static class MiddlewareExtensions
{
    public static void ConfigureExceptionMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionMiddleware>();
    }
}