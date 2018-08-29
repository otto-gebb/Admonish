using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Routing;

namespace WebApiUtils
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private static readonly RouteData EmptyRouteData = new RouteData();
        private static readonly ActionDescriptor EmptyActionDescriptor = new ActionDescriptor();

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (CustomValidationException ve)
            {
                await WriteError(context, ve.Errors);
            }
        }

        private Task WriteError(HttpContext context, object error)
        {
            RouteData routeData = context.GetRouteData() ?? EmptyRouteData;
            var actionContext = new ActionContext(context, routeData, EmptyActionDescriptor);
            var result = new ObjectResult(error)
            {
                StatusCode = (int)HttpStatusCode.BadRequest
            };
            return result.ExecuteResultAsync(actionContext);
        }
    }
}
