using Microsoft.AspNetCore.Builder;

namespace Rgs.Dms.Api.Infrastructure.Middlewares.Extentions
{
    public static class DmsMiddlewareExtensions
    {
        public static IApplicationBuilder AddCorrelationIdToLogContextMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<AddCorrelationIdToLogContextMiddleware>();
        }

        public static IApplicationBuilder AddCorrelationIdToResponseMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<AddCorrelationIdToResponseMiddleware>();
        }
    }
}
