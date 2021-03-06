﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Routing;

namespace WebApiUtils
{
    // NB. Fix documentation (intro.md) if you change this class.
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
                var details = new ValidationProblemDetails(ve.Errors)
                {
                    Type = "urn:acme-corp:validation-error"
                };
                await WriteError(context, details);
            }
        }

        private Task WriteError(HttpContext context, object error)
        {
            RouteData routeData = context.GetRouteData() ?? EmptyRouteData;
            var actionContext = new ActionContext(context, routeData, EmptyActionDescriptor);
            var result = new ObjectResult(error)
            {
                StatusCode = StatusCodes.Status400BadRequest
            };
            return result.ExecuteResultAsync(actionContext);
        }
    }
}
