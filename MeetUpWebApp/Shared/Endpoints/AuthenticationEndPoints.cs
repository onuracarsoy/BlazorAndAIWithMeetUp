using MeetUpWebApp.Data;
using MeetUpWebApp.Data.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Security.Claims;

namespace MeetUpWebApp.Shared.Endpoints
{
    public static class AuthenticationEndPoints
    {
        public static void MapAuthenticationEndpoints(this WebApplication app)
        {
            // Attendee authentication
            app.MapGet("/authentication/{provider}",
                async (string provider, HttpContext context) =>
                {
                    await context.ChallengeAsync(provider);
                });


            // Organizer authentication
            app.MapGet("/authentication/{provider}/organizer",
                async (string provider, HttpContext context) =>
                {
                    await context.ChallengeAsync(provider);
                });

            app.MapGet("/sigin-callback/organizer", async (HttpContext context, IDbContextFactory<ApplicationDbContext> contextFactory) =>
            {
                await HandleSignInCallback(context, contextFactory, isOrganizer: true);
            });

            app.MapGet("/signout", async (HttpContext context) =>
            {
                await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                context.Response.Redirect("/");
            });
        }

        public static async Task HandleSignInCallback(
            HttpContext context,
            IDbContextFactory<ApplicationDbContext> contextFactory,
            bool isOrganizer = false,
            ClaimsPrincipal? principal = null,
            string? redirectUri = null)
        {
            principal ??= context.User;

            // Create the user record in the database
            var emailClaim = principal?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);
            var nameClaim = principal?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name);

            if (emailClaim is not null && nameClaim is not null)
            {
                using var dbContext = contextFactory.CreateDbContext();

                // Check if the user already exists
                var existingUser = await dbContext.Users?.FirstOrDefaultAsync(u => u.Email == emailClaim.Value);

                if (existingUser is null)
                {
                    // User does not exist, create a new user
                    existingUser = new User
                    {
                        Name = nameClaim.Value,
                        Email = emailClaim.Value,
                        Role = isOrganizer ? SharedHelper.OrganizerRole : SharedHelper.AttendeeRole
                    };
                    dbContext.Users?.Add(existingUser);
                    await dbContext.SaveChangesAsync();
                }
                else
                {
                    // User already exists, update the role if necessary                        
                    existingUser.Name = nameClaim.Value;

                    if (isOrganizer && existingUser.Role != SharedHelper.OrganizerRole)
                    {
                        existingUser.Role = SharedHelper.OrganizerRole;
                    }

                    await dbContext.SaveChangesAsync();
                }

                var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, existingUser.UserId.ToString()),
                        new Claim(ClaimTypes.Name, existingUser.Name),
                        new Claim(ClaimTypes.Email, existingUser.Email),
                        new Claim(ClaimTypes.Role, existingUser.Role)
                    };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                // Sign in the user to recreate the authentication cookie
                await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

                if (!string.IsNullOrWhiteSpace(redirectUri) && !redirectUri.Contains("google", StringComparison.OrdinalIgnoreCase))
                {
                    context.Response.Redirect(redirectUri);
                }
                else
                {
                    context.Response.Redirect("/");
                }
            }
        }
    }
}
