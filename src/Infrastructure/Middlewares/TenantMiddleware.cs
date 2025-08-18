using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Linq;
using ChatApi.Infrastructure.Services;

namespace ChatApi.Infrastructure.Middlewares
{
    public class TenantMiddleware
    {
        private readonly RequestDelegate _next;

        public TenantMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, TenantService tenantService)
        {
            var tenantId = context.Request.Headers["X-Tenant-ID"].FirstOrDefault();

            if (string.IsNullOrEmpty(tenantId))
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("Tenant header missing");
                return;
            }

            //await tenantService.SetTenantAsync(tenantId);
            await _next(context);
        }
    }
}
