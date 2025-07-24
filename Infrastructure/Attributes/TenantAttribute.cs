using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ChatApi.Infrastructure.Services;

namespace ChatApi.Infrastructure.Attributes
{
    public class TenantAttribute : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var tenantId = context.HttpContext.Request.Headers["X-Tenant-ID"].FirstOrDefault();

            if (string.IsNullOrEmpty(tenantId))
            {
                context.Result = new BadRequestObjectResult("Tenant header missing");
                return;
            }

            // var tenantService = context.HttpContext.RequestServices.GetService<TenantService>();
            // await tenantService.SetTenantAsync(tenantId); 

            await next();
        }
    }
}