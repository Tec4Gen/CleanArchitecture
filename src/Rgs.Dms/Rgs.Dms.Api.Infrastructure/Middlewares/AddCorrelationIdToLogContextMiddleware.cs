using Microsoft.AspNetCore.Http;

using Serilog.Context;

namespace Rgs.Dms.Api.Infrastructure.Middlewares
{
    internal class AddCorrelationIdToLogContextMiddleware
    {
        private readonly RequestDelegate _next;

        public AddCorrelationIdToLogContextMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            using (LogContext.PushProperty("CorrelationId", context.TraceIdentifier))
            {
                await _next(context);
            }
        }
    }
}
