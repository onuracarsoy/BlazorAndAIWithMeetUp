using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace MeetUpWebApp.Shared.Endpoints
{
    public class AuthorizationPolicies
    {
        //Program.cs
        public static void AddCustomPolices(AuthorizationOptions options)
        {
            options.AddPolicy("SameUserPolicy", policy =>
            {
                policy.RequireAssertion(context =>
                {
                    var user = context.User;
                    var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                    if (int.TryParse(userIdClaim, out var auhenticatedUserId))
                    {
                        var routeData = context.Resource as HttpContext;
                        if (routeData is not null)
                        {
                            var routeUserId = routeData.Request.RouteValues["userId"]?.ToString();
                            if (int.TryParse(routeUserId, out var userId))
                            {
                                return auhenticatedUserId == userId;
                            }
                        }
                    }
                    return false;
                });

            });
        }
    }
}
