using Microsoft.AspNetCore.Http;

namespace Rgs.Dms.Api.Infrastructure.Middlewares
{
    internal class AddCorrelationIdToResponseMiddleware
    {
        private const string CorrelationIdHeaderName = "X-Correlation-ID";
        private readonly RequestDelegate _next;

        public AddCorrelationIdToResponseMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {

            httpContext
                .Response
                .Headers
                .Add(CorrelationIdHeaderName, httpContext.TraceIdentifier);

            await _next(httpContext);
        }
    }
}
