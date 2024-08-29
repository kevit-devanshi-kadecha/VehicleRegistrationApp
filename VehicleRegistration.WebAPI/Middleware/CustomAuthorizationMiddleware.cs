﻿using Azure;
using System.Security.Claims;

namespace VehicleRegistration.WebAPI.Middleware
{
    public class CustomAuthorizationMiddleware 
    {
        private readonly RequestDelegate _next;

        public CustomAuthorizationMiddleware(RequestDelegate requestDelegate)
        {
            _next = requestDelegate;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var controller = context.Request.RouteValues["controller"]?.ToString();
            if (controller.Equals("Account"))
            {
                await _next(context);
                return;
            }
            try
            {
                var user = context.User;

                if (user.Identity?.IsAuthenticated != true)
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("Unauthorized");
                    return;
                }
                if (user.Identity?.IsAuthenticated == true)
                {
                    context.Response.StatusCode = StatusCodes.Status200OK;
                    await _next(context);
                    return;
                }

                // Extracting claims
                var userIdClaim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(userIdClaim) || userIdClaim != "ExpectedUserId")
                {
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    await context.Response.WriteAsync("Forbidden");
                    return;
                }

                await _next(context);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsync("Internal Server error");
            }
        }
    }
}

