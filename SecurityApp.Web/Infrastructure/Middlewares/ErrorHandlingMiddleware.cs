using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using SecurityApp.Web.Infrastructure.Entities.Exceptions;
using System;
using System.Net;
using System.Threading.Tasks;

namespace SecurityApp.Web.Infrastructure.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;
            
            if (exception is UnauthorizedException) code = HttpStatusCode.Unauthorized;
            else if (exception is BadRequestException) code = HttpStatusCode.BadRequest;
            else if (exception is SecurityTokenValidationException) code = HttpStatusCode.Unauthorized;
            else if (exception is UnauthorizedAccessException) code = HttpStatusCode.Unauthorized;

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            return context.Response.WriteAsync(JsonConvert.SerializeObject(new
            {
                statusCode = (int)code,
                error = exception.Message
            }));
        }
    }
}
