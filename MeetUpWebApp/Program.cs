using MeetUpWebApp.Data;
using MeetUpWebApp.Features.AIChatBot;
using MeetUpWebApp.Features.CreateEvent;
using MeetUpWebApp.Features.DeleteEvent;
using MeetUpWebApp.Features.DiscoverEvents;
using MeetUpWebApp.Features.EditEvent;
using MeetUpWebApp.Features.LeaveEventComments;
using MeetUpWebApp.Features.LeaveReview;
using MeetUpWebApp.Features.ManageUserRSVPEvents;
using MeetUpWebApp.Features.RSVPEvent;
using MeetUpWebApp.Features.ViewCreatedEvents;
using MeetUpWebApp.Features.ViewEvent;
using MeetUpWebApp.Features.ViewOrganizerReviews;
using MeetUpWebApp.Shared;
using MeetUpWebApp.Shared.Endpoints;
using MeetUpWebApp.Shared.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

#region Register Application Services
// Dependency Injection (DI) 

builder.Services.AddSingleton<LayoutService>();

builder.Services.AddTransient<CreateEventService>();
builder.Services.AddTransient<DeleteEventService>();
builder.Services.AddTransient<EditEventService>();
builder.Services.AddTransient<ViewCreatedEventService>();
builder.Services.AddTransient<ViewEventService>();
builder.Services.AddTransient<DiscoverEventsService>();
builder.Services.AddTransient<RSVPEventService>();
builder.Services.AddTransient<SharedHelper>();
builder.Services.AddTransient<ManageUserRSVPEventsService>();
builder.Services.AddTransient<LeaveEventCommentsService>();
builder.Services.AddTransient<ViewOrganizerReviewsService>();
builder.Services.AddTransient<LeaveReviewService>();
builder.Services.AddTransient<AIChatBotService>();


#endregion


// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();


//DbContext konfig�rasyonu  
builder.Services.AddDbContextFactory<ApplicationDbContext>
    (options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


//AutoMapper konfig�rasyonu  
builder.Services.AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>());


//Google Authentication konfig�rasyonu yap�land�rmas� ve cookie authentication ile entegre edildi 
//Google'dan ba�ar�l� bir �ekilde kimlik do�rulamas� yap�ld���nda, kullan�c�n�n bilgileri al�n�p cookie ile oturum a��l�yor
builder.Services.AddAuthentication(options =>
{
    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
.AddGoogle(options =>
{
    options.ClientId = builder.Configuration["Google:ClientId"] ?? string.Empty;
    options.ClientSecret = builder.Configuration["Google:ClientSecret"] ?? string.Empty;

    options.Events = new Microsoft.AspNetCore.Authentication.OAuth.OAuthEvents
    {
        OnTicketReceived = async context =>
        {
           if(context.Principal is not null)
            {
                // await context.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, context.Principal);
                await AuthenticationEndPoints.HandleSignInCallback(
                     context.HttpContext,
                     context.HttpContext.RequestServices.GetRequiredService<IDbContextFactory<ApplicationDbContext>>(),
                     isOrganizer: context.ReturnUri?.Contains("organizer", StringComparison.OrdinalIgnoreCase) ?? false,
                     principal: context.Principal,
                     redirectUri: context.ReturnUri);
              
                context.HandleResponse();
            }
        },
    };
});


//Url �zerinden userId al�p, tokendaki userId ile kar��la�t�rma yapacak policy bu sayede kullan�c� sadece kendi verisini g�rebilecek
// Di�er kullan�c�lar�n verisine eri�meye �al��t���nda 403 hatas� alacak

builder.Services.AddAuthorization(options =>
{
    
    AuthorizationPolicies.AddCustomPolices(options);
    options.AddPolicy("OrganizerOnly", policy =>
    {
        policy.RequireRole(SharedHelper.OrganizerRole);
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapAuthenticationEndpoints();


app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
